//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public static partial class NetcodeClient
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
        ///     Is it a client
        /// </summary>
        private static bool IsClient => NetManager.IsClient;

        /// <summary>
        ///     Local client index
        /// </summary>
        public static ulong LocalClientId => NetManager.LocalClientId;

        /// <summary>
        ///     Reason for disconnection
        /// </summary>
        public static string DisconnectReason => NetManager.DisconnectReason;

        /// <summary>
        ///     Current connection status
        /// </summary>
        public static NetcodeConnectState CurrentConnectState => GetCurrentConnectState();

        /// <summary>
        ///     Get current connection status
        /// </summary>
        private static NetcodeConnectState GetCurrentConnectState()
        {
            if (NetManager.IsConnectedClient)
                return NetcodeConnectState.ConnectedClient;
            if (IsClient)
                return NetcodeConnectState.Client;
            return NetcodeConnectState.None;
        }

        /// <summary>
        ///     Start client
        /// </summary>
        public static void StartClient()
        {
            if (IsListening)
                return;
            NetManager.StartClient();
        }

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        public static void StartClient(string ipv4Address)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address);
            NetManager.StartClient();
        }

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        public static void StartClient(string ipv4Address, ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address, port);
            NetManager.StartClient();
        }

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipv4Address">Server address</param>
        /// <param name="port">Server Port</param>
        /// <param name="listenAddress">Listening address</param>
        public static void StartClient(string ipv4Address, ushort port, string listenAddress)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetConnectionData(ipv4Address, port, listenAddress);
            NetManager.StartClient();
        }

        /// <summary>
        ///     Start local client
        /// </summary>
        public static void StartLocalClient()
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData();
            NetManager.StartClient();
        }

        /// <summary>
        ///     Start local client
        /// </summary>
        /// <param name="port">Port</param>
        public static void StartLocalClient(ushort port)
        {
            if (IsListening)
                return;
            NetcodeSystem.SetLocalConnectionData(port);
            NetManager.StartClient();
        }

        /// <summary>
        ///     Stop client
        /// </summary>
        public static void StopClient() => NetManager.Shutdown();

        /// <summary>
        ///     Stop client
        /// </summary>
        /// <param name="discardMessageQueue">Immediately disconnect</param>
        public static void StopClient(bool discardMessageQueue) => NetManager.Shutdown(discardMessageQueue);
    }
}