using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct QuicheRecvInfo
    {
        // The remote address the packet was received from
        public SystemStructures.SockAddr* from;
        public int fromLen;

        // The local address the packet was received on
        public SystemStructures.SockAddr* to;
        public int toLen;
    }
}
