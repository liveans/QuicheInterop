using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheConfig
    {
        private QuicheConfigSafeHandle _handle;
        internal QuicheConfigSafeHandle Handle { get { return _handle; } }
        internal QuicheConfig()
        {
            //_handle = new QuicheConfigSafeHandle(0xbabababa);
            _handle = new QuicheConfigSafeHandle(0x00000001);
            bool success = true;
            _handle.DangerousAddRef(ref success);
        }

        internal unsafe int LoadCertChainFromPemFile(string path)
        {
            fixed (byte* pathPtr = Encoding.ASCII.GetBytes(path + byte.MinValue)) // Add null termination character at the end
            {
                return QuicheApi.QuicheConfigLoadCertChainFromPemFile(_handle.DangerousGetHandle(), pathPtr);
            }
        }

        internal unsafe int LoadPrivKeyFromPemFile(string path)
        {
            fixed (byte* pathPtr = Encoding.ASCII.GetBytes(path + byte.MinValue)) // Add null termination character at the end
            {
                return QuicheApi.QuicheConfigLoadPrivKeyFromPemFile(_handle.DangerousGetHandle(), pathPtr);
            }
        }

        internal unsafe int LoadVerifyLocationsFromFile(string path)
        {
            fixed (byte* pathPtr = Encoding.ASCII.GetBytes(path + byte.MinValue)) // Add null termination character at the end
            {
                return QuicheApi.QuicheConfigLoadVerifyLocationsFromFile(_handle.DangerousGetHandle(), pathPtr);
            }
        }

        internal unsafe int LoadVerifyLocationsFromDirectory(string path)
        {
            fixed (byte* pathPtr = Encoding.ASCII.GetBytes(path + byte.MinValue)) // Add null termination character at the end
            {
                return QuicheApi.QuicheConfigLoadVerifyLocationsFromDirectory(_handle.DangerousGetHandle(), pathPtr);
            }
        }

        internal unsafe void EnableVerifyPeer(bool v = true)
        {
            QuicheApi.QuicheConfigVerifyPeer(_handle.DangerousGetHandle(), Convert.ToByte(v));
        }

        internal unsafe void EnableGrease(bool v = true)
        {
            QuicheApi.QuicheConfigGrease(_handle.DangerousGetHandle(), Convert.ToByte(v));
        }

        internal unsafe void EnableLogKeys()
        {
            QuicheApi.QuicheConfigLogKeys(_handle.DangerousGetHandle());
        }

        internal unsafe void EnableEarlyData()
        {
            QuicheApi.QuicheConfigEnableEarlyData(_handle.DangerousGetHandle());
        }

        public unsafe int SetApplicationProtocols(ReadOnlySpan<SslApplicationProtocol> protocols)
        {
            List<byte> alpns = new();
            foreach (var proto in protocols)
            {
                var protocol = proto.Protocol;
                alpns.Add((byte)protocol.Length);
                alpns.AddRange(protocol.ToArray());
            }
            //alpns.Add(0); // Add null termination character at the end
            fixed (byte* alpnsPointer = alpns.ToArray())
            {
                return QuicheApi.QuicheConfigSetApplicationProtocols(_handle.DangerousGetHandle(), alpnsPointer, (ulong)alpns.Count);
            }
        }

        internal unsafe void SetMaxIdleTimeout(ulong maxIdleTimeout)
        {
            QuicheApi.QuicheConfigSetMaxIdleTimeout(_handle.DangerousGetHandle(), maxIdleTimeout);
        }

        internal unsafe void SetMaxRecvUdpPayloadSize(ulong value)
        {
            QuicheApi.QuicheConfigSetMaxRecvUdpPayloadSize(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetMaxSendUdpPayloadSize(ulong value)
        {
            QuicheApi.QuicheConfigSetMaxSendUdpPayloadSize(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetInitialMaxData(ulong value)
        {
            QuicheApi.QuicheConfigSetInitialMaxData(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetInitialMaxStreamDataBidiLocal(ulong value)
        {
            QuicheApi.QuicheConfigSetInitialMaxStreamDataBidiLocal(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetInitialMaxStreamDataBidiRemote(ulong value)
        {
            QuicheApi.QuicheConfigSetInitialMaxStreamDataBidiRemote(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetInitialMaxStreamDataUni(ulong value)
        {
            QuicheApi.QuicheConfigSetInitialMaxStreamDataUni(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetInitialMaxStreamsBidi(ulong value)
        {
            QuicheApi.QuicheConfigSetInitialMaxStreamsBidi(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetInitialMaxStreamsUni(ulong value)
        {
            QuicheApi.QuicheConfigSetInitialMaxStreamsUni(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetAckDelayExponent(ulong value)
        {
            QuicheApi.QuicheConfigSetAckDelayExponent(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetMaxAckDelay(ulong value)
        {
            QuicheApi.QuicheConfigSetMaxAckDelay(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetDisableActiveMigration(bool value)
        {
            QuicheApi.QuicheConfigSetDisableActiveMigration(_handle.DangerousGetHandle(), Convert.ToByte(value));
        }

        internal unsafe void SetCcAlgorithm(QuicheCcAlgorithm value)
        {
            QuicheApi.QuicheConfigSetCcAlgorithm(_handle.DangerousGetHandle(), (int)value);
        }

        internal unsafe void EnableHystart(bool value)
        {
            QuicheApi.QuicheConfigEnableHystart(_handle.DangerousGetHandle(), Convert.ToByte(value));
        }

        internal unsafe void EnablePacing(bool value)
        {
            QuicheApi.QuicheConfigEnablePacing(_handle.DangerousGetHandle(), Convert.ToByte(value));
        }

        internal unsafe void SetMaxPacingRate(ulong value)
        {
            QuicheApi.QuicheConfigSetMaxPacingRate(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void EnableDatagram(bool enabled, ulong recvQueueLen, ulong sendQueueLen)
        {
            QuicheApi.QuicheConfigEnableDgram(_handle.DangerousGetHandle(), Convert.ToByte(enabled), recvQueueLen, sendQueueLen);
        }

        internal unsafe void SetMaxConnectionWindow(ulong value)
        {
            QuicheApi.QuicheConfigSetMaxConnectionWindow(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetMaxStreamWindow(ulong value)
        {
            QuicheApi.QuicheConfigSetMaxStreamWindow(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetActiveConnectionIdLimit(ulong value)
        {
            QuicheApi.QuicheConfigSetActiveConnectionIdLimit(_handle.DangerousGetHandle(), value);
        }

        internal unsafe void SetStatelessResetToken(byte[] token)
        {
            if (token.Length < 16)
            {
                throw new ArgumentException("Token must be at least 16 bytes.");
            }

            fixed (byte* tokenPtr = token)
            {
                QuicheApi.QuicheConfigSetStatelessResetToken(_handle.DangerousGetHandle(), tokenPtr);
            }
        }
    }
}
