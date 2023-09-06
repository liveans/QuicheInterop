using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal static class QuicheConstants
    {
        internal static int ProtocolVersion = 0x00000001;
        internal static int MaxConnIdLen = 20;
        internal static int MinClientInitialLen = 1200;
        internal static int MaxDatagramSize = 1350;
    }
}
