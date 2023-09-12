using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    public class SystemStructures
    {
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SockAddr
        {
            public ushort sa_family;
            public fixed byte sa_data[14];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 128)]
        public struct SockAddrStorage // Should be aligned to 128 bytes
        {
            public ushort ss_family;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Timespec
        {
            long tv_sec;
            CLong tv_nsec;
        }
    }
}
