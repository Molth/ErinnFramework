//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Relay Transmission
    /// </summary>
    public sealed class ENetPunchTransport : UnityTransport, IClientRPCListener
    {
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
        ///     Is it supported
        /// </summary>
        public override bool IsSupported => true;

        /// <summary>
        ///     The client of the serverId
        /// </summary>
        public override ulong ServerClientId => 0UL;

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
        ///     Connection callback
        /// </summary>
        private void OnConnectedCallback() => InvokeTransportEvent(NetworkEvent.Connect);

        /// <summary>
        ///     Break callback
        /// </summary>
        private void OnDisconnectedCallback() => InvokeTransportEvent(NetworkEvent.Disconnect);

        /// <summary>
        ///     Connection callback
        /// </summary>
        private void OnConnectedCallback(uint id)
        {
            var clientId = id + 1UL;
            InvokeTransportEvent(NetworkEvent.Connect, clientId);
        }

        /// <summary>
        ///     Break callback
        /// </summary>
        private void OnDisconnectedCallback(uint id)
        {
            var clientId = id + 1UL;
            InvokeTransportEvent(NetworkEvent.Disconnect, clientId);
        }

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        public override ulong GetCurrentRtt(ulong clientId)
        {
            if (clientId == 0UL)
            {
                if (_client != null)
                    return _client.GetRoundTripTime();
            }
            else
            {
                if (_server != null)
                {
                    var id = (uint)clientId - 1U;
                    return _server.GetRoundTripTime(id);
                }
            }

            return 0UL;
        }

        /// <summary>
        ///     Execute transmission events
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        private void InvokeTransportEvent(NetworkEvent eventType, ulong clientId = 0UL, ArraySegment<byte> payload = default) => InvokeOnTransportEvent(eventType, clientId, payload, Time.realtimeSinceStartup);

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        /// <param name="networkDelivery">Transmission mode</param>
        public override void Send(ulong clientId, ArraySegment<byte> payload, NetworkDelivery networkDelivery)
        {
            if (clientId == 0UL)
                ClientSend(payload);
            else
                ServerSend((uint)clientId, payload);
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="payload">Load</param>
        private void ClientSend(ArraySegment<byte> payload)
        {
            var networkDataPacket = new NetworkDataPacket(payload);
            _client.Send(networkDataPacket);
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        private void ServerSend(uint clientId, ArraySegment<byte> payload)
        {
            var networkDataPacket = new NetworkDataPacket(payload);
            var id = clientId - 1U;
            _server.Send(id, networkDataPacket);
        }

        /// <summary>
        ///     Polling events
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        /// <param name="receiveTime">Receive timestamp</param>
        /// <returns>Event types obtained</returns>
        public override NetworkEvent PollEvent(out ulong clientId, out ArraySegment<byte> payload, out float receiveTime)
        {
            clientId = default;
            payload = default;
            receiveTime = default;
            return NetworkEvent.Nothing;
        }

        /// <summary>
        ///     Start client
        /// </summary>
        public override bool StartClient() => _client.Start(ConnectionData.Address, ConnectionData.Port, HostAddress, HostPort);

        /// <summary>
        ///     Start the server
        /// </summary>
        public override bool StartServer() => _server.Start(ConnectionData.Port, MaxClients, HostAddress, HostPort);

        /// <summary>
        ///     Disconnect remote client
        /// </summary>
        /// <param name="clientId">ClientId</param>
        public override void DisconnectRemoteClient(ulong clientId)
        {
            var id = (uint)clientId - 1U;
            _server.Disconnect(id);
        }

        /// <summary>
        ///     Disconnect local client
        /// </summary>
        public override void DisconnectLocalClient() => Shutdown();

        /// <summary>
        ///     Cease
        /// </summary>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public override void Shutdown()
        {
            _client.Shutdown();
            _server.Shutdown();
            var fieldInfo = typeof(NetworkManager).GetField("ConnectionManager", BindingFlags.Instance | BindingFlags.NonPublic);
            var connectionManager = fieldInfo.GetValue(NetworkManager.Singleton);
            var fieldInfo2 = typeof(NetworkConnectionManager).GetField("m_NextClientId", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo2.SetValue(connectionManager, 1UL);
        }

        /// <summary>
        ///     Initialization
        /// </summary>
        public override void Initialize(NetworkManager networkManager = null)
        {
        }

        /// <summary>
        ///     Server data packet
        ///     Only Client
        /// </summary>
        [ClientRPC(true)]
        private void OnNetworkDataPacket(NetworkDataPacket data) => OnClientDataReceived(data.Payload);

        /// <summary>
        ///     Client receives data packet
        /// </summary>
        /// <param name="payload">Data packet</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnClientDataReceived(ArraySegment<byte> payload) => InvokeTransportEvent(NetworkEvent.Data, 0UL, payload);

        /// <summary>
        ///     Client packet
        ///     Only Server/Host
        /// </summary>
        [ServerRPC(true)]
        private void OnNetworkDataPacket(uint clientId, NetworkDataPacket data) => OnServerDataReceived(clientId, data.Payload);

        /// <summary>
        ///     Server receives data packet
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Data packet</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnServerDataReceived(ulong clientId, ArraySegment<byte> payload)
        {
            var id = clientId + 1UL;
            InvokeTransportEvent(NetworkEvent.Data, id, payload);
        }
    }
}