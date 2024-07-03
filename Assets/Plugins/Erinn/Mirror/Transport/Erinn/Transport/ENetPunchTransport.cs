//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Erinn;
using UnityEngine;

namespace Mirror
{
    /// <summary>
    ///     Transmission
    /// </summary>
    public sealed class ENetPunchTransport : Transport, IClientRPCListener, IPortTransport
    {
        /// <summary>
        ///     Address
        /// </summary>
        [Header("Address")] public string Address = "127.0.0.1";

        /// <summary>
        ///     Port
        /// </summary>
        [Header("Port")] [SerializeField] private ushort _port = 7777;

        /// <summary>
        ///     Maximum connection
        /// </summary>
        [Header("Maximum connection")] public uint MaxClients = 4U;

        /// <summary>
        ///     Address
        /// </summary>
        [Header("Address")] public string HostAddress = "127.0.0.1";

        /// <summary>
        ///     Port
        /// </summary>
        [Header("Port")] public ushort HostPort = 7778;

        /// <summary>
        ///     Client
        /// </summary>
        private ENetPunchClientPeer _client;

        /// <summary>
        ///     Server
        /// </summary>
        private ENetPunchServerPeer _server;

        /// <summary>
        ///     Singleton
        /// </summary>
        public static ENetPunchTransport Singleton { get; private set; }

        /// <summary>
        ///     Awake When calling
        /// </summary>
        private void Awake() => Singleton = this;

        /// <summary>
        ///     Start When calling
        /// </summary>
        private void Start()
        {
            _client = new ENetPunchClientPeer(new NetworkClientMessageChannel());
            _client.OnConnectedCallback += OnConnectedCallback;
            _client.OnDisconnectedCallback += OnDisconnectedCallback;
            _client.RegisterHandler<NetworkDataPacket>(OnNetworkDataPacket);
            _server = new ENetPunchServerPeer(new NetworkServerMessageChannel());
            _server.OnConnectedCallback += OnConnectedCallback;
            _server.OnDisconnectedCallback += OnDisconnectedCallback;
            _server.RegisterHandler<NetworkDataPacket>(OnNetworkDataPacket);
        }

        /// <summary>
        ///     Port
        /// </summary>
        public ushort Port
        {
            get => _port;
            set => _port = value;
        }

        /// <summary>
        ///     Connection callback
        /// </summary>
        private void OnConnectedCallback() => OnClientConnected.Invoke();

        /// <summary>
        ///     Break callback
        /// </summary>
        private void OnDisconnectedCallback() => OnClientDisconnected.Invoke();

        /// <summary>
        ///     Connection callback
        /// </summary>
        private void OnConnectedCallback(uint clientId)
        {
            var connectionId = (int)clientId + 1;
            OnServerConnected.Invoke(connectionId);
        }

        /// <summary>
        ///     Break callback
        /// </summary>
        private void OnDisconnectedCallback(uint clientId)
        {
            var connectionId = (int)clientId + 1;
            OnServerDisconnected.Invoke(connectionId);
        }

        /// <summary>
        ///     Is it supported
        /// </summary>
        public override bool Available() => true;

        /// <summary>
        ///     Start client
        /// </summary>
        public override void ClientConnect(string address)
        {
            Address = address;
            _client.Start(address, Port, HostAddress, HostPort);
        }

        /// <summary>
        ///     Start the server
        /// </summary>
        public override void ServerStart() => _server.Start(Port, MaxClients, HostAddress, HostPort);

        /// <summary>
        ///     Client Connection
        /// </summary>
        /// <returns>Client Connection</returns>
        public override bool ClientConnected() => _client.Connected;

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <param name="channelId">Passageway</param>
        public override void ClientSend(ArraySegment<byte> payload, int channelId = Channels.Reliable)
        {
            var networkDataPacket = new NetworkDataPacket(payload);
            _client.Send(networkDataPacket);
        }

        /// <summary>
        ///     Disconnect local client
        /// </summary>
        public override void ClientDisconnect() => _client.Shutdown();

        /// <summary>
        ///     ServerUri
        /// </summary>
        /// <returns>ServerUri</returns>
        public override Uri ServerUri() => null;

        /// <summary>
        ///     Server valid
        /// </summary>
        /// <returns>Server valid</returns>
        public override bool ServerActive() => _server.IsListening;

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        /// <param name="payload">Payload</param>
        /// <param name="channelId">Passageway</param>
        public override void ServerSend(int connectionId, ArraySegment<byte> payload, int channelId = Channels.Reliable)
        {
            var networkDataPacket = new NetworkDataPacket(payload);
            var clientId = (uint)connectionId - 1;
            _server.Send(clientId, networkDataPacket);
        }

        /// <summary>
        ///     Disconnect remote client
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        public override void ServerDisconnect(int connectionId) => _server.Disconnect((uint)connectionId);

        /// <summary>
        ///     Server obtains client address
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        /// <returns>The client address obtained by the server</returns>
        public override string ServerGetClientAddress(int connectionId) => null;

        /// <summary>
        ///     Stop server
        /// </summary>
        public override void ServerStop() => _server.Shutdown();

        /// <summary>
        ///     Get the maximum packet capacity
        /// </summary>
        /// <param name="channelId">Passageway</param>
        /// <returns>Maximum packet capacity obtained</returns>
        public override int GetMaxPacketSize(int channelId = Channels.Reliable) => 1024;

        /// <summary>
        ///     Cease
        /// </summary>
        public override void Shutdown()
        {
            ClientDisconnect();
            ServerStop();
        }

        /// <summary>
        ///     Server data packet
        ///     Only Client
        /// </summary>
        [ClientRPC(true)]
        private void OnNetworkDataPacket(NetworkDataPacket data) => OnClientDataReceived.Invoke(data.Payload, 0);

        /// <summary>
        ///     Client packet
        ///     Only Server/Host
        /// </summary>
        [ServerRPC(true)]
        private void OnNetworkDataPacket(uint clientId, NetworkDataPacket data)
        {
            var connectionId = (int)clientId + 1;
            OnServerDataReceived.Invoke(connectionId, data.Payload, 0);
        }
    }
}