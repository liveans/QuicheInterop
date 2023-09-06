using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuicheInterop
{
    internal static class QuicheApiNames
    {
        internal static string Version = "quiche_version";
        internal static string EnableDebugLogging = "quiche_enable_debug_logging";

        internal static class Config
        {
            internal static string ConfigurationNew = "quiche_config_new";
            internal static string LoadCertChainFromPemFile = "quiche_config_load_cert_chain_from_pem_file";
            internal static string LoadPrivKeyFromPemFile = "quiche_config_load_priv_key_from_pem_file";
            internal static string LoadVerifyLocationsFromFile = "quiche_config_load_verify_locations_from_file";
            internal static string LoadVerifyLocationsFromDirectory = "quiche_config_load_verify_locations_from_directory";
            internal static string VerifyPeer = "quiche_config_verify_peer";
            internal static string Grease = "quiche_config_grease";
            internal static string LogKeys = "quiche_config_log_keys";
            internal static string EnableEarlyData = "quiche_config_enable_early_data";
            internal static string SetApplicationProtocols = "quiche_config_set_application_protos";
            internal static string SetMaxIdleTimeout = "quiche_config_set_max_idle_timeout";
            internal static string SetMaxRecvUdpPayloadSize = "quiche_config_set_max_recv_udp_payload_size";
            internal static string SetMaxSendUdpPayloadSize = "quiche_config_set_max_send_udp_payload_size";
            internal static string SetInitialMaxData = "quiche_config_set_initial_max_data";
            internal static string SetInitialMaxStreamDataBidiLocal = "quiche_config_set_initial_max_stream_data_bidi_local";
            internal static string SetInitialMaxStreamDataBidiRemote = "quiche_config_set_initial_max_stream_data_bidi_remote";
            internal static string SetInitialMaxStreamDataUni = "quiche_config_set_initial_max_stream_data_uni";
            internal static string SetInitialMaxStreamsBidi = "quiche_config_set_initial_max_streams_bidi";
            internal static string SetInitialMaxStreamsUni = "quiche_config_set_initial_max_streams_uni";
            internal static string SetAckDelayExponent = "quiche_config_set_ack_delay_exponent";
            internal static string SetMaxAckDelay = "quiche_config_set_max_ack_delay";
            internal static string SetDisableActiveMigration = "quiche_config_set_disable_active_migration";
            internal static string SetCcAlgorithm = "quiche_config_set_cc_algorithm";
            internal static string EnableHystart = "quiche_config_enable_hystart";
            internal static string EnablePacing = "quiche_config_enable_pacing";
            internal static string SetMaxPacingRate = "quiche_config_set_max_pacing_rate";
            internal static string EnableDgram = "quiche_config_enable_dgram";
            internal static string SetMaxConnectionWindow = "quiche_config_set_max_connection_window";
            internal static string SetMaxStreamWindow = "quiche_config_set_max_stream_window";
            internal static string SetActiveConnectionIdLimit = "quiche_config_set_active_connection_id_limit";
            internal static string SetStatelessResetToken = "quiche_config_set_stateless_reset_token";
            internal static string Free = "quiche_config_free";

        }
        internal static string HeaderInfo = "quiche_header_info";
        internal static string Accept = "quiche_accept";
        internal static string Connect = "quiche_connect";
        internal static string NegotiateVersion = "quiche_negotiate_version";
        internal static string Retry = "quiche_retry";
        internal static string VersionIsSupported = "quiche_version_is_supported";
        internal static class Conn
        {
            internal static string NewWithTls = "quiche_conn_new_with_tls";
            internal static string SetKeylogPath = "quiche_conn_set_keylog_path";
            internal static string SetKeylogFd = "quiche_conn_set_keylog_fd";
            internal static string SetQlogPath = "quiche_conn_set_qlog_path";
            internal static string SetQlogFd = "quiche_conn_set_qlog_fd";
            internal static string SetSession = "quiche_conn_set_session";
            internal static string Recv = "quiche_conn_recv";
            internal static string Send = "quiche_conn_send";
            internal static string SendQuantum = "quiche_conn_send_quantum";
            internal static class Stream
            {
                internal static string Recv = "quiche_conn_stream_recv";
                internal static string Send = "quiche_conn_stream_send";
                internal static string Priority = "quiche_conn_stream_priority";
                internal static string Shutdown = "quiche_conn_stream_shutdown";
                internal static string Capacity = "quiche_conn_stream_capacity";
                internal static string Readable = "quiche_conn_stream_readable";
                internal static string ReadableNext = "quiche_conn_stream_readable_next";
                internal static string Writable = "quiche_conn_stream_writable";
                internal static string WritableNext = "quiche_conn_stream_writable_next";
                internal static string Finished = "quiche_conn_stream_finished";
            }
            internal static string Readable = "quiche_conn_readable";
            internal static string Writable = "quiche_conn_writable";
            internal static string MaxSendUdpPayloadSize = "quiche_conn_max_send_udp_payload_size";
            internal static string TimeoutAsNanos = "quiche_conn_timeout_as_nanos";
            internal static string TimeoutAsMillis = "quiche_conn_timeout_as_millis";
            internal static string OnTimeout = "quiche_conn_on_timeout";
            internal static string Close = "quiche_conn_close";
            internal static string TraceId = "quiche_conn_trace_id";
            internal static string SourceId = "quiche_conn_source_id";
            internal static string DestinationId = "quiche_conn_destination_id";
            internal static string ApplicationProto = "quiche_conn_application_proto";
            internal static string PeerCert = "quiche_conn_peer_cert";
            internal static string Session = "quiche_conn_session";
            internal static string IsEstablished = "quiche_conn_is_established";
            internal static string IsInEarlyData = "quiche_conn_is_in_early_data";
            internal static string IsReadable = "quiche_conn_is_readable";
            internal static string IsDraining = "quiche_conn_is_draining";
            internal static string PeerStreamsLeftBidi = "quiche_conn_peer_streams_left_bidi";
            internal static string PeerStreamsLeftUni = "quiche_conn_peer_streams_left_uni";
            internal static string IsClosed = "quiche_conn_is_closed";
            internal static string IsTimedOut = "quiche_conn_is_timed_out";
            internal static string PeerError = "quiche_conn_peer_error";
            internal static string LocalError = "quiche_conn_local_error";
            internal static class Dgram
            {
                internal static string MaxWritableLen = "quiche_conn_dgram_max_writable_len";
                internal static string RecvFrontLen = "quiche_conn_dgram_recv_front_len";
                internal static string RecvQueueLen = "quiche_conn_dgram_recv_queue_len";
                internal static string RecvQueueByteSize = "quiche_conn_dgram_recv_queue_byte_size";
                internal static string SendQueueLen = "quiche_conn_dgram_send_queue_len";
                internal static string SendQueueByteSize = "quiche_conn_dgram_send_queue_byte_size";
                internal static string Recv = "quiche_conn_dgram_recv";
                internal static string Send = "quiche_conn_dgram_send";
                internal static string PurgeOutgoing = "quiche_conn_dgram_purge_outgoing";
            }
            internal static string SendAckEliciting = "quiche_conn_send_ack_eliciting";
            internal static string SendAckElicitingOnPath = "quiche_conn_send_ack_eliciting_on_path";
            internal static string Free = "quiche_conn_free";
            internal static string IsServer = "quiche_conn_is_server";
        }
        internal static class Stream
        {
            internal static string IterNext = "quiche_stream_iter_next";
            internal static string IterFree = "quiche_stream_iter_free";

        }
        internal static class Stats
        { 
            internal static string Conn = "quiche_conn_stats";
            internal static string ConnPath = "quiche_conn_path_stats";
        }
    }
}
