using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct QuichePathStats
    {
        /// <summary>
        /// The local address used by this path.
        /// </summary>
        SystemStructures.SockAddrStorage LocalAddr;
        /// <summary>
        /// The local address lenght used by this path.
        /// </summary>
        int LocalAddrLen;

        /// <summary>
        /// The peer address seen by this path.
        /// </summary>
        SystemStructures.SockAddrStorage PeerAddr;
        /// <summary>
        /// The peer address length seen by this path.
        /// </summary>
        int PeerAddrLen;

        /// <summary>
        /// The validation state of the path.
        /// </summary>
        [MarshalAs(UnmanagedType.SysInt)] long ValidationState;
        /// <summary>
        /// Whether this path is active.
        /// </summary>
        bool Active;
        /// <summary>
        /// The number of QUIC packets received on this path.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Recv;
        /// <summary>
        /// The number of QUIC packets sent on this path.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Sent;
        /// <summary>
        /// The number of QUIC packets that were lost on this path.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Lost;
        /// <summary>
        /// The number of sent QUIC packets with retransmitted data on this path.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Retrans;
        /// <summary>
        /// The estimated round-trip time of the path (in nanoseconds).
        /// </summary>
        ulong Rtt;
        /// <summary>
        /// The size of the path's congestion window in bytes.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Cwnd;
        /// <summary>
        /// The number of sent bytes on this path.
        /// </summary>
        ulong SentBytes;
        /// <summary>
        /// The number of received bytes on this path.
        /// </summary>
        ulong RecvBytes;
        /// <summary>
        /// The number of bytes lost on this path.
        /// </summary>
        ulong LostBytes;
        /// <summary>
        /// The number of stream bytes retransmitted on this path.
        /// </summary>
        ulong StreamRetransBytes;
        /// <summary>
        /// The current PMTU for the path.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Pmtu;
        /// <summary>
        /// The most recent data delivery rate estimate in bytes/s.
        /// </summary>
        ulong DeliveryRate;
    }
}
