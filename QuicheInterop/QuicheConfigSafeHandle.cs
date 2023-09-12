using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal unsafe class QuicheConfigSafeHandle : SafeHandle
    {
        internal unsafe QuicheConfigSafeHandle(uint protocolVersion, bool ownsHandle = true) : base(QuicheApi.QuicheConfigNew(protocolVersion), ownsHandle)
        {
            
        }

        public override bool IsInvalid => DangerousGetHandle() == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            QuicheApi.QuicheConfigFree(this);
            SetHandle(IntPtr.Zero);
            SetHandleAsInvalid();
            return true;
        }
    }
}
