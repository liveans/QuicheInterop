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
        SystemStructures.SockAddrStorage LocalAddr;
        int LocalAddrLen;

        SystemStructures.SockAddrStorage PeerAddr;
        int PeerAddrLen;

        long ValidationState;
        bool Active;
        ulong Recv;
        ulong Sent;
        ulong Retrans;
        ulong Rtt;
        ulong Cwnd;
        ulong SentBytes;
        ulong RecvBytes;
        ulong LostBytes;
        ulong StreamRetransBytes;
        ulong Pmtu;
        ulong DeliveryRate;
    }
}
