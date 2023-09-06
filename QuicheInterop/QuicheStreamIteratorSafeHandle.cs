using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal unsafe class QuicheStreamIteratorSafeHandle : SafeHandle
    {
        private readonly delegate* unmanaged[Cdecl]<nint, void> _QuicheStreamIterFree;
        public QuicheStreamIteratorSafeHandle(nint handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
            _QuicheStreamIterFree = QuicheApi.QuicheStreamIterFree;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            _QuicheStreamIterFree(handle);
            SetHandle(IntPtr.Zero);
            SetHandleAsInvalid();
            return true;
        }
    }
}
