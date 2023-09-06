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

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct SockAddr_In
        {
            public short sin_family;
            public ushort sin_port;
            public In_Addr sin_addr;
            public fixed byte sin_zero[8];
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct In_Addr
        {
            public byte s_b1, s_b2, s_b3, s_b4;
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
            long tv_nsec;
        }

        public static unsafe void CStyle()
        {
            SockAddr s;
            SockAddr* ps = &s;

            SockAddr_In* psa = (SockAddr_In*)ps;

            var inAddr = new In_Addr();

            inAddr.s_b1 = 192;
            inAddr.s_b2 = 168;
            inAddr.s_b3 = 168;
            inAddr.s_b4 = 56;
            psa->sin_addr = inAddr;
            Console.WriteLine("{0}.{1}.{2}.{3}",
                               psa->sin_addr.s_b1, psa->sin_addr.s_b2,
                               psa->sin_addr.s_b3, psa->sin_addr.s_b4);
        }
    }
}
