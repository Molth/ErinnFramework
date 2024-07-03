//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Network Discovery ComponentsHud
    /// </summary>
    [RequireComponent(typeof(NetworkDiscovery))]
    public sealed class NetworkDiscoveryHud : MonoBehaviour
    {
        /// <summary>
        ///     Deviation
        /// </summary>
        [SerializeField] private Vector2 _drawOffset = new(10, 210);

        /// <summary>
        ///     Discovered servers
        /// </summary>
        private readonly Dictionary<string, DiscoveryResponseData> _discoveredServers = new();

        /// <summary>
        ///     Find
        /// </summary>
        private NetworkDiscovery _discovery;

        /// <summary>
        ///     Call on load
        /// </summary>
        private void Awake()
        {
            _discovery = GetComponent<NetworkDiscovery>();
            _discovery.OnServerFound += OnServerFound;
        }

        /// <summary>
        ///     Call upon destruction
        /// </summary>
        private void OnDestroy() => _discovery.OnServerFound -= OnServerFound;

        /// <summary>
        ///     GUI
        /// </summary>
        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(_drawOffset, new Vector2(200, 600)));
            if (NetcodeSystem.IsServer)
                ServerControlsGUI();
            else
                ClientSearchGUI();
            GUILayout.EndArea();
        }

        /// <summary>
        ///     Discovery server
        /// </summary>
        private void OnServerFound(IPEndPoint sender, DiscoveryResponseData response) => _discoveredServers[response.Address] = response;

        /// <summary>
        ///     Client Discovery
        /// </summary>
        private void ClientSearchGUI()
        {
            if (_discovery.IsRunning)
            {
                if (GUILayout.Button("Stop Client Discovery"))
                {
                    _discovery.StopDiscovery();
                    _discoveredServers.Clear();
                }

                if (GUILayout.Button("Refresh List"))
                {
                    _discoveredServers.Clear();
                    _discovery.ClientBroadcast();
                }

                GUILayout.Space(40);
                foreach (var discoveredServer in _discoveredServers.Values)
                {
                    if (GUILayout.Button($"{discoveredServer.ServerName}[{discoveredServer.Address}]"))
                    {
                        _discovery.StopDiscovery();
                        _discoveredServers.Clear();
                        NetcodeSystem.SetConnectionData(discoveredServer.Address, discoveredServer.Port);
                        NetcodeClient.StartClient();
                    }
                }
            }
            else
            {
                if (GUILayout.Button("Discover Servers"))
                {
                    _discovery.StartDiscovery();
                    _discovery.ClientBroadcast();
                }
            }
        }

        /// <summary>
        ///     Server Control
        /// </summary>
        private void ServerControlsGUI()
        {
            if (_discovery.IsRunning)
            {
                if (GUILayout.Button("Stop Server Discovery"))
                    _discovery.StopDiscovery();
            }
            else
            {
                if (GUILayout.Button("Start Server Discovery"))
                    _discovery.AdviseServer();
            }
        }
    }
}