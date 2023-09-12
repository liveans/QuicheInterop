using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheStreamIteratorSafeHandle : SafeHandle
    {
        public QuicheStreamIteratorSafeHandle(nint handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
            
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            QuicheApi.QuicheStreamIterFree(this);
            SetHandle(IntPtr.Zero);
            SetHandleAsInvalid();
            return true;
        }
    }
}
