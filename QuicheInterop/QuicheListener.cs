using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Quic;
using System.Net.Sockets;
using System.Net.Security;
using System.Net;

namespace QuicheInterop
{
    internal class QuicheListener
    {
        private QuicheConfig _config;
        private Socket _socket;
        private Func<QuicConnection, SslClientHelloInfo, CancellationToken, ValueTask<QuicServerConnectionOptions>> _connectionCallback;
        private int _pendingConnectionsCapacity;
        public IPEndPoint LocalEndPoint { get; }
        internal static ValueTask<QuicheListener> ListenAsync(QuicListenerOptions options)
        {
            QuicheListener listener = new(options);
            // options.Validate(nameof(options));
            return ValueTask.FromResult(listener);
        }
#pragma warning disable CA1416
        private unsafe QuicheListener(QuicListenerOptions options)
        {
            // Let's create socket listener, bind and listen.
            _socket = new Socket(options.ListenEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(options.ListenEndPoint);
            _socket.Listen(options.ListenBacklog);

            _pendingConnectionsCapacity = options.ListenBacklog;
            LocalEndPoint = (IPEndPoint)_socket.LocalEndPoint!;
            // Let's set config
            _config = new QuicheConfig();
            _config.SetApplicationProtocols(new(options.ApplicationProtocols.ToArray()));
        }

        public async ValueTask<QuicConnection> AcceptConnectionAsync(CancellationToken cancellationToken = default)
        {
            var connection = await _socket.AcceptAsync(cancellationToken);
            ArraySegment<byte> buffer = new ArraySegment<byte>();
            var headerLength = await connection.ReceiveAsync(buffer);

            throw new NotImplementedException();
        }
#pragma warning restore CA1416
    }
}
