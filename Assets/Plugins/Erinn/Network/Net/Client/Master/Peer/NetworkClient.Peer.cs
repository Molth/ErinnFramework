//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using UnityEngine;
#endif

#if GODOT
using Godot;
#endif

#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed partial class NetworkClient
    {
        /// <summary>
        ///     Client Peer
        /// </summary>
        private NetworkClientPeer _peer;

        /// <summary>
        ///     Initialize client
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
                    _peer = new ENetClientPeer(_messageChannel);
                    break;
                case NetworkProtocolType.Tcp:
                    _peer = new OnryoClientPeer(_messageChannel);
                    break;
                case NetworkProtocolType.WebSocket:
#if UNITY_2021_3_OR_NEWER
                    if (Application.platform == RuntimePlatform.WebGLPlayer && !Application.isEditor)
                        _peer = new WebGLWebSocketClientPeer(_messageChannel);
                    else
                        _peer = new WebSocketClientPeer(_messageChannel);
#elif GODOT
                    if (OS.GetName() == "Web" && !Engine.IsEditorHint())
                        _peer = new WebGLWebSocketClientPeer(_messageChannel);
                    else
                        _peer = new WebSocketClientPeer(_messageChannel);
#else
                    _peer = new WebSocketClientPeer(_messageChannel);
#endif
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
        private void OnConnected() => OnConnectedCallback?.Invoke();

        /// <summary>
        ///     Disconnected callback
        /// </summary>
        private void OnDisconnected() => OnDisconnectedCallback?.Invoke();

#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Called upon application exit
        /// </summary>
        /// <param name="sender">Sender </param>
        /// <param name="args">Parameter</param>
        private void OnApplicationQuit(object? sender, EventArgs args) => _peer?.Shutdown();
#endif
    }
}