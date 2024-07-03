//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public sealed partial class NetworkServer
    {
        /// <summary>
        ///     Server Peer
        /// </summary>
        private NetworkServerPeer _peer;

        /// <summary>
        ///     Initialize server
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        public bool Initialize(NetworkProtocolType networkProtocolType)
        {
            if (_peer != null)
            {
                if (_peer.ProtocolType == networkProtocolType)
                    return true;
                Log.Warning($"Already initialized: [{_peer.ProtocolType}]");
                return false;
            }

            return Create(networkProtocolType);
        }

        /// <summary>
        ///     Create Peer
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        /// <returns>Successfully created</returns>
        private bool Create(NetworkProtocolType networkProtocolType)
        {
            switch (networkProtocolType)
            {
                case NetworkProtocolType.Udp:
                    _peer = new ENetServerPeer(_messageChannel);
                    break;
                case NetworkProtocolType.Tcp:
                    _peer = new OnryoServerPeer(_messageChannel);
                    break;
                case NetworkProtocolType.WebSocket:
                    _peer = new WebSocketServerPeer(_messageChannel);
                    break;
                default:
                    Log.Warning("Unsupported type");
                    return false;
            }
#if !UNITY_2021_3_OR_NEWER && !GODOT
            Console.CancelKeyPress += OnApplicationQuit;
#endif
            _peer.OnConnectedCallback += OnConnected;
            _peer.OnDisconnectedCallback += OnDisconnected;
            return true;
        }

        /// <summary>
        ///     Release
        /// </summary>
        public void Destroy()
        {
#if !UNITY_2021_3_OR_NEWER && !GODOT
            Console.CancelKeyPress -= OnApplicationQuit;
#endif
            _peer?.Shutdown();
            _peer = null;
        }

        /// <summary>
        ///     Connected callback
        /// </summary>
        /// <param name="id">ClientId</param>
        private void OnConnected(uint id) => OnConnectedCallback?.Invoke(id);

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        /// <param name="id">ClientId</param>
        private void OnDisconnected(uint id) => OnDisconnectedCallback?.Invoke(id);

#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Called upon application exit
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Parameter</param>
        private void OnApplicationQuit(object? sender, EventArgs args) => _peer?.Shutdown();
#endif
    }
}