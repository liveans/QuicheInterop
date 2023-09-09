using QuicheInterop;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Net.Security;

Console.WriteLine(Quiche.Version);

byte[] buffer = new byte[1350];
byte[] sendBuffer = new byte[1350];

Quiche quiche = new Quiche();
QuicheConfig config = new();
config.SetApplicationProtocols(new SslApplicationProtocol[] 
{ 
    new("hq-interop"),
    new("hq-29"),
    new("hq-28"),
    new("hq-27"),
    new("http/0.9"),
});
config.SetMaxIdleTimeout(5000);
config.SetInitialMaxData(10_000_000);
config.SetMaxRecvUdpPayloadSize(1350);
config.SetMaxSendUdpPayloadSize(1350);
config.SetInitialMaxStreamDataBidiLocal(1000000);
config.SetInitialMaxStreamDataBidiRemote(1000000);
config.SetInitialMaxStreamsBidi(100);
config.SetInitialMaxStreamsUni(100);
config.SetDisableActiveMigration(true);
//var test = quiche.CreateNegotiateVersionPacket("ahmet", "aksoy");
Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
socket.Blocking = false;
socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));

IPEndPoint remote = new IPEndPoint(IPAddress.Loopback, 4567);
EndPoint remoteEp = remote;

QuicheConnection connection = quiche.Connect("localhost", "ahmetahmetahmetahmet"u8, (IPEndPoint) socket.LocalEndPoint!, (IPEndPoint)remote, config);

SendPacket();

bool requestSent = false;
bool firstTimeInfoEstablish = false;
int readLen = 0;

while (true)
{
    Thread.Sleep(2000);
    while (true)
    {
        try
        {
            //buffer.CopyTo(newBuffer);
            //byte[] newBuffer = new byte[1350];
            readLen = socket.ReceiveFrom(buffer, ref remoteEp);
            if (readLen > 0)
            {
                QuicheRecvInfoLifetime recvInfoLifetime = new((IPEndPoint)socket.LocalEndPoint!, remote);
                var info = recvInfoLifetime.GetQuicheRecvInfo();
                var dataLen = (int)connection.Recv(buffer, readLen, info);
                Console.WriteLine("Data Received Len = " + dataLen);
                Console.WriteLine($"Data: {Encoding.ASCII.GetString(buffer[..dataLen])}");
                string hex = BitConverter.ToString(buffer[..dataLen]).Replace("-", string.Empty);
                Console.WriteLine($"Data as hex: {hex}");
            }
        }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.WouldBlock || ex.SocketErrorCode == SocketError.TryAgain)
        {
            break;
        }
    }

    if (readLen > 0)
    {
        //var headerInfo = quiche.GetHeaderInfo(buffer[..readLen]);
        //if (headerInfo.Version != 0 && !quiche.CheckVersionSupport(headerInfo.Version))
        //{
        //    Console.WriteLine(headerInfo.Version);
        //    Console.WriteLine("Version is not supported!");
        //    // return;
        //}
        //Console.WriteLine($"DestinationConnectionId: {headerInfo.DestinationConnectionId}");
        //Console.WriteLine($"SourceConnectionId: {headerInfo.SourceConnectionId}");
        //Console.WriteLine($"Type: {headerInfo.Type}");
        //if (headerInfo.Version == 0)
        //{
        //    Console.WriteLine("Version Negotiation Packet Received!");
        //}
    }

    if (connection.IsClosed())
    {
        Console.WriteLine("Connection closed!");
        socket.Shutdown(SocketShutdown.Both);
    }
    
    if (connection.IsEstablished() && !requestSent)
    {
        Console.WriteLine($"Negotiated ALPN: {Encoding.ASCII.GetString(connection.GetNegotiatedALPN())}");
        Console.WriteLine("Connected!");
        QuicheStream stream = new QuicheStream(connection);
        stream.SetPriority(0, true);
        var res = stream.Send("GET /index.html\r\n"u8, true);
        if (res < 0)
        {
            Console.WriteLine("Failure!");
            Console.WriteLine($"Request Result: {res}");
        }
        else
        {
            requestSent = true;
            Console.WriteLine("Request sent!");
        }
    }

    if (connection.IsEstablished())
    {
        if (firstTimeInfoEstablish == false)
        {
            Console.WriteLine("Connection established!");
            firstTimeInfoEstablish = true;
        }
        foreach (var stream in connection.GetReadableStreamsIterator())
        {
            if (stream is not null)
            {
                (var len, var isFinished) = stream.Recv(buffer);
                Console.WriteLine("Got data from stream");
                if (isFinished)
                {
                    Console.WriteLine("Stream finished");
                    connection.Close(true, 0, "");
                }
            }
        }
    }

    SendPacket();

    if (connection.IsDraining())
    {
        Console.WriteLine("Connection is draining!");
        return;
    }
}

void SendPacket()
{
    while (true)
    {
        // buffer = new byte[1350];
        (int result, _) = connection.CreateSendPacket(sendBuffer);
        if (result == (int)QuicheErrorCode.Done)
        {
            break;
        }
        else if (result < 0)
        {
            Console.WriteLine($"Failure: {result}");
        }
        else
        {
            Console.WriteLine("Sent!");
            socket.SendTo(sendBuffer[..result], remote);
        }
    }
}