using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct QuicheStats
    {
        /// <summary>
        /// The number of QUIC packets received on this connection.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Recv;
        /// <summary>
        /// The number of QUIC packets sent on this connection.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Sent;
        /// <summary>
        /// The number of QUIC packets that were lost.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Lost;
        /// <summary>
        /// The number of sent QUIC packets with retransmitted data.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong Retrans;
        /// <summary>
        /// The number of sent bytes.
        /// </summary>
        ulong SentBytes;
        /// <summary>
        /// The number of received bytes.
        /// </summary>
        ulong RecvBytes;
        /// <summary>
        /// The number of bytes lost.
        /// </summary>
        ulong LostBytes;
        /// <summary>
        /// The number of stream bytes retransmitted.
        /// </summary>
        ulong StreamRetransBytes;
        /// <summary>
        /// The number of known paths for the connection.
        /// </summary>
        [MarshalAs(UnmanagedType.SysUInt)] ulong PathsCount;
    }
}
