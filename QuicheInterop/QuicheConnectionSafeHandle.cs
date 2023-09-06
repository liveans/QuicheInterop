using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal unsafe class QuicheConnectionSafeHandle : SafeHandle
    {
        private readonly delegate* unmanaged[Cdecl]<nint, void> _QuicheConnectionFree;

        public QuicheConnectionSafeHandle(nint handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
            _QuicheConnectionFree = QuicheApi.QuicheConnFree;
        }

        public override bool IsInvalid => DangerousGetHandle() == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            _QuicheConnectionFree(DangerousGetHandle());
            SetHandle(IntPtr.Zero);
            SetHandleAsInvalid();

            return true;
        }
    }
}
