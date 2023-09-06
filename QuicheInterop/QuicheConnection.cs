using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    public class QuicheConnection : IAsyncDisposable
    {
        private QuicheConnectionSafeHandle _handle;
        internal QuicheConnectionSafeHandle Handle => _handle;

        SystemStructures.SockAddr _localAddress;
        SystemStructures.SockAddr _peerAddress;

        internal QuicheConnection(nint handle)
        {
            _handle = new QuicheConnectionSafeHandle(handle);
            bool success = true;
            _handle.DangerousAddRef(ref success);
        }

        unsafe internal QuicheConnection(
            ReadOnlySpan<byte> sourceConnectionId,
            ReadOnlySpan<byte> oDestinationConnectionId,
            IPEndPoint local,
            IPEndPoint peer,
            QuicheConfig config,
            void* ssl,
            bool isServer
        )
        {
            (_localAddress, var _) = GetSockAddr(local);
            (_peerAddress, var _) = GetSockAddr(peer);
            fixed (byte* scidPtr = sourceConnectionId)
            fixed (byte* oDcidPtr = oDestinationConnectionId)
            fixed (SystemStructures.SockAddr* localPtr = &_localAddress)
            fixed (SystemStructures.SockAddr* peerPtr = &_peerAddress)
            {
                _handle = new QuicheConnectionSafeHandle(
                    QuicheApi.QuicheConnNewWithTls(
                        scidPtr, (ulong)sourceConnectionId.Length,
                        oDcidPtr, (ulong)oDestinationConnectionId.Length,
                        localPtr, (ulong)sizeof(SystemStructures.SockAddr),
                        peerPtr, (ulong)sizeof(SystemStructures.SockAddr),
                        config.Handle.DangerousGetHandle(),
                        ssl,
                        Convert.ToByte(isServer)
                )
            );
            }

        }

        public unsafe static (SystemStructures.SockAddr, ulong) GetSockAddr(IPEndPoint endpoint)
        {
            SystemStructures.SockAddr sockAddr;
            sockAddr.sa_family = (ushort)endpoint.AddressFamily;
            if (!endpoint.Address.TryWriteBytes(new Span<byte>(sockAddr.sa_data, 14), out int writtenBytes))
            {
                // TODO (aaksoy): Invalid Ip
            }
            return (sockAddr, (ulong)writtenBytes);
        }

        public unsafe bool SetKeylogPath(string path)
        {
            fixed (byte* pathPtr = Encoding.ASCII.GetBytes(path + byte.MinValue))
            {
                return QuicheApi.QuicheConnSetKeylogPath(_handle.DangerousGetHandle(), pathPtr) > 0;
            }
        }

        public unsafe bool SetQlogPath(string path, string logTitle, string logDesc)
        {
            fixed (byte* pathPtr = Encoding.ASCII.GetBytes(path + byte.MinValue))
            fixed (byte* logTitlePtr = Encoding.ASCII.GetBytes(logTitle + byte.MinValue))
            fixed (byte* logDescPtr = Encoding.ASCII.GetBytes(logDesc + byte.MinValue))
            {
                return QuicheApi.QuicheConnSetQlogPath(_handle.DangerousGetHandle(), pathPtr, logTitlePtr, logDescPtr) > 0;
            }
        }

        public unsafe int SetSession(ReadOnlySpan<byte> session)
        {
            fixed (byte* sessionPtr = session)
            {
                return QuicheApi.QuicheConnSetSession(_handle.DangerousGetHandle(), sessionPtr, (ulong)session.Length);
            }
        }

        public unsafe long Recv(Span<byte> buffer, int dataLen, QuicheRecvInfo recvInfo)
        {
            long writtenBytes = 0;
            fixed (byte* bufferPtr = buffer)
            {
                writtenBytes = QuicheApi.QuicheConnRecv(_handle.DangerousGetHandle(), bufferPtr, (ulong) dataLen, &recvInfo);
            }
            //bufferTemp.CopyTo(buffer);
            return writtenBytes;
        }

        public unsafe (int, QuicheSendInfo) CreateSendPacket(Span<byte> buffer)
        {
            QuicheSendInfo sendInfo;
            fixed (byte* bufferPtr = buffer) // Pin the buffer
            {
                var writtenBytes = (int) QuicheApi.QuicheConnSend(_handle.DangerousGetHandle(), bufferPtr, (ulong)buffer.Length, &sendInfo);
                return (writtenBytes, sendInfo);
            }
        }

        public unsafe (int, QuicheSendInfo) CreateSendPacket(byte[] buffer)
        {
            return CreateSendPacket(new Span<byte>(buffer));
        }

        public unsafe ulong SendQuantum()
        {
            return QuicheApi.QuicheConnSendQuantum(_handle.DangerousGetHandle());
        }

        internal unsafe QuicheStream GetNextReadableStream()
        {
            var streamId = QuicheApi.QuicheConnStreamReadableNext(_handle.DangerousGetHandle());
            if (streamId == -1)
            {
                // TODO: Handle no stream available case
            }
            return new QuicheStream(this, (ulong) streamId);
        }

        internal unsafe QuicheStream GetNextWritableStream()
        {
            var streamId = QuicheApi.QuicheConnStreamWritableNext(_handle.DangerousGetHandle());
            if (streamId == -1)
            {
                // TODO: Handle no stream available case
            }
            return new QuicheStream(this, (ulong) streamId);
        }

        internal unsafe QuicheStreamIterator GetReadableStreamsIterator()
        {
            return new(QuicheApi.QuicheConnReadable(_handle.DangerousGetHandle()), this);
        }

        internal unsafe QuicheStreamIterator GetWritableStreamsIterator()
        {
            return new(QuicheApi.QuicheConnWritable(_handle.DangerousGetHandle()), this);
        }

        internal unsafe ulong GetMaxSendUdpPayloadSize()
        {
            return QuicheApi.QuicheConnMaxSendUdpPayloadSize(_handle.DangerousGetHandle());
        }

        internal unsafe ulong GetTimeoutAsNanos()
        {
            return QuicheApi.QuicheConnTimeoutAsNanos(_handle.DangerousGetHandle());
        }

        internal unsafe ulong GetTimeoutAsMillis()
        {
            return QuicheApi.QuicheConnTimeoutAsMillis(_handle.DangerousGetHandle());
        }

        internal unsafe void ProcessTimeoutEvent()
        {
            QuicheApi.QuicheConnOnTimeout(_handle.DangerousGetHandle());
        }

        internal unsafe int Close(bool isApplicationError, int errorCode, string reason) => Close(isApplicationError, errorCode, Encoding.ASCII.GetBytes(reason));

        internal unsafe int Close(bool isApplicationError, int errorCode, ReadOnlySpan<byte> reason)
        {
            fixed (byte* reasonPtr = reason)
            {
                return QuicheApi.QuicheConnClose(_handle.DangerousGetHandle(), Convert.ToByte(isApplicationError), (ulong)errorCode, reasonPtr, (ulong)reason.Length);
            }
        }

        internal unsafe ReadOnlySpan<byte> GetTraceId()
        {
            byte* result;
            ulong resultLen;
            QuicheApi.QuicheConnTraceId(_handle.DangerousGetHandle(), &result, &resultLen);
            return new(result, (int) resultLen);
        }

        internal unsafe ReadOnlySpan<byte> GetSourceId()
        {
            byte* result;
            ulong resultLen;
            QuicheApi.QuicheConnSourceId(_handle.DangerousGetHandle(), &result, &resultLen);
            return new(result, (int) resultLen);
        }

        internal unsafe ReadOnlySpan<byte> GetDestinationId()
        {
            byte* result;
            ulong resultLen;
            QuicheApi.QuicheConnDestinationId(_handle.DangerousGetHandle(), &result, &resultLen);
            return new(result, (int) resultLen);
        }

        internal unsafe ReadOnlySpan<byte> GetNegotiatedALPN()
        {
            byte* result;
            ulong resultLen;
            QuicheApi.QuicheConnApplicationProto(_handle.DangerousGetHandle(), &result, &resultLen);
            return new(result, (int) resultLen);
        }

        internal unsafe ReadOnlySpan<byte> GetPeerCertificate()
        {
            byte* result;
            ulong resultLen;
            QuicheApi.QuicheConnPeerCert(_handle.DangerousGetHandle(), &result, &resultLen);
            return new(result, (int) resultLen);
        }

        internal unsafe ReadOnlySpan<byte> GetSession()
        {
            byte* result;
            ulong resultLen;
            QuicheApi.QuicheConnSession(_handle.DangerousGetHandle(), &result, &resultLen);
            return new(result, (int) resultLen);
        }

        internal unsafe bool IsEstablished()
        {
            return QuicheApi.QuicheConnIsEstablished(_handle.DangerousGetHandle()) > 0;
        }

        internal unsafe bool IsInEarlyData()
        {
            return QuicheApi.QuicheConnIsInEarlyData(_handle.DangerousGetHandle()) > 0;
        }

        internal unsafe bool IsReadable()
        {
            return QuicheApi.QuicheConnIsReadable(_handle.DangerousGetHandle()) > 0;
        }

        internal unsafe bool IsDraining()
        {
            return QuicheApi.QuicheConnIsDraining(_handle.DangerousGetHandle()) > 0;
        }

        internal unsafe ulong GetPeerStreamsLeftBidirectionalCount()
        {
            return QuicheApi.QuicheConnPeerStreamsLeftBidi(_handle.DangerousGetHandle());
        }

        internal unsafe ulong GetPeerStreamsLeftUnidirectionalCount()
        {
            return QuicheApi.QuicheConnPeerStreamsLeftUni(_handle.DangerousGetHandle());
        }

        internal unsafe bool IsClosed()
        {
            return QuicheApi.QuicheConnIsClosed(_handle.DangerousGetHandle()) > 0;
        }

        private QuicheGenericError GenerateError(bool isApp, ulong errorCode, ReadOnlySpan<byte> reason)
        {
            return isApp ? new QuicheApplicationError(errorCode, reason) : new QuicheError((QuicheErrorCode)errorCode, reason);
        }

        internal unsafe QuicheGenericError? ProcessPeerError()
        {
            byte isApp;
            ulong errorCode;
            byte* reason;
            ulong reasonLen;

            bool success = QuicheApi.QuicheConnPeerError(_handle.DangerousGetHandle(), &isApp, &errorCode, &reason, &reasonLen) > 0;
            return success ? GenerateError(isApp > 0, errorCode, new(reason, (int) reasonLen)) : null;
        }

        internal unsafe QuicheGenericError? ProcessLocalError()
        {
            byte isApp;
            ulong errorCode;
            byte* reason;
            ulong reasonLen;

            bool success = QuicheApi.QuicheConnLocalError(_handle.DangerousGetHandle(), &isApp, &errorCode, &reason, &reasonLen) > 0;
            return success ? GenerateError(isApp > 0, errorCode, new(reason, (int) reasonLen)) : null;
        }

        internal unsafe QuicheStats Stats()
        {
            QuicheStats stats;
            QuicheApi.QuicheConnStats(_handle.DangerousGetHandle(), &stats);
            return stats;
        }

        internal unsafe QuichePathStats PathStats(ulong pathIndex)
        {
            QuichePathStats stats;
            QuicheApi.QuicheConnPathStats(_handle.DangerousGetHandle(), Convert.ToByte(pathIndex), &stats);
            return stats;
        }

        internal ValueTask<QuicheDatagram> CreateDatagramAsync()
        {
            return ValueTask.FromResult(new QuicheDatagram(this));
        }

        internal unsafe long ScheduleAckElicitingPacket()
        {
            return QuicheApi.QuicheConnSendAckEliciting(_handle.DangerousGetHandle());
        }

        internal unsafe long ScheduleAckElicitingPacket(IPEndPoint local, IPEndPoint peer)
        {
            (var localSockAddr, var _) = GetSockAddr(local);
            (var peerSockAddr, var _) = GetSockAddr(peer);
            return QuicheApi.QuicheConnSendAckElicitingOnPath(_handle.DangerousGetHandle(), &localSockAddr, (ulong) sizeof(SystemStructures.SockAddr), &peerSockAddr, (ulong) sizeof(SystemStructures.SockAddr));
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
