using System;
using System.Runtime.InteropServices;

using QuicheConnPtr = nint;
using QuicheConfigPtr = nint;
using QuicheStreamIterPtr = nint;

namespace QuicheInterop
{
    internal sealed unsafe partial class QuicheApi
    {
        //[LibraryImport("quiche", EntryPoint = "quiche_conn_send")]
        //internal static partial long QuicheConnSend(nint handle, byte* outValue, ulong outLen, QuicheSendInfo* sendInfo);

        //[LibraryImport("quiche", EntryPoint = "quiche_conn_recv")]
        //internal static partial long QuicheConnRecv(nint handle, byte* outValue, ulong outLen, QuicheRecvInfo* receiveInfo);

        internal static readonly delegate* unmanaged[Cdecl]<byte*> QuicheVersion;
        internal static readonly delegate* unmanaged[Cdecl]<delegate* unmanaged[Cdecl]<byte*, void*, void>, void*, int> QuicheEnableDebugLogging;
        internal static readonly delegate* unmanaged[Cdecl]<uint, QuicheConfigPtr> QuicheConfigNew;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int> QuicheConfigLoadCertChainFromPemFile;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int> QuicheConfigLoadPrivKeyFromPemFile;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int> QuicheConfigLoadVerifyLocationsFromFile;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int> QuicheConfigLoadVerifyLocationsFromDirectory;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void> QuicheConfigVerifyPeer;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void> QuicheConfigGrease;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, void> QuicheConfigLogKeys;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, void> QuicheConfigEnableEarlyData;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, ulong, int> QuicheConfigSetApplicationProtocols;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetMaxIdleTimeout;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetMaxRecvUdpPayloadSize;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetMaxSendUdpPayloadSize;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetInitialMaxData;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetInitialMaxStreamDataBidiLocal;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetInitialMaxStreamDataBidiRemote;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetInitialMaxStreamDataUni;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetInitialMaxStreamsBidi;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetInitialMaxStreamsUni;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetAckDelayExponent;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetMaxAckDelay;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void> QuicheConfigSetDisableActiveMigration;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, int, void> QuicheConfigSetCcAlgorithm;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void> QuicheConfigEnableHystart;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void> QuicheConfigEnablePacing;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetMaxPacingRate;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, ulong, ulong, void> QuicheConfigEnableDgram;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetMaxConnectionWindow;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetMaxStreamWindow;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void> QuicheConfigSetActiveConnectionIdLimit;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, void> QuicheConfigSetStatelessResetToken;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConfigPtr, void> QuicheConfigFree;
        
        internal static readonly delegate* unmanaged[Cdecl]<byte*, ulong, ulong, uint*, byte*, byte*, ulong*, byte*, ulong*, byte*, ulong*, int> QuicheHeaderInfo;
        // TODO (aaksoy) : Change nint types with sockaddr struct itself
        internal static readonly delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, QuicheConfigPtr, QuicheConnPtr> QuicheAccept;
        internal static readonly delegate* unmanaged[Cdecl]<byte*, byte*, ulong, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, QuicheConfigPtr, QuicheConnPtr> QuicheConnect;

        internal static readonly delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, byte*, ulong, long> QuicheNegotiateVersion;
        internal static readonly delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, byte*, ulong, byte*, ulong, uint, byte*, ulong, long> QuicheRetry;
        internal static readonly delegate* unmanaged[Cdecl]<uint, byte> QuicheVersionIsSupported;
        internal static readonly delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, QuicheConfigPtr, void*, byte, QuicheConnPtr> QuicheConnNewWithTls;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, byte> QuicheConnSetKeylogPath;
        // internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, int, void> QuicheConnSetKeylogFd;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, byte*, byte*, byte> QuicheConnSetQlogPath;
        // internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, int, byte*, byte*, void> QuicheConnSetQlogFd;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, int> QuicheConnSetSession;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, QuicheRecvInfo*, long> QuicheConnRecv;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, QuicheSendInfo*, long> QuicheConnSend;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong> QuicheConnSendQuantum;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte*, ulong, byte*, long> QuicheConnStreamRecv;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte*, ulong, byte, long> QuicheConnStreamSend;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte, byte, int> QuicheConnStreamPriority;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, int, ulong, int> QuicheConnStreamShutdown;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, long> QuicheConnStreamCapacity;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte> QuicheConnStreamReadable;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnStreamReadableNext;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, ulong, int> QuicheConnStreamWritable;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnStreamWritableNext;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte> QuicheConnStreamFinished;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, QuicheStreamIterPtr> QuicheConnReadable;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, QuicheStreamIterPtr> QuicheConnWritable;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong> QuicheConnMaxSendUdpPayloadSize;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong> QuicheConnTimeoutAsNanos;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong> QuicheConnTimeoutAsMillis;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, void> QuicheConnOnTimeout;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte, ulong, byte*, ulong, int> QuicheConnClose;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void> QuicheConnTraceId;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void> QuicheConnSourceId;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void> QuicheConnDestinationId;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void> QuicheConnApplicationProto;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void> QuicheConnPeerCert;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void> QuicheConnSession;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte> QuicheConnIsEstablished;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte> QuicheConnIsInEarlyData;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte> QuicheConnIsReadable;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte> QuicheConnIsDraining;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong> QuicheConnPeerStreamsLeftBidi;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong> QuicheConnPeerStreamsLeftUni;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte> QuicheConnIsClosed;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte> QuicheConnIsTimedOut;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong*, byte**, ulong*, byte> QuicheConnPeerError;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong*, byte**, ulong*, byte> QuicheConnLocalError;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheStreamIterPtr, ulong*, byte> QuicheStreamIterNext;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheStreamIterPtr, void> QuicheStreamIterFree;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, QuicheStats*, void> QuicheConnStats;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, QuichePathStats*, int> QuicheConnPathStats;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte> QuicheConnIsServer;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnDgramMaxWritableLen;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnDgramRecvFrontLen;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnDgramRecvQueueLen;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnDgramRecvQueueByteSize;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnDgramSendQueueLen;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnDgramSendQueueByteSize;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, long> QuicheConnDgramRecv;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, long> QuicheConnDgramSend;

        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, delegate* unmanaged[Cdecl]<byte*, ulong, byte>, void> QuicheConnDgramPurgeOutgoing;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, long> QuicheConnSendAckEliciting;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, long> QuicheConnSendAckElicitingOnPath;
        internal static readonly delegate* unmanaged[Cdecl]<QuicheConnPtr, void> QuicheConnFree;

        internal static bool IsValid { get; set; }

        private static void Log(string data)
        {
            Console.WriteLine(data);
        }
        static QuicheApi()
        {
            string LibraryPostfix = OperatingSystem.IsWindows() ? "dll" : "so";
            bool loaded = NativeLibrary.TryLoad($"quiche.{LibraryPostfix}", typeof(QuicheApi).Assembly, null, handle: out IntPtr quicheHandle); ;

            if (!loaded)
            {
                Log("Problem on loading library!");
                return;
            }

            Log("Library Loaded!");

            QuicheVersion = (delegate* unmanaged[Cdecl]<byte*>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Version);
            QuicheEnableDebugLogging = (delegate* unmanaged[Cdecl]<delegate* unmanaged[Cdecl]<byte*, void*, void>, void*, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.EnableDebugLogging);

            QuicheConfigNew = (delegate* unmanaged[Cdecl]<uint, nint>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.ConfigurationNew);
            QuicheConfigLoadCertChainFromPemFile = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.LoadCertChainFromPemFile);
            QuicheConfigLoadPrivKeyFromPemFile = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.LoadPrivKeyFromPemFile);
            QuicheConfigLoadVerifyLocationsFromFile = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.LoadVerifyLocationsFromFile);
            QuicheConfigLoadVerifyLocationsFromDirectory = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.LoadVerifyLocationsFromDirectory);
            QuicheConfigVerifyPeer = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.VerifyPeer);
            QuicheConfigGrease = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.Grease);
            QuicheConfigLogKeys = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.LogKeys);
            QuicheConfigEnableEarlyData = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.EnableEarlyData);
            QuicheConfigSetApplicationProtocols = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, ulong, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetApplicationProtocols);
            QuicheConfigSetMaxIdleTimeout = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetMaxIdleTimeout);
            QuicheConfigSetMaxRecvUdpPayloadSize = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetMaxRecvUdpPayloadSize);
            QuicheConfigSetMaxSendUdpPayloadSize = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetMaxSendUdpPayloadSize);
            QuicheConfigSetInitialMaxData = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetInitialMaxData);
            QuicheConfigSetInitialMaxStreamDataBidiLocal = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetInitialMaxStreamDataBidiLocal);
            QuicheConfigSetInitialMaxStreamDataBidiRemote = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetInitialMaxStreamDataBidiRemote);
            QuicheConfigSetInitialMaxStreamDataUni = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetInitialMaxStreamDataUni);
            QuicheConfigSetInitialMaxStreamsBidi = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetInitialMaxStreamsBidi);
            QuicheConfigSetInitialMaxStreamsUni = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetInitialMaxStreamsUni);
            QuicheConfigSetAckDelayExponent = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetAckDelayExponent);
            QuicheConfigSetMaxAckDelay = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetMaxAckDelay);
            QuicheConfigSetDisableActiveMigration = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetDisableActiveMigration);
            QuicheConfigSetCcAlgorithm = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, int, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetCcAlgorithm);
            QuicheConfigEnableHystart = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.EnableHystart);
            QuicheConfigEnablePacing = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.EnablePacing);
            QuicheConfigSetMaxPacingRate = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetMaxPacingRate);
            QuicheConfigEnableDgram = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte, ulong, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.EnableDgram);
            QuicheConfigSetMaxConnectionWindow = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetMaxConnectionWindow);
            QuicheConfigSetMaxStreamWindow = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetMaxStreamWindow);
            QuicheConfigSetActiveConnectionIdLimit = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, ulong, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetActiveConnectionIdLimit);
            QuicheConfigSetStatelessResetToken = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, byte*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.SetStatelessResetToken);
            QuicheConfigFree = (delegate* unmanaged[Cdecl]<QuicheConfigPtr, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Config.Free);

            QuicheHeaderInfo = (delegate* unmanaged[Cdecl]<byte*, ulong, ulong, uint*, byte*, byte*, ulong*, byte*, ulong*, byte*, ulong*, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.HeaderInfo);
            QuicheAccept = (delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, QuicheConfigPtr, QuicheConnPtr>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Accept);
            QuicheConnect = (delegate* unmanaged[Cdecl]<byte*, byte*, ulong, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, QuicheConfigPtr, QuicheConnPtr>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Connect);
            QuicheNegotiateVersion = (delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, byte*, ulong, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.NegotiateVersion);
            QuicheRetry = (delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, byte*, ulong, byte*, ulong, uint, byte*, ulong, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Retry);
            QuicheVersionIsSupported = (delegate* unmanaged[Cdecl]<uint, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.VersionIsSupported);

            QuicheConnNewWithTls = (delegate* unmanaged[Cdecl]<byte*, ulong, byte*, ulong, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, QuicheConfigPtr, void*, byte, QuicheConnPtr>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.NewWithTls);
            QuicheConnSetKeylogPath = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SetKeylogPath);
            // QuicheConnSetKeylogFd = (delegate* unmanaged[Cdecl]<QuicheConnPtr, int, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SetKeylogFd);
            QuicheConnSetQlogPath = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, byte*, byte*, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SetQlogPath);
            // QuicheConnSetQlogFd = (delegate* unmanaged[Cdecl]<QuicheConnPtr, int, byte*, byte*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SetQlogFd);
            QuicheConnSetSession = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SetSession);
            QuicheConnRecv = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, QuicheRecvInfo*, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Recv);
            QuicheConnSend = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, QuicheSendInfo*, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Send);
            QuicheConnSendQuantum = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SendQuantum);

            QuicheConnStreamRecv = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte*, ulong, byte*, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Recv);
            QuicheConnStreamSend = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte*, ulong, byte, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Send);
            QuicheConnStreamPriority = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte, byte, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Priority);
            QuicheConnStreamShutdown = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, int, ulong, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Shutdown);
            QuicheConnStreamCapacity = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Capacity);
            QuicheConnStreamReadable = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Readable);
            QuicheConnStreamReadableNext = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.ReadableNext);
            QuicheConnStreamWritable = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, ulong, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Writable);
            QuicheConnStreamWritableNext = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.WritableNext);
            QuicheConnStreamFinished = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Stream.Finished);

            QuicheConnReadable = (delegate* unmanaged[Cdecl]<QuicheConnPtr, QuicheStreamIterPtr>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Readable);
            QuicheConnWritable = (delegate* unmanaged[Cdecl]<QuicheConnPtr, QuicheStreamIterPtr>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Writable);
            QuicheConnMaxSendUdpPayloadSize = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.MaxSendUdpPayloadSize);
            QuicheConnTimeoutAsNanos = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.TimeoutAsNanos);
            QuicheConnTimeoutAsMillis = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.TimeoutAsMillis);
            QuicheConnOnTimeout = (delegate* unmanaged[Cdecl]<QuicheConnPtr, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.OnTimeout);
            QuicheConnClose = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte, ulong, byte*, ulong, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Close);
            QuicheConnTraceId = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.TraceId);
            QuicheConnSourceId = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SourceId);
            QuicheConnDestinationId = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.DestinationId);
            QuicheConnApplicationProto = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.ApplicationProto);
            QuicheConnPeerCert = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.PeerCert);
            QuicheConnSession = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte**, ulong*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Session);
            QuicheConnIsEstablished = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.IsEstablished);
            QuicheConnIsInEarlyData = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.IsInEarlyData);
            QuicheConnIsReadable = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.IsReadable);
            QuicheConnIsDraining = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.IsDraining);
            QuicheConnPeerStreamsLeftBidi = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.PeerStreamsLeftBidi);
            QuicheConnPeerStreamsLeftUni = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.PeerStreamsLeftUni);
            QuicheConnIsClosed = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.IsClosed);
            QuicheConnIsTimedOut = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.IsTimedOut);
            QuicheConnPeerError = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong*, byte**, ulong*, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.PeerError);
            QuicheConnLocalError = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong*, byte**, ulong*, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.LocalError);

            QuicheStreamIterNext = (delegate* unmanaged[Cdecl]<QuicheStreamIterPtr, ulong*, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Stream.IterNext);
            QuicheStreamIterFree = (delegate* unmanaged[Cdecl]<QuicheStreamIterPtr, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Stream.IterFree);

            QuicheConnStats = (delegate* unmanaged[Cdecl]<QuicheConnPtr, QuicheStats*, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Stats.Conn);
            QuicheConnPathStats = (delegate* unmanaged[Cdecl]<QuicheConnPtr, ulong, QuichePathStats*, int>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Stats.ConnPath);
            QuicheConnIsServer = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.IsServer);

            QuicheConnDgramMaxWritableLen = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.MaxWritableLen);
            QuicheConnDgramRecvFrontLen = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.RecvFrontLen);
            QuicheConnDgramRecvQueueLen = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.RecvQueueLen);
            QuicheConnDgramRecvQueueByteSize = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.RecvQueueByteSize);
            QuicheConnDgramSendQueueLen = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.SendQueueLen);
            QuicheConnDgramSendQueueByteSize = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.SendQueueByteSize);
            QuicheConnDgramRecv = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.Recv);
            QuicheConnDgramSend = (delegate* unmanaged[Cdecl]<QuicheConnPtr, byte*, ulong, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.Send);
            QuicheConnDgramPurgeOutgoing = (delegate* unmanaged[Cdecl]<QuicheConnPtr, delegate* unmanaged[Cdecl]<byte*, ulong, byte>, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Dgram.PurgeOutgoing);
            QuicheConnSendAckEliciting = (delegate* unmanaged[Cdecl]<QuicheConnPtr, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SendAckEliciting);
            QuicheConnSendAckElicitingOnPath = (delegate* unmanaged[Cdecl]<QuicheConnPtr, SystemStructures.SockAddr*, ulong, SystemStructures.SockAddr*, ulong, long>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.SendAckElicitingOnPath);
            QuicheConnFree = (delegate* unmanaged[Cdecl]<QuicheConnPtr, void>)NativeLibrary.GetExport(quicheHandle, QuicheApiNames.Conn.Free);


            IsValid = true;
        }

    }
}
