using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    /// <summary>
    /// The side of the stream to be shut down.
    /// </summary>
    internal enum QuicheShutdown
    {
        /// <summary>
        /// Stop receiving stream data.
        /// </summary>
        Read = 0,

        /// <summary>
        /// Stop sending stream data.
        /// </summary>
        Write = 1,
    }
}
