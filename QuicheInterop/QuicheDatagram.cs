using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheDatagram
    {
        private readonly QuicheConnection _connection;
        private nint _connHandle => _connection.Handle.DangerousGetHandle();
        public QuicheDatagram(QuicheConnection connection) 
        {
            _connection = connection;
        }

        internal unsafe long MaxWritableLen()
        {
            return QuicheApi.QuicheConnDgramMaxWritableLen(_connHandle);
        }

        internal unsafe long ReceiveFrontLen()
        {
            return QuicheApi.QuicheConnDgramRecvFrontLen(_connHandle);
        }

        internal unsafe long ReceiveQueueLen()
        {
            return QuicheApi.QuicheConnDgramRecvQueueLen(_connHandle);
        }

        internal unsafe long ReceiveQueueByteSize()
        {
            return QuicheApi.QuicheConnDgramRecvQueueByteSize(_connHandle);
        }

        internal unsafe long SendQueueLen()
        {
            return QuicheApi.QuicheConnDgramSendQueueLen(_connHandle);
        }

        internal unsafe long SendQueueByteSize()
        {
            return QuicheApi.QuicheConnDgramSendQueueByteSize(_connHandle);
        }

        internal unsafe long Send(ReadOnlySpan<byte> buffer)
        {
            fixed (byte* bufferPtr = buffer)
            {
                return QuicheApi.QuicheConnDgramSend(_connHandle, bufferPtr, (ulong) buffer.Length);
            }
        }

        // TODO : Implement Datagram Purge Outgoing API
    }
}
