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
        private readonly delegate* unmanaged[Cdecl]<nint, void> _QuicheConfigFree;
        internal unsafe QuicheConfigSafeHandle(uint protocolVersion, bool ownsHandle = true) : base(QuicheApi.QuicheConfigNew(protocolVersion), ownsHandle)
        {
            _QuicheConfigFree = QuicheApi.QuicheConfigFree;
        }

        public override bool IsInvalid => DangerousGetHandle() == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            _QuicheConfigFree(DangerousGetHandle());
            SetHandle(IntPtr.Zero);
            SetHandleAsInvalid();
            return true;
        }
    }
}
