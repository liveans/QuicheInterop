using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheConnectionSafeHandle : SafeHandle
    {

        public QuicheConnectionSafeHandle(nint handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
        }

        public override bool IsInvalid => DangerousGetHandle() == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            QuicheApi.QuicheConnFree(this);
            SetHandle(IntPtr.Zero);
            SetHandleAsInvalid();

            return true;
        }
    }
}
