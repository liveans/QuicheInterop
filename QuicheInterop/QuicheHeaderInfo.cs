using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheHeaderInfo
    {
        private readonly uint _version;
        private readonly byte _type;
        private readonly string _sourceConnectionId;
        private readonly string _destinationConnectionId;
        private readonly string _token;
        public uint Version => _version;
        public byte Type => _type;
        public string SourceConnectionId => _sourceConnectionId;
        public string DestinationConnectionId => _destinationConnectionId;
        public string Token => _token;

        internal QuicheHeaderInfo(uint version, byte type, string sourceConnectionId, string destinationConnectionId, string token)
        {
            _version = version;
            _type = type;
            _sourceConnectionId = sourceConnectionId;
            _destinationConnectionId = destinationConnectionId;
            _token = token;
        }
    }
}
