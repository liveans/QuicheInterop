using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct QuicheSendInfo
    {
        // The remote address the packet was received from
        public SystemStructures.SockAddrStorage* from;
        public int fromLen;

        // The local address the packet was received on
        public SystemStructures.SockAddrStorage* to;
        public int toLen;

        SystemStructures.Timespec at;
    }
}
