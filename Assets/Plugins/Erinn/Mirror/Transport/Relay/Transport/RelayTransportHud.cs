//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Mirror
{
    /// <summary>
    ///     RelayTransmissionHud
    /// </summary>
    public sealed class RelayTransportHud : MonoBehaviour
    {
        /// <summary>
        ///     DeviationX
        /// </summary>
        [SerializeField] private int _offsetX;

        /// <summary>
        ///     DeviationY
        /// </summary>
        [SerializeField] private int _offsetY;

        /// <summary>
        ///     Enable server only
        /// </summary>
        [SerializeField] private bool _enableServerOnly;

        /// <summary>
        ///     HostId
        /// </summary>
        private string _hostId;

        /// <summary>
        ///     Password
        /// </summary>
        private string _password;

        /// <summary>
        ///     Manager
        /// </summary>
        private NetworkManager _manager;

        /// <summary>
        ///     Transmission
        /// </summary>
        private RelayTransport _relayTransport;

        /// <summary>
        ///     AwakeWhen calling
        /// </summary>
        private void Awake() => _manager = GetComponent<NetworkManager>();

        /// <summary>
        ///     StartWhen calling
        /// </summary>
        private void Start()
        {
            _relayTransport = RelayTransport.Singleton;
            _hostId = _relayTransport.HostId.ToString();
            _password = _relayTransport.Password.ToString();
        }

        /// <summary>
        ///     GUI
        /// </summary>
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10 + _offsetX, 40 + _offsetY, 300, 9999));
            if (!NetworkClient.isConnected && !NetworkServer.active)
                StartButtons();
            else
                StatusLabels();
            if (NetworkClient.isConnected && !NetworkClient.ready)
            {
                if (GUILayout.Button("Client ready"))
                {
                    NetworkClient.Ready();
                    if (NetworkClient.localPlayer == null)
                        NetworkClient.AddPlayer();
                }
            }

            StopButtons();
            GUILayout.EndArea();
        }

        /// <summary>
        ///     Start-up
        /// </summary>
        private void StartButtons()
        {
            if (!NetworkClient.active)
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
                        _relayTransport.HostId.Set(roomHostId);
                    }
                }

                if (uint.TryParse(GUILayout.TextField(_password), out var password))
                {
                    var passwordString = password.ToString();
                    if (_password != passwordString)
                    {
                        _password = passwordString;
                        _relayTransport.Password = password;
                    }
                }

                GUILayout.EndHorizontal();
                if (!_enableServerOnly)
                    return;
                if (!GUILayout.Button("Server only"))
                    return;
                _manager.StartServer();
            }
            else
            {
                GUILayout.Label($"Joining room {_relayTransport.HostId}..");
                if (GUILayout.Button("Cancel connection attempt"))
                    _manager.StopClient();
            }
        }

        /// <summary>
        ///     State
        /// </summary>
        private void StatusLabels()
        {
            switch (NetworkServer.active)
            {
                case true when NetworkClient.active:
                    GUILayout.Label($"<b>Host</b>: {_relayTransport.LocalId}");
                    break;
                case true:
                    GUILayout.Label($"<b>Server</b>: {_relayTransport.LocalId}");
                    break;
                default:
                    if (NetworkClient.isConnected)
                    {
                        GUILayout.Label($"<b>Master</b>: {_relayTransport.HostId}");
                        GUILayout.Label($"<b>Client</b>: {NetworkClient.LocalClientId}");
                    }

                    break;
            }
        }

        /// <summary>
        ///     Cease
        /// </summary>
        private void StopButtons()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Stop host"))
                    _manager.StopHost();
                if (GUILayout.Button("Stop client"))
                    _manager.StopClient();
                GUILayout.EndHorizontal();
            }
            else if (NetworkClient.isConnected)
            {
                if (GUILayout.Button("Stop client"))
                    _manager.StopClient();
            }
            else if (NetworkServer.active)
            {
                if (GUILayout.Button("Stop server"))
                    _manager.StopServer();
            }
        }
    }
}