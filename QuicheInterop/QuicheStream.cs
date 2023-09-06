using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheStream
    {
        private static ulong streamIdCounter = 0;
        private readonly ulong _streamId;
        private readonly QuicheConnection _connection;
        internal ulong StreamId => _streamId;
        internal QuicheConnection Connection => _connection;
        private nint _connHandle => _connection.Handle.DangerousGetHandle();
        internal QuicheStream(QuicheConnection connection)
        {
            _streamId = Interlocked.Increment(ref streamIdCounter);
            _connection = connection;
        }

        internal QuicheStream(QuicheConnection connection, ulong streamId)
        {
            _streamId = streamId;
            _connection = connection;
        }

        internal unsafe (long, bool) Recv(Span<byte> buffer)
        {
            byte finished = 0;
            long writtenBytes = 0;
            fixed (byte* bufferPtr = buffer)
            {
                writtenBytes = QuicheApi.QuicheConnStreamRecv(_connHandle, _streamId, bufferPtr, (ulong)buffer.Length, &finished);
            }
            return (writtenBytes, finished > 0);
        }

        internal unsafe long Send(ReadOnlySpan<byte> buffer, bool finished)
        {
            long writtenBytes = 0;
            fixed (byte* bufferPtr = buffer)
            {
                writtenBytes = QuicheApi.QuicheConnStreamSend(_connHandle, _streamId, bufferPtr, (ulong)buffer.Length, Convert.ToByte(finished));
            }
            return writtenBytes;
        }

        internal unsafe int SetPriority(int priority, bool incremental)
        {
            return QuicheApi.QuicheConnStreamPriority(_connHandle, _streamId, (byte)priority, Convert.ToByte(incremental));
        }

        internal unsafe int Shutdown(QuicheShutdown shutdownDirection, QuicheErrorCode errorCode)
        {
            return QuicheApi.QuicheConnStreamShutdown(_connHandle, _streamId, (int)shutdownDirection, (ulong)errorCode);
        }

        internal unsafe long GetCapacity()
        {
            return QuicheApi.QuicheConnStreamCapacity(_connHandle, _streamId);
        }

        internal unsafe bool IsReadable()
        {
            return QuicheApi.QuicheConnStreamReadable(_connHandle, _streamId) > 0;
        }

        internal unsafe int IsWritable(int len)
        {
            return QuicheApi.QuicheConnStreamWritable(_connHandle, _streamId, (ulong) len);
        }

        internal unsafe bool IsFinished()
        {
            return QuicheApi.QuicheConnStreamFinished(_connHandle, _streamId) > 0;
        }
    }
}
