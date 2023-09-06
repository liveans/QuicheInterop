using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    public class QuicheException : Exception
    {
        public QuicheException(QuicheErrorCode errorCode) : base("")
        {
        }
    }
}
