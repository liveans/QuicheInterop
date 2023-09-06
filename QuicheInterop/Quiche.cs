using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using QuicheConnPtr = nint;
using QuicheConfigPtr = nint;
using QuicheStreamIterPtr = nint;
using System.Net;

namespace QuicheInterop
{
    internal class Quiche
    {
        static QuicheApi _api = new QuicheApi();
        public static string Version { get; private set; }

        public Quiche()
        {
        }

        static unsafe Quiche()
        {
            if (!QuicheApi.IsValid)
            {
                throw new NullReferenceException();
            }

            Version = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(QuicheApi.QuicheVersion()));
        }

        public unsafe QuicheHeaderInfo GetHeaderInfo(ReadOnlySpan<byte> buffer)
        {
            uint version;
            byte type;
            string scid, dcid, token;

            fixed (byte* ptr = buffer)
            {
                byte* scidPtr = stackalloc byte[QuicheConstants.MaxConnIdLen], dcidPtr = stackalloc byte[QuicheConstants.MaxConnIdLen], tokenPtr = stackalloc byte[QuicheConstants.MaxConnIdLen];
                ulong scidLen, dcidLen, tokenLen;

                QuicheApi.QuicheHeaderInfo(ptr, (ulong)buffer.Length, 0, &version, &type, scidPtr, &scidLen, dcidPtr, &dcidLen, tokenPtr, &tokenLen);

                scid = Encoding.ASCII.GetString(scidPtr, (int)scidLen);
                dcid = Encoding.ASCII.GetString(dcidPtr, (int)dcidLen);
                token = Encoding.ASCII.GetString(tokenPtr, (int)tokenLen);
            }

            return new QuicheHeaderInfo(version, type, scid, dcid, token);
        }

        private unsafe (SystemStructures.SockAddr, ulong) GetSockAddr(IPEndPoint endpoint)
        {
            SystemStructures.SockAddr sockAddr;
            sockAddr.sa_family = (ushort)endpoint.AddressFamily;
            if (!endpoint.Address.TryWriteBytes(new Span<byte>(sockAddr.sa_data, 14), out int writtenBytes))
            {
                // TODO (aaksoy): Invalid Ip
                throw new Exception("Invalid IP");
            }
            return (sockAddr, (ulong)writtenBytes);
        }

        private unsafe ulong GetLength(int length)
        {
            return (ulong)length;
        }

        public unsafe QuicheConnection Accept(ReadOnlySpan<byte> sourceConnectionId, ReadOnlySpan<byte> otherDestinationConnectionId, IPEndPoint local, IPEndPoint peer, QuicheConfig config)
        {
            (SystemStructures.SockAddr localSockAddr, ulong localSockAddrLen) = GetSockAddr(local);
            (SystemStructures.SockAddr peerSockAddr, ulong peerSockAddrLen) = GetSockAddr(peer);

            fixed (byte* scidPtr = sourceConnectionId)
            fixed (byte* odcidPtr = otherDestinationConnectionId)
            {
                return new QuicheConnection(QuicheApi.QuicheAccept(scidPtr, (ulong)sourceConnectionId.Length, odcidPtr, (ulong)otherDestinationConnectionId.Length, &localSockAddr, localSockAddrLen, &peerSockAddr, peerSockAddrLen, config.Handle.DangerousGetHandle()));
            }
        }
        public unsafe QuicheConnection Connect(string hostname, ReadOnlySpan<byte> sourceConnectionId, IPEndPoint local, IPEndPoint peer, QuicheConfig config)
        {
            (SystemStructures.SockAddr localSockAddr, ulong localSockAddrLen) = GetSockAddr(local);
            (SystemStructures.SockAddr peerSockAddr, ulong peerSockAddrLen) = GetSockAddr(peer);

            fixed (byte* hostnamePtr = Encoding.ASCII.GetBytes(hostname + byte.MinValue))
            fixed (byte* scidPtr = sourceConnectionId)
            {
                try
                {
                    bool success = true;
                    config.Handle.DangerousAddRef(ref success);
                    var handle = QuicheApi.QuicheConnect(hostnamePtr, scidPtr, (ulong)sourceConnectionId.Length, &localSockAddr, (ulong)sizeof(SystemStructures.SockAddr), &peerSockAddr, (ulong)sizeof(SystemStructures.SockAddr), config.Handle.DangerousGetHandle());
                    return new QuicheConnection(handle);
                }
                finally
                {
                    config.Handle.DangerousRelease();
                }
            }
        }

        public unsafe List<byte> CreateNegotiateVersionPacket(ReadOnlySpan<byte> sourceConnectionId, ReadOnlySpan<byte> destinationConnectionId)
        {
            Span<byte> buffer = new byte[QuicheConstants.MaxDatagramSize];
            long packetLen = 0;
            fixed (byte* scidPtr = sourceConnectionId)
            fixed (byte* dcidPtr = destinationConnectionId)
            fixed (byte* bufferPtr = buffer)
            {
                packetLen = QuicheApi.QuicheNegotiateVersion(scidPtr, (ulong)sourceConnectionId.Length, dcidPtr, (ulong)destinationConnectionId.Length, bufferPtr, (ulong)buffer.Length);
            }
            return new List<byte>(buffer[..(int)packetLen].ToArray());
        }

        public unsafe List<byte> CreateRetryPacket(ReadOnlySpan<byte> sourceConnectionId, ReadOnlySpan<byte> destinationConnectionId, ReadOnlySpan<byte> newSourceConnectionId, ReadOnlySpan<byte> token, uint version)
        {
            Span<byte> buffer = new byte[QuicheConstants.MaxDatagramSize];
            int writtenBytes;
            fixed (byte* scidPtr = sourceConnectionId)
            fixed (byte* dcidPtr = destinationConnectionId)
            fixed (byte* newScidPtr = newSourceConnectionId)
            fixed (byte* tokenPtr = sourceConnectionId)
            fixed (byte* bufferPtr = buffer)
            {
                writtenBytes = (int) QuicheApi.QuicheRetry(
                    scidPtr, GetLength(sourceConnectionId.Length),
                    dcidPtr, GetLength(destinationConnectionId.Length),
                    newScidPtr, GetLength(newSourceConnectionId.Length),
                    tokenPtr, GetLength(token.Length),
                    version, bufferPtr, GetLength(buffer.Length)
                );
            }
            return new(buffer[..writtenBytes].ToArray());
        }

        public unsafe bool CheckVersionSupport(uint version)
        {
            return QuicheApi.QuicheVersionIsSupported(version) > 0;
        }
    }
}
