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
        private QuicheConnectionSafeHandle _connHandle => _connection.Handle;
        public QuicheDatagram(QuicheConnection connection)
        {
            _connection = connection;
        }

        internal long MaxWritableLen()
        {
            return QuicheApi.QuicheConnDgramMaxWritableLen(_connHandle);
        }

        internal long ReceiveFrontLen()
        {
            return QuicheApi.QuicheConnDgramRecvFrontLen(_connHandle);
        }

        internal long ReceiveQueueLen()
        {
            return QuicheApi.QuicheConnDgramRecvQueueLen(_connHandle);
        }

        internal long ReceiveQueueByteSize()
        {
            return QuicheApi.QuicheConnDgramRecvQueueByteSize(_connHandle);
        }

        internal long SendQueueLen()
        {
            return QuicheApi.QuicheConnDgramSendQueueLen(_connHandle);
        }

        internal long SendQueueByteSize()
        {
            return QuicheApi.QuicheConnDgramSendQueueByteSize(_connHandle);
        }

        internal long Send(ReadOnlySpan<byte> buffer)
        {
            return QuicheApi.QuicheConnDgramSend(_connHandle, buffer, (nuint)buffer.Length);
        }

        // TODO : Implement Datagram Purge Outgoing API
    }
}
