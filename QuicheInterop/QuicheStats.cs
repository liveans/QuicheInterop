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
        ulong Recv;
        ulong Sent;
        ulong Lost;
        ulong Retrans;
        ulong SentBytes;
        ulong RecvBytes;
        ulong LostBytes;
        ulong StreamRetransBytes;
        ulong PathsCount;
        ulong PeerMaxIdleTimeout;
        ulong PeerMaxUdpPayloadSize;
        ulong PeerInitialMaxData;
        ulong PeerInitialMaxStreamDataBidiLocal;
        ulong PeerInitialMaxStreamDataBidiRemote;
        ulong PeerInitialMaxStreamDataUni;
        ulong PeerInitialMaxStreamsBidi;
        ulong PeerInitialMaxStreamsUni;
        ulong PeerAckDelayExponent;
        ulong PeerMaxAckDelay;
        bool PeerDisableActiveMigration;
        ulong PeerActiveConnIdLimit;
        long PeerMaxDatagramFrameSize;
    }
}
