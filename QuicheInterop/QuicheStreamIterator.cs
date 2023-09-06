﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal class QuicheStreamIterator
    {
        private readonly QuicheStreamIteratorSafeHandle _handle;
        private readonly QuicheConnection _connection;
        internal QuicheStreamIterator(nint handle, QuicheConnection connection)
        {
            _handle = new(handle);
            _connection = connection;
        }

        internal unsafe QuicheStream? Next()
        {
            ulong streamId = 0;
            bool success = QuicheApi.QuicheStreamIterNext(_handle.DangerousGetHandle(), &streamId) > 0;
            return success ? new QuicheStream(_connection, streamId) : null;
        }

        public IEnumerator<QuicheStream> GetEnumerator()
        {
            var next = Next();
            if (next is not null)
            {
                yield return next;
            }
        }
    }
}
