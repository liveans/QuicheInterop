using System.Runtime.InteropServices;

namespace QuicheInterop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct QuicheTransportParameters
    {
        /// <summary>
        /// The maximum idle timeout.
        /// </summary>
        ulong PeerMaxIdleTimeout;
        /// <summary>
        /// The maximum UDP payload size.
        /// </summary>
        ulong PeerMaxUdpPayloadSize;
        /// <summary>
        /// The initial flow control maximum data for the connection.
        /// </summary>
        ulong PeerInitialMaxData;
        /// <summary>
        /// The initial flow control maximum data for local bidirectional streams.
        /// </summary>
        ulong PeerInitialMaxStreamDataBidiLocal;
        /// <summary>
        /// The initial flow control maximum data for remote bidirectional streams.
        /// </summary>
        ulong PeerInitialMaxStreamDataBidiRemote;
        /// <summary>
        /// The initial flow control maximum data for unidirectional streams.
        /// </summary>
        ulong PeerInitialMaxStreamDataUni;
        /// <summary>
        /// The initial maximum bidirectional streams.
        /// </summary>
        ulong PeerInitialMaxStreamsBidi;
        /// <summary>
        /// The initial maximum unidirectional streams.
        /// </summary>
        ulong PeerInitialMaxStreamsUni;
        /// <summary>
        /// The ACK delay exponent.
        /// </summary>
        ulong PeerAckDelayExponent;
        /// <summary>
        /// The max ACK delay.
        /// </summary>
        ulong PeerMaxAckDelay;
        /// <summary>
        /// Whether active migration is disabled.
        /// </summary>
        [MarshalAs(UnmanagedType.U1)] bool PeerDisableActiveMigration;
        /// <summary>
        /// The active connection ID limit.
        /// </summary>
        ulong PeerActiveConnIdLimit;
        /// <summary>
        /// DATAGRAM frame extension parameter, if any.
        /// </summary>
        [MarshalAs(UnmanagedType.SysInt)] long PeerMaxDatagramFrameSize;
    }
}
