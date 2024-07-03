//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     LobbyTransmissionHud
    /// </summary>
    public sealed class LobbyTransportHud : MonoBehaviour
    {
        /// <summary>
        ///     x offset
        /// </summary>
        [SerializeField] private int _offsetX;

        /// <summary>
        ///     y offset
        /// </summary>
        [SerializeField] private int _offsetY;

        /// <summary>
        ///     Enable server only
        /// </summary>
        [SerializeField] private bool _enableServerOnly;

        /// <summary>
        ///     Master ClientId
        /// </summary>
        private string _hostId;

        /// <summary>
        ///     Password
        /// </summary>
        private string _password;

        /// <summary>
        ///     Network Manager
        /// </summary>
        private NetworkManager _manager;

        /// <summary>
        ///     Transmission
        /// </summary>
        private LobbyTransport _transport;

        /// <summary>
        ///     Call on load
        /// </summary>
        private void Start()
        {
            _manager = GetComponent<NetworkManager>();
            _transport = LobbyTransport.Singleton;
            _hostId = _transport.HostId.ToString();
            _password = _transport.Password.ToString();
        }

        /// <summary>
        ///     Displayed onGUI
        /// </summary>
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10 + _offsetX, 40 + _offsetY, 250, 400));
            if (!_manager.IsServer && !_manager.IsConnectedClient)
                StartButtons();
            else
                StatusLabels();
            GUILayout.EndArea();
        }

        /// <summary>
        ///     Start button
        /// </summary>
        private void StartButtons()
        {
            if (_manager.IsClient)
            {
                GUILayout.Label($"Joining room {_transport.HostId}");
                if (GUILayout.Button("Cancel connection attempt"))
                    _manager.Shutdown();
            }
            else
            {
                if (GUILayout.Button("Host"))
                    _manager.StartHost();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Client"))
                    _manager.StartClient();
                if (ushort.TryParse(GUILayout.TextField(_hostId), out var roomHostId))
                {
                    var hostIdString = roomHostId.ToString();
                    if (_hostId != hostIdString)
                    {
                        _hostId = hostIdString;
                        _transport.HostId.Set(roomHostId);
                    }
                }

                if (uint.TryParse(GUILayout.TextField(_password), out var password))
                {
                    var passwordString = password.ToString();
                    if (_password != passwordString)
                    {
                        _password = passwordString;
                        _transport.Password = password;
                    }
                }

                GUILayout.EndHorizontal();
                if (!_enableServerOnly)
                    return;
                if (!GUILayout.Button("Server only"))
                    return;
                _manager.StartServer();
            }
        }

        /// <summary>
        ///     Status bar
        /// </summary>
        private void StatusLabels()
        {
            if (_manager.IsServer && _manager.IsConnectedClient)
            {
                GUILayout.Label($"<b>Host</b>: {_transport.LocalId}");
                if (GUILayout.Button("Stop host"))
                    _manager.Shutdown();
            }
            else if (_manager.IsConnectedClient)
            {
                GUILayout.Label($"<b>Master</b>: {_transport.HostId}");
                GUILayout.Label($"<b>Client</b>: {NetworkManager.Singleton.LocalClientId}");
                if (GUILayout.Button("Stop client"))
                    _manager.Shutdown();
            }
            else if (_manager.IsServer)
            {
                GUILayout.Label($"<b>Server side</b>: {_transport.LocalId}");
                if (GUILayout.Button("Stop server"))
                    _manager.Shutdown();
            }
        }
    }
}