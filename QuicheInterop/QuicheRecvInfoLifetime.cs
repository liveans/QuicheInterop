using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheRecvInfoLifetime
    {
        private readonly SystemStructures.SockAddr _to;
        private readonly SystemStructures.SockAddr _from;
        private QuicheRecvInfo _recvInfo;
        public unsafe QuicheRecvInfoLifetime(IPEndPoint local, IPEndPoint peer)
        {
            QuicheRecvInfo recvInfo;
            // Lifetime Issue
            (_from, var fromLen) = QuicheConnection.GetSockAddr(peer);
            (_to, var toLen) = QuicheConnection.GetSockAddr(local);
            recvInfo.fromLen = sizeof(SystemStructures.SockAddr);
            recvInfo.toLen = sizeof(SystemStructures.SockAddr);
            fixed (SystemStructures.SockAddr* fromPtr = &_from)
            fixed (SystemStructures.SockAddr* toPtr = &_to)
            {
                recvInfo.from = fromPtr;
                recvInfo.to = toPtr;
            }
            _recvInfo = recvInfo;
        }

        public QuicheRecvInfo GetQuicheRecvInfo()
        {
            return _recvInfo;
        }
    }
}
