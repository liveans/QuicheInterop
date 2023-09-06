using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    public enum QuicheErrorCode
    {
        Done = -1,
        TooShort = -2,
        UnknownVersion = -3,
        InvalidFrame = -4,
        InvalidPacket = -5,
        InvalidState = -6,
        InvalidStreamState = -7,
        InvalidTransportParam = -8,
        CryptoFail = -9,
        TlsFail = -10,
        FlowControl = -11,
        StreamLimit = -12,
        FinalSize = -13,
        CongestionControl = -14,
        StreamStopped = -15,
        StreamReset = -16,
        IdLimit = -17,
        OutOfIdentifiers = -18,
        KeyUpdate = -19,
    }

    internal class QuicheGenericError
    {
        public bool IsApplicationError { get; set; }
        public ulong ErrorCode { get; set; }
        public byte[] Reason { get; set; }

        protected QuicheGenericError(bool isApplicationError, ulong errorCode, ReadOnlySpan<byte> reason)
        {
            IsApplicationError = isApplicationError;
            ErrorCode = errorCode;
            Reason = reason.ToArray();
        }
    }

    internal sealed class QuicheApplicationError : QuicheGenericError
    {
        public QuicheApplicationError(ulong errorCode, ReadOnlySpan<byte> reason) : base(true, errorCode, reason)
        {
        }
    }

    internal sealed class QuicheError : QuicheGenericError
    {
        public QuicheError(QuicheErrorCode errorCode, ReadOnlySpan<byte> reason) : base(false, (ulong) errorCode, reason)
        {
        }
    }
}
