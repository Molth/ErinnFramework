//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Online system
    /// </summary>
    public static partial class NetcodeSystem
    {
        /// <summary>
        ///     Device identification code
        /// </summary>
        public static readonly ulong UniqueIdentifier = MathV.HashU64(SystemInfo.deviceUniqueIdentifier);

        /// <summary>
        ///     Network Manager
        /// </summary>
        private static NetworkManager NetManager => NetworkManager.Singleton;

        /// <summary>
        ///     Listening
        /// </summary>
        public static bool IsListening => NetManager.IsListening;

        /// <summary>
        ///     Is it a client
        /// </summary>
        public static bool IsClient => NetManager.IsClient;

        /// <summary>
        ///     Whether to connect to the client of the server
        /// </summary>
        public static bool IsConnectedClient => NetManager.IsConnectedClient;

        /// <summary>
        ///     Is it a server
        /// </summary>
        public static bool IsServer => NetManager.IsServer;

        /// <summary>
        ///     Is the host
        /// </summary>
        public static bool IsHost => NetManager.IsHost;

        /// <summary>
        ///     Is it valid
        /// </summary>
        public static bool IsActive => IsServer || IsConnectedClient;

        /// <summary>
        ///     UnityTransmitters
        /// </summary>
        private static UnityTransport Utp => (UnityTransport)NetManager.NetworkConfig.NetworkTransport;

        /// <summary>
        ///     Connection address
        /// </summary>
        public static string Address => Utp.ConnectionData.Address;

        /// <summary>
        ///     Connection port
        /// </summary>
        public static ushort Port => Utp.ConnectionData.Port;

        /// <summary>
        ///     Server listening address
        /// </summary>
        public static string ServerListenAddress => Utp.ConnectionData.ServerListenAddress;

        /// <summary>
        ///     Protocol version
        /// </summary>
        public static ushort ProtocolVersion => Config.ProtocolVersion;

        /// <summary>
        ///     RpcHash capacity
        /// </summary>
        public static HashSize RpcHashSize => Config.RpcHashSize;

        /// <summary>
        ///     Transmission frequency
        /// </summary>
        public static uint TickRate => Config.TickRate;

        /// <summary>
        ///     Transmission interval
        /// </summary>
        public static float TickRateInterval => 1f / TickRate;

        /// <summary>
        ///     Network time system
        /// </summary>
        private static NetworkTimeSystem NetTimeSystem => NetManager.NetworkTimeSystem;

        /// <summary>
        ///     Local time
        /// </summary>
        public static double LocalTime => NetTimeSystem.LocalTime;

        /// <summary>
        ///     Server time
        /// </summary>
        public static double ServerTime => NetTimeSystem.ServerTime;

        /// <summary>
        ///     Current connection status
        /// </summary>
        public static NetcodeConnectState CurrentConnectState => GetCurrentConnectState();

        /// <summary>
        ///     Current online mode
        /// </summary>
        public static NetcodeMode CurrentMode => GetCurrentMode();

        /// <summary>
        ///     Get current connection status
        /// </summary>
        private static NetcodeConnectState GetCurrentConnectState()
        {
            if (IsHost)
                return NetcodeConnectState.Host;
            if (IsServer)
                return NetcodeConnectState.Server;
            if (IsConnectedClient)
                return NetcodeConnectState.ConnectedClient;
            if (IsClient)
                return NetcodeConnectState.Client;
            return NetcodeConnectState.None;
        }

        /// <summary>
        ///     Get the current online mode
        /// </summary>
        private static NetcodeMode GetCurrentMode()
        {
            if (IsHost)
                return NetcodeMode.Host;
            if (IsServer)
                return NetcodeMode.ServerOnly;
            if (IsClient)
                return NetcodeMode.ClientOnly;
            return NetcodeMode.Offline;
        }

        /// <summary>
        ///     Get connection data
        /// </summary>
        /// <returns>Obtained connection data</returns>
        public static ConnectionData GetConnectionData() => new(Utp.ConnectionData);

        /// <summary>
        ///     Set connection data
        /// </summary>
        /// <param name="connectionData">Connect data</param>
        public static void SetConnectionData(ConnectionData connectionData) => Utp.SetConnectionData(connectionData.Address, connectionData.Port);

        /// <summary>
        ///     Set connection data
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        public static void SetConnectionData(string ipv4Address) => Utp.SetConnectionData(ipv4Address, Port, ServerListenAddress);

        /// <summary>
        ///     Set connection data
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        public static void SetConnectionData(string ipv4Address, ushort port) => Utp.SetConnectionData(ipv4Address, port, ServerListenAddress);

        /// <summary>
        ///     Set connection data
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Server listening address</param>
        public static void SetConnectionData(string ipv4Address, ushort port, string listenAddress) => Utp.SetConnectionData(ipv4Address, port, listenAddress);

        /// <summary>
        ///     Set up offline localIpv4Connect data
        /// </summary>
        public static void SetLocalConnectionData() => SetConnectionData("127.0.0.1", Port, "127.0.0.1");

        /// <summary>
        ///     Set up offline localIpv4Connect data
        /// </summary>
        /// <param name="port">Port</param>
        public static void SetLocalConnectionData(ushort port) => SetConnectionData("127.0.0.1", port, "127.0.0.1");

        /// <summary>
        ///     Set remote connection data
        /// </summary>
        public static void SetRemoteConnectionData() => SetConnectionData("0.0.0.0", Port, null);

        /// <summary>
        ///     Set remote connection data
        /// </summary>
        /// <param name="port">Port</param>
        public static void SetRemoteConnectionData(ushort port) => SetConnectionData("0.0.0.0", port, null);

        /// <summary>
        ///     Set remote connection data
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="listenAddress">Server listening address</param>
        public static void SetRemoteConnectionData(ushort port, string listenAddress) => SetConnectionData("0.0.0.0", port, listenAddress);
    }
}