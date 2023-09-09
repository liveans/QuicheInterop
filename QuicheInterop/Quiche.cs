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

        static Quiche()
        {
            Version = QuicheApi.Version;
        }

        public QuicheHeaderInfo GetHeaderInfo(ReadOnlySpan<byte> buffer)
        {
            string scid, dcid, token;
            Span<byte> scidSpan = stackalloc byte[QuicheConstants.MaxConnIdLen];
            Span<byte> dcidSpan = stackalloc byte[QuicheConstants.MaxConnIdLen];
            Span<byte> tokenSpan = stackalloc byte[QuicheConstants.MaxConnIdLen];

            QuicheApi.QuicheHeaderInfo(buffer, (ulong)buffer.Length, 0, out var version, out var type, scidSpan, out var scidLen, dcidSpan, out var dcidLen, tokenSpan, out var tokenLen);

            scid = Encoding.ASCII.GetString(scidSpan[..(int)scidLen]);
            dcid = Encoding.ASCII.GetString(dcidSpan[..(int)dcidLen]);
            token = Encoding.ASCII.GetString(tokenSpan[..(int)tokenLen]);

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

        private unsafe nuint GetLength(int length)
        {
            return (nuint)length;
        }

        public QuicheConnection Accept(ReadOnlySpan<byte> sourceConnectionId, ReadOnlySpan<byte> otherDestinationConnectionId, IPEndPoint local, IPEndPoint peer, QuicheConfig config)
        {
            (SystemStructures.SockAddr localSockAddr, ulong localSockAddrLen) = GetSockAddr(local);
            (SystemStructures.SockAddr peerSockAddr, ulong peerSockAddrLen) = GetSockAddr(peer);

            return new QuicheConnection(QuicheApi.QuicheAccept(sourceConnectionId, (nuint)sourceConnectionId.Length, otherDestinationConnectionId, (nuint)otherDestinationConnectionId.Length, ref localSockAddr, (nuint)localSockAddrLen, ref peerSockAddr, (nuint)peerSockAddrLen, config.Handle));
        }
        public unsafe QuicheConnection Connect(string hostname, ReadOnlySpan<byte> sourceConnectionId, IPEndPoint local, IPEndPoint peer, QuicheConfig config)
        {
            (SystemStructures.SockAddr localSockAddr, ulong localSockAddrLen) = GetSockAddr(local);
            (SystemStructures.SockAddr peerSockAddr, ulong peerSockAddrLen) = GetSockAddr(peer);

            var handle = QuicheApi.QuicheConnect(hostname, sourceConnectionId, (nuint)sourceConnectionId.Length, ref localSockAddr, (nuint)sizeof(SystemStructures.SockAddr), ref peerSockAddr, (nuint)sizeof(SystemStructures.SockAddr), config.Handle);
            return new QuicheConnection(handle);
        }

        public List<byte> CreateNegotiateVersionPacket(ReadOnlySpan<byte> sourceConnectionId, ReadOnlySpan<byte> destinationConnectionId)
        {
            Span<byte> buffer = stackalloc byte[QuicheConstants.MaxDatagramSize];
            long packetLen = 0;
            packetLen = QuicheApi.QuicheNegotiateVersion(sourceConnectionId, (nuint)sourceConnectionId.Length, destinationConnectionId, (nuint)destinationConnectionId.Length, buffer, (nuint)buffer.Length);
            return new List<byte>(buffer[..(int)packetLen].ToArray());
        }

        public List<byte> CreateRetryPacket(ReadOnlySpan<byte> sourceConnectionId, ReadOnlySpan<byte> destinationConnectionId, ReadOnlySpan<byte> newSourceConnectionId, ReadOnlySpan<byte> token, uint version)
        {
            Span<byte> buffer = stackalloc byte[QuicheConstants.MaxDatagramSize];
            int writtenBytes = (int)QuicheApi.QuicheRetry(
                    sourceConnectionId, GetLength(sourceConnectionId.Length),
                    destinationConnectionId, GetLength(destinationConnectionId.Length),
                    newSourceConnectionId, GetLength(newSourceConnectionId.Length),
                    token, GetLength(token.Length),
                    version, buffer, GetLength(buffer.Length)
                );
            return new(buffer[..writtenBytes].ToArray());
        }

        public bool CheckVersionSupport(uint version)
        {
            return QuicheApi.QuicheVersionIsSupported(version);
        }
    }
}
