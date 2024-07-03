//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Virtual lobby transport
    /// </summary>
    public sealed class LobbyTransport : UnityTransport
    {
        /// <summary>
        ///     Automatic connection
        /// </summary>
        [Header("Auto Connect")] public bool AutoConnect = true;

        /// <summary>
        ///     Automatic reconnection
        /// </summary>
        [Header("Auto Reconnect")] public bool AutoReconnect = true;

        /// <summary>
        ///     Reconnect delay
        /// </summary>
        [Header("Reconnect Delay")] [Min(1f)] public float ReconnectDelay = 1f;

        /// <summary>
        ///     HostId
        /// </summary>
        [Header("Master Client Id")] public Variable<uint> HostId;

        /// <summary>
        ///     LocalId
        /// </summary>
        [Header("Local Id")] public Variable<uint> LocalId;

        /// <summary>
        ///     MaxPlayers
        /// </summary>
        [Header("Max Players")] public uint MaxPlayers = 2U;

        /// <summary>
        ///     Password
        /// </summary>
        [Header("Password")] public uint Password;

        /// <summary>
        ///     Singleton
        /// </summary>
        public static LobbyTransport Singleton { get; private set; }

        /// <summary>
        ///     Client
        /// </summary>
        private static UnityClient ClientMaster => UnityClient.Singleton;

        /// <summary>
        ///     The client of the serverId
        /// </summary>
        public override ulong ServerClientId => 0UL;

        /// <summary>
        ///     Awake
        /// </summary>
        private void Awake() => Singleton = this;

        /// <summary>
        ///     Start
        /// </summary>
        private void Start()
        {
            ClientMaster.OnConnectedCallback += OnConnectedCallback;
            ClientMaster.OnDisconnectedCallback += OnDisconnectedCallback;
            ClientMaster.RegisterHandler<NetworkIdPacket>(OnNetworkIdPacket);
            ClientMaster.RegisterHandler<RawConnectRequest>(ServerOnRawConnectRequest);
            ClientMaster.RegisterHandler<RawConnectResponse>(ClientOnRawConnectResponse);
            ClientMaster.RegisterHandler<RawServerDisconnectPacket>(ServerOnRawDisconnectPacket);
            ClientMaster.RegisterHandler<RawServerSendPacket>(ServerOnRawSendPacket);
            ClientMaster.RegisterHandler<RawClientDisconnectPacket>(ClientOnRawDisconnectPacket);
            ClientMaster.RegisterHandler<RawClientSendPacket>(ClientOnRawSendPacket);
            if (AutoConnect)
                Connect();
        }

        /// <summary>
        ///     Initialize
        /// </summary>
        [ClientRPC]
        private void OnNetworkIdPacket(NetworkIdPacket data) => LocalId.Set(data.Id);

        /// <summary>
        ///     Connect
        /// </summary>
        public void Connect() => ClientMaster.StartClient(ConnectionData.Address, ConnectionData.Port);

        /// <summary>
        ///     Connection callback
        /// </summary>
        private void OnConnectedCallback() => StopAllCoroutines();

        /// <summary>
        ///     Break callback
        /// </summary>
        private void OnDisconnectedCallback()
        {
            StopAllCoroutines();
            if (NetworkManager.Singleton == null)
                return;
            NetworkManager.Singleton.Shutdown();
            if (AutoReconnect)
                StartCoroutine(Reconnect());
        }

        /// <summary>
        ///     Reconnection
        /// </summary>
        private IEnumerator Reconnect()
        {
            if (ReconnectDelay < 1f)
                ReconnectDelay = 1f;
            yield return new WaitForSecondsRealtime(ReconnectDelay);
            Connect();
        }

        /// <summary>
        ///     Start server
        /// </summary>
        public override bool StartServer() => true;

        /// <summary>
        ///     Start client
        /// </summary>
        public override bool StartClient()
        {
            if (!ClientMaster.Connected || HostId.Value == LocalId.Value)
                return false;
            ClientMaster.Send(new RawConnectRequest(HostId, Password));
            return true;
        }

        /// <summary>
        ///     Connect
        /// </summary>
        [ClientRPC]
        private void ServerOnRawConnectRequest(RawConnectRequest rawConnectRequest)
        {
            if (NetworkManager.Singleton.ConnectedClients.Count >= MaxPlayers || rawConnectRequest.Password != Password)
            {
                ClientMaster.Send(new RawClientDisconnectPacket(rawConnectRequest.RemoteEndPoint));
                return;
            }

            InvokeTransportEvent(NetworkEvent.Connect, rawConnectRequest.RemoteEndPoint + 1U);
            ClientMaster.Send(new RawConnectResponse(rawConnectRequest.RemoteEndPoint));
        }

        /// <summary>
        ///     Connect
        /// </summary>
        [ClientRPC]
        private void ClientOnRawConnectResponse(RawConnectResponse rawConnectResponse)
        {
            if (HostId != rawConnectResponse.RemoteEndPoint)
                return;
            InvokeTransportEvent(NetworkEvent.Connect);
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        [ClientRPC]
        private void ServerOnRawDisconnectPacket(RawServerDisconnectPacket rawDisconnectPacket) => InvokeTransportEvent(NetworkEvent.Disconnect, rawDisconnectPacket.RemoteEndPoint + 1U);

        /// <summary>
        ///     Disconnect
        /// </summary>
        [ClientRPC]
        private void ClientOnRawDisconnectPacket(RawClientDisconnectPacket rawDisconnectPacket)
        {
            if (HostId != rawDisconnectPacket.RemoteEndPoint)
                return;
            InvokeTransportEvent(NetworkEvent.Disconnect);
        }

        /// <summary>
        ///     Data
        /// </summary>
        [ClientRPC(true)]
        private void ServerOnRawSendPacket(RawServerSendPacket rawSendPacket) => InvokeTransportEvent(NetworkEvent.Data, rawSendPacket.RemoteEndPoint + 1U, rawSendPacket.Payload);

        /// <summary>
        ///     Data
        /// </summary>
        [ClientRPC(true)]
        private void ClientOnRawSendPacket(RawClientSendPacket rawSendPacket)
        {
            if (HostId != rawSendPacket.RemoteEndPoint)
                return;
            InvokeTransportEvent(NetworkEvent.Data, 0UL, rawSendPacket.Payload);
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public override void DisconnectLocalClient() => ClientMaster.Send(new RawServerDisconnectPacket(HostId));

        /// <summary>
        ///     Disconnect
        /// </summary>
        public override void DisconnectRemoteClient(ulong clientId) => ClientMaster.Send(new RawClientDisconnectPacket((uint)clientId - 1U));

        /// <summary>
        ///     Initialize
        /// </summary>
        public override void Initialize(NetworkManager networkManager = null)
        {
        }

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        public override ulong GetCurrentRtt(ulong clientId) => ClientMaster.GetRoundTripTime();

        /// <summary>
        ///     Shutdown
        /// </summary>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public override void Shutdown()
        {
            var fieldInfo = typeof(NetworkManager).GetField("ConnectionManager", BindingFlags.Instance | BindingFlags.NonPublic);
            var connectionManager = fieldInfo.GetValue(NetworkManager.Singleton);
            var fieldInfo2 = typeof(NetworkConnectionManager).GetField("m_NextClientId", BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo2.SetValue(connectionManager, 1UL);
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="payload">Load</param>
        private void ClientSend(ArraySegment<byte> payload) => ClientMaster.Send(new RawServerSendPacket(HostId, payload));

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        private void ServerSend(uint clientId, ArraySegment<byte> payload) => ClientMaster.Send(new RawClientSendPacket(clientId - 1U, payload));

        /// <summary>
        ///     Send
        /// </summary>
        public override void Send(ulong clientId, ArraySegment<byte> payload, NetworkDelivery networkDelivery)
        {
            if (clientId == 0UL)
                ClientSend(payload);
            else
                ServerSend((uint)clientId, payload);
        }

        /// <summary>
        ///     Execute transmission events
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        private void InvokeTransportEvent(NetworkEvent eventType, ulong clientId = 0UL, ArraySegment<byte> payload = default) => InvokeOnTransportEvent(eventType, clientId, payload, Time.realtimeSinceStartup);
    }
}