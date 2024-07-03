//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections;
using Erinn;
using UnityEngine;

namespace Mirror
{
    /// <summary>
    ///     Relay Transmission
    /// </summary>
    public sealed class RelayTransport : Transport, IClientRPCListener
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
        ///     Address
        /// </summary>
        [Header("Address")] public string Address = "127.0.0.1";

        /// <summary>
        ///     Port
        /// </summary>
        [Header("Address")] public ushort Port = 7777;

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
        ///     Client Connection
        /// </summary>
        private bool _clientConnected;

        /// <summary>
        ///     Server valid
        /// </summary>
        private bool _serverActive;

        /// <summary>
        ///     Singleton
        /// </summary>
        public static RelayTransport Singleton { get; private set; }

        /// <summary>
        ///     Client
        /// </summary>
        private static UnityClient ClientMaster => UnityClient.Singleton;

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
            NetworkManager.singleton.StartHost();
        }

        /// <summary>
        ///     Start the server
        /// </summary>
        /// <param name="password">Password</param>
        public static void StartServer(uint password)
        {
            Singleton.Password = password;
            NetworkManager.singleton.StartServer();
        }

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="hostId">HostId</param>
        public static void StartClient(uint hostId)
        {
            Singleton.HostId.Set(hostId);
            NetworkManager.singleton.StartClient();
        }

        /// <summary>
        ///     Is it supported
        /// </summary>
        public override bool Available() => true;

        /// <summary>
        ///     Connect
        /// </summary>
        public void Connect() => ClientMaster.StartClient(Address, Port);

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
            if (NetworkManager.singleton == null)
                return;
            OnShutdown();
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
        ///     Start client
        /// </summary>
        public override void ClientConnect(string address)
        {
            if (!ClientMaster.Connected || HostId.Value == LocalId.Value)
            {
                NetworkManager.singleton.StopClient();
                return;
            }

            _valid = true;
            ClientMaster.Send(new JoinRoomRequest(HostId, Password));
        }

        /// <summary>
        ///     Start the server
        /// </summary>
        public override void ServerStart()
        {
            if (!ClientMaster.Connected)
                return;
            _valid = true;
            ClientMaster.Send(new CreateRoomRequest(Password));
        }

        /// <summary>
        ///     Client Connection
        /// </summary>
        /// <returns>Client Connection</returns>
        public override bool ClientConnected() => _clientConnected;

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <param name="channelId">Passageway</param>
        public override void ClientSend(ArraySegment<byte> payload, int channelId = Channels.Reliable)
        {
            var relayServerPacket = new RelayServerPacket(payload);
            ClientMaster.Send(relayServerPacket);
        }

        /// <summary>
        ///     Disconnect local client
        /// </summary>
        public override void ClientDisconnect()
        {
            _clientConnected = false;
            if (_serverActive)
                return;
            ShutdownInternal();
        }

        /// <summary>
        ///     ServerUri
        /// </summary>
        /// <returns>ServerUri</returns>
        public override Uri ServerUri() => null;

        /// <summary>
        ///     Server valid
        /// </summary>
        /// <returns>Server valid</returns>
        public override bool ServerActive() => _serverActive;

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        /// <param name="payload">Payload</param>
        /// <param name="channelId">Passageway</param>
        public override void ServerSend(int connectionId, ArraySegment<byte> payload, int channelId = Channels.Reliable)
        {
            var relayPacket = new RelayPacket((uint)connectionId, payload);
            ClientMaster.Send(relayPacket);
        }

        /// <summary>
        ///     Disconnect remote client
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        public override void ServerDisconnect(int connectionId) => ClientMaster.Send(new DisconnectRemoteClientMessage((uint)connectionId));

        /// <summary>
        ///     Server obtains client address
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        /// <returns>The client address obtained by the server</returns>
        public override string ServerGetClientAddress(int connectionId) => null;

        /// <summary>
        ///     Stop server
        /// </summary>
        public override void ServerStop()
        {
            _serverActive = false;
            ShutdownInternal();
        }

        /// <summary>
        ///     Get the maximum packet capacity
        /// </summary>
        /// <param name="channelId">Passageway</param>
        /// <returns>Maximum packet capacity obtained</returns>
        public override int GetMaxPacketSize(int channelId = Channels.Reliable) => 1024;

        /// <summary>
        ///     Cease
        /// </summary>
        public override void Shutdown() => ShutdownInternal();

        /// <summary>
        ///     Cease
        /// </summary>
        private void ShutdownInternal()
        {
            if (!_valid)
                return;
            _valid = false;
            ClientMaster.Send(new DeleteRoomMessage());
        }

        /// <summary>
        ///     Join Room
        ///     Only Server/Host
        /// </summary>
        [ClientRPC]
        private void OnJoinedRoomMessage(JoinedRoomMessage data)
        {
            if (!_serverActive)
                return;
            ClientMaster.Send(new JoinRoomAckMessage(data.RoomId));
            var clientId = (int)data.RoomId;
            OnServerConnected.Invoke(clientId);
        }

        /// <summary>
        ///     Leave the room
        ///     Only Server/Host
        /// </summary>
        [ClientRPC]
        private void OnLeftRoomMessage(LeftRoomMessage data)
        {
            if (!_serverActive)
                return;
            var clientId = (int)data.RoomId;
            OnServerDisconnected.Invoke(clientId);
        }

        /// <summary>
        ///     RelayData packet
        ///     Only Client
        /// </summary>
        [ClientRPC(true)]
        private void OnRelayServerPacket(RelayServerPacket data)
        {
            if (!_clientConnected)
                return;
            OnClientDataReceived.Invoke(data.Payload, 0);
        }

        /// <summary>
        ///     RelayData packet
        ///     Only Server/Host
        /// </summary>
        [ClientRPC(true)]
        private void OnRelayPacket(RelayPacket data)
        {
            if (!_serverActive)
                return;
            var clientId = (int)data.RoomId;
            OnServerDataReceived.Invoke(clientId, data.Payload, 0);
        }

        /// <summary>
        ///     Join Room
        ///     Only Client
        /// </summary>
        [ClientRPC]
        private void OnJoinRoomResponse(JoinRoomResponse data)
        {
            if (!data.Success)
            {
                _clientConnected = false;
                OnShutdown();
            }
            else
            {
                _clientConnected = true;
                OnClientConnected.Invoke();
            }
        }

        /// <summary>
        ///     Leave the room
        /// </summary>
        [ClientRPC]
        private void OnDeleteRoomMessage(DeleteRoomMessage data)
        {
            if (!NetworkClient.active)
                return;
            OnShutdown();
        }

        /// <summary>
        ///     Cease
        /// </summary>
        private void OnShutdown()
        {
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopServer();
        }
    }
}