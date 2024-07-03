//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public static partial class NetcodeServer
    {
        /// <summary>
        ///     Network Manager
        /// </summary>
        private static NetworkManager NetManager => NetworkManager.Singleton;

        /// <summary>
        ///     Listening
        /// </summary>
        private static bool IsListening => NetManager.IsListening;

        /// <summary>
        ///     Is it a server
        /// </summary>
        private static bool IsServer => NetManager.IsServer;

        /// <summary>
        ///     Number of clients in the connection
        /// </summary>
        public static int ConnectedClientsCount => NetManager.ConnectedClients.Count;

        /// <summary>
        ///     Current connection status
        /// </summary>
        public static NetcodeConnectState CurrentConnectState => GetCurrentConnectState();

        /// <summary>
        ///     Get current connection status
        /// </summary>
        private static NetcodeConnectState GetCurrentConnectState()
        {
            if (NetManager.IsHost)
                return NetcodeConnectState.Host;
            if (IsServer)
                return NetcodeConnectState.Server;
            return NetcodeConnectState.None;
        }

        /// <summary>
        ///     Start server
        /// </summary>
        public static void StartServer()
        {
            if (IsListening)
                return;
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        public static void StartServer(string ipv4Address)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        public static void StartServer(string ipv4Address, ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address, port);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Listening address</param>
        public static void StartServer(string ipv4Address, ushort port, string listenAddress)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address, port, listenAddress);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start LAN server
        /// </summary>
        public static void StartLobbyServer()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData();
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start LAN server
        /// </summary>
        /// <param name="port">Server Port</param>
        public static void StartLobbyServer(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start LAN server
        /// </summary>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Server listening address</param>
        public static void StartLobbyServer(ushort port, string listenAddress)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port, listenAddress);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start local server
        /// </summary>
        public static void StartLocalServer()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData();
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start local server
        /// </summary>
        /// <param name="port">Server Port</param>
        public static void StartLocalServer(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData(port);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start local server
        /// </summary>
        /// <param name="isOffline">Do you want to close the connection</param>
        public static void StartLocalServer(bool isOffline)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData();
            NetManager.StartServer();
            if (isOffline)
                Utp.Shutdown();
        }

        /// <summary>
        ///     Start local server
        /// </summary>
        /// <param name="port">Server Port</param>
        /// <param name="isOffline">Do you want to close the connection</param>
        public static void StartLocalServer(ushort port, bool isOffline)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData(port);
            NetManager.StartServer();
            if (isOffline)
                Utp.Shutdown();
        }

        /// <summary>
        ///     Start local offline server
        /// </summary>
        public static void StartLocalOfflineServer()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData();
            NetManager.StartServer();
            Utp.Shutdown();
        }

        /// <summary>
        ///     Start local offline server
        /// </summary>
        /// <param name="port">Server Port</param>
        public static void StartLocalOfflineServer(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData(port);
            NetManager.StartServer();
            Utp.Shutdown();
        }

        /// <summary>
        ///     Start remote server
        /// </summary>
        public static void StartRemoteServer()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData();
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start remote server
        /// </summary>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Server listening address</param>
        public static void StartRemoteServer(ushort port, string listenAddress)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port, listenAddress);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Start remote server
        /// </summary>
        /// <param name="port">Server Port</param>
        public static void StartRemoteServer(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port);
            NetManager.StartServer();
        }

        /// <summary>
        ///     Stop server
        /// </summary>
        public static void StopServer() => NetManager.Shutdown();

        /// <summary>
        ///     Stop server
        /// </summary>
        /// <param name="discardMessageQueue">Immediately disconnect</param>
        public static void StopServer(bool discardMessageQueue) => NetManager.Shutdown(discardMessageQueue);

        /// <summary>
        ///     Starting the host
        /// </summary>
        public static void StartHost()
        {
            if (IsListening)
                return;
            NetManager.StartHost();
        }

        /// <summary>
        ///     Starting the host
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        public static void StartHost(string ipv4Address)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Starting the host
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        public static void StartHost(string ipv4Address, ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address, port);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Starting the host
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Listening address</param>
        public static void StartHost(string ipv4Address, ushort port, string listenAddress)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address, port, listenAddress);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start LAN host
        /// </summary>
        public static void StartLobbyHost()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData();
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start LAN host
        /// </summary>
        /// <param name="port">Server Port</param>
        public static void StartLobbyHost(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start LAN host
        /// </summary>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Server listening address</param>
        public static void StartLobbyHost(ushort port, string listenAddress)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port, listenAddress);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start local host
        /// </summary>
        public static void StartLocalHost()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData();
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start local host
        /// </summary>
        /// <param name="port">Port</param>
        public static void StartLocalHost(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData(port);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start local offline host
        /// </summary>
        /// <param name="isOffline">Do you want to close the connection</param>
        public static void StartLocalHost(bool isOffline)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData();
            NetManager.StartHost();
            if (isOffline)
                Utp.Shutdown();
        }

        /// <summary>
        ///     Start local offline host
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="isOffline">Do you want to close the connection</param>
        public static void StartLocalHost(ushort port, bool isOffline)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData(port);
            NetManager.StartHost();
            if (isOffline)
                Utp.Shutdown();
        }

        /// <summary>
        ///     Start local offline host
        /// </summary>
        public static void StartLocalOfflineHost()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData();
            NetManager.StartHost();
            Utp.Shutdown();
        }

        /// <summary>
        ///     Start local offline host
        /// </summary>
        /// <param name="port">Port</param>
        public static void StartLocalOfflineHost(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData(port);
            NetManager.StartHost();
            Utp.Shutdown();
        }

        /// <summary>
        ///     Start remote host
        /// </summary>
        public static void StartRemoteHost()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData();
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start remote host
        /// </summary>
        /// <param name="port">Server Port</param>
        public static void StartRemoteHost(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Start remote host
        /// </summary>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Server listening address</param>
        public static void StartRemoteHost(ushort port, string listenAddress)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetRemoteConnectionData(port, listenAddress);
            NetManager.StartHost();
        }

        /// <summary>
        ///     Stop host
        /// </summary>
        public static void StopHost() => NetManager.Shutdown();

        /// <summary>
        ///     Stop host
        /// </summary>
        /// <param name="discardMessageQueue">Immediately disconnect</param>
        public static void StopHost(bool discardMessageQueue) => NetManager.Shutdown(discardMessageQueue);

        /// <summary>
        ///     Disconnect client connection
        /// </summary>
        /// <param name="clientId">Client Index</param>
        /// <returns>Is the disconnection successful</returns>
        public static bool DisconnectClient(ulong clientId)
        {
            if (!IsServer)
                return false;
            if (clientId == 0)
                return false;
            if (!NetManager.ConnectedClients.ContainsKey(clientId))
                return false;
            NetManager.DisconnectClient(clientId);
            return true;
        }

        /// <summary>
        ///     Disconnect client connection
        /// </summary>
        /// <param name="clientId">Client Index</param>
        /// <param name="reason">Reason</param>
        /// <returns>Is the disconnection successful</returns>
        public static bool DisconnectClient(ulong clientId, string reason)
        {
            if (!IsServer)
                return false;
            if (clientId == 0)
                return false;
            if (!NetManager.ConnectedClients.ContainsKey(clientId))
                return false;
            NetManager.DisconnectClient(clientId, reason);
            return true;
        }
    }
}