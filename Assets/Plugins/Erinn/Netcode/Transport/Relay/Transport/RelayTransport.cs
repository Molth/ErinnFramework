//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections;
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
    public sealed class RelayTransport : UnityTransport, IClientRPCListener
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
        ///     Password
        /// </summary>
        [Header("Password")] public uint Password;

        /// <summary>
        ///     Valid
        /// </summary>
        /// <returns>Valid</returns>
        private bool _valid;

        /// <summary>
        ///     Singleton
        /// </summary>
        public static RelayTransport Singleton { get; private set; }

        /// <summary>
        ///     Is it supported
        /// </summary>
        public override bool IsSupported => true;

        /// <summary>
        ///     Client
        /// </summary>
        private static UnityClient ClientMaster => UnityClient.Singleton;

        /// <summary>
        ///     The client of the serverId
        /// </summary>
        public override ulong ServerClientId => 0UL;

        /// <summary>
        ///     Awake When calling
        /// </summary>
        private void Awake()
        {
            HostId = new Variable<uint>();
            Singleton = this;
        }

        /// <summary>
        ///     Start When calling
        /// </summary>
        private void Start()
        {
            ClientMaster.OnConnectedCallback += OnConnectedCallback;
            ClientMaster.OnDisconnectedCallback += OnDisconnectedCallback;
            ClientMaster.RegisterHandler<NetworkIdPacket>(OnNetworkIdPacket);
            ClientMaster.RegisterHandler<JoinedRoomMessage>(OnJoinedRoomMessage);
            ClientMaster.RegisterHandler<LeftRoomMessage>(OnLeftRoomMessage);
            ClientMaster.RegisterHandler<RelayServerPacket>(OnRelayServerPacket);
            ClientMaster.RegisterHandler<RelayPacket>(OnRelayPacket);
            ClientMaster.RegisterHandler<JoinRoomResponse>(OnJoinRoomResponse);
            ClientMaster.RegisterHandler<DeleteRoomMessage>(OnDeleteRoomMessage);
            if (AutoConnect)
                Connect();
        }

        /// <summary>
        ///     Initialize
        /// </summary>
        [ClientRPC]
        private void OnNetworkIdPacket(NetworkIdPacket data) => LocalId.Set(data.Id);

        /// <summary>
        ///     Starting the host
        /// </summary>
        /// <param name="password">Password</param>
        public static void StartHost(uint password)
        {
            Singleton.Password = password;
            NetworkManager.Singleton.StartHost();
        }

        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="password">Password</param>
        public static void StartServer(uint password)
        {
            Singleton.Password = password;
            NetworkManager.Singleton.StartServer();
        }

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="hostId">HostId</param>
        public static void StartClient(uint hostId)
        {
            Singleton.HostId.Set(hostId);
            NetworkManager.Singleton.StartClient();
        }

        /// <summary>
        ///     Connect
        /// </summary>
        public void Connect() => ClientMaster.StartClient(ConnectionData.Address, ConnectionData.Port);

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        public override ulong GetCurrentRtt(ulong clientId) => ClientMaster.GetRoundTripTime() * 2UL;

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
            var relayServerPacket = new RelayServerPacket(payload);
            ClientMaster.Send(relayServerPacket);
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        private void ServerSend(uint clientId, ArraySegment<byte> payload)
        {
            var relayPacket = new RelayPacket(clientId, payload);
            ClientMaster.Send(relayPacket);
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="payload">Load</param>
        private void ClientSendInternal(ArraySegment<byte> payload)
        {
            var relayServerPacket = new RelayServerPacket(payload);
            ClientMaster.Send(relayServerPacket);
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Load</param>
        private void ServerSendInternal(uint clientId, ArraySegment<byte> payload)
        {
            var relayPacket = new RelayPacket(clientId, payload);
            ClientMaster.Send(relayPacket);
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
        public override bool StartClient()
        {
            if (!ClientMaster.Connected || HostId.Value == LocalId.Value)
                return false;
            _valid = true;
            ClientMaster.Send(new JoinRoomRequest(HostId, Password));
            return true;
        }

        /// <summary>
        ///     Start the server
        /// </summary>
        public override bool StartServer()
        {
            if (!ClientMaster.Connected)
                return false;
            _valid = true;
            ClientMaster.Send(new CreateRoomRequest(Password));
            return true;
        }

        /// <summary>
        ///     Disconnect remote client
        /// </summary>
        /// <param name="clientId">ClientId</param>
        public override void DisconnectRemoteClient(ulong clientId) => ClientMaster.Send(new DisconnectRemoteClientMessage((uint)clientId));

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
            if (_valid)
            {
                _valid = false;
                ClientMaster.Send(new DeleteRoomMessage());
            }

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
        ///     Join Room
        ///     Only Server/Host
        /// </summary>
        [ClientRPC]
        private void OnJoinedRoomMessage(JoinedRoomMessage data)
        {
            if (!NetworkManager.Singleton.IsServer)
                return;
            ClientMaster.Send(new JoinRoomAckMessage(data.RoomId));
            var clientId = (ulong)data.RoomId;
            InvokeTransportEvent(NetworkEvent.Connect, clientId);
        }

        /// <summary>
        ///     Leave the room
        ///     Only Server/Host
        /// </summary>
        [ClientRPC]
        private void OnLeftRoomMessage(LeftRoomMessage data)
        {
            var clientId = (ulong)data.RoomId;
            InvokeTransportEvent(NetworkEvent.Disconnect, clientId);
        }

        /// <summary>
        ///     RelayData packet
        ///     Only Client
        /// </summary>
        [ClientRPC(true)]
        private void OnRelayServerPacket(RelayServerPacket data) => OnClientDataReceived(data.Payload);

        /// <summary>
        ///     Client receives data packet
        /// </summary>
        /// <param name="payload">Data packet</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnClientDataReceived(ArraySegment<byte> payload) => InvokeTransportEvent(NetworkEvent.Data, 0UL, payload);

        /// <summary>
        ///     RelayData packet
        ///     Only Server/Host
        /// </summary>
        [ClientRPC(true)]
        private void OnRelayPacket(RelayPacket data)
        {
            var clientId = (ulong)data.RoomId;
            OnServerDataReceived(clientId, data.Payload);
        }

        /// <summary>
        ///     Server receives data packet
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="payload">Data packet</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void OnServerDataReceived(ulong clientId, ArraySegment<byte> payload) => InvokeTransportEvent(NetworkEvent.Data, clientId, payload);

        /// <summary>
        ///     Join Room
        ///     Only Client
        /// </summary>
        [ClientRPC]
        private void OnJoinRoomResponse(JoinRoomResponse data)
        {
            if (!data.Success)
                NetworkManager.Singleton.Shutdown();
            else
                InvokeTransportEvent(NetworkEvent.Connect);
        }

        /// <summary>
        ///     Leave the room
        /// </summary>
        [ClientRPC]
        private void OnDeleteRoomMessage(DeleteRoomMessage data)
        {
            if (!NetworkManager.Singleton.IsClient)
                return;
            NetworkManager.Singleton.Shutdown();
        }
    }
}