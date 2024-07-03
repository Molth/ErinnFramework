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
    ///     Virtual lobby transport
    /// </summary>
    public sealed class LobbyTransport : Transport
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
        ///     MaxPlayers
        /// </summary>
        [Header("Max Players")] public uint MaxPlayers = 2U;

        /// <summary>
        ///     Password
        /// </summary>
        [Header("Password")] public uint Password;

        /// <summary>
        ///     Connections
        /// </summary>
        private UintIndexMap<uint> _connections;

        /// <summary>
        ///     Client Connection
        /// </summary>
        private bool _clientConnected;

        /// <summary>
        ///     Server valid
        /// </summary>
        /// <returns>Server valid</returns>
        private bool _serverActive;

        /// <summary>
        ///     Singleton
        /// </summary>
        public static LobbyTransport Singleton { get; private set; }

        /// <summary>
        ///     Client
        /// </summary>
        private static UnityClient ClientMaster => UnityClient.Singleton;

        /// <summary>
        ///     Awake
        /// </summary>
        private void Awake()
        {
            Singleton = this;
            _connections = new UintIndexMap<uint>();
        }

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
        ///     Is it supported
        /// </summary>
        public override bool Available() => Application.platform != RuntimePlatform.WebGLPlayer;

        /// <summary>
        ///     Client Connection
        /// </summary>
        /// <returns>Client Connection</returns>
        public override bool ClientConnected() => _clientConnected;

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

            ClientMaster.Send(new RawConnectRequest(HostId, Password));
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <param name="channelId">Passageway</param>
        public override void ClientSend(ArraySegment<byte> payload, int channelId = Channels.Reliable) => ClientMaster.Send(new RawServerSendPacket(HostId, payload));

        /// <summary>
        ///     Disconnect
        /// </summary>
        public override void ClientDisconnect()
        {
            _clientConnected = false;
            ClientMaster.Send(new RawServerDisconnectPacket(HostId));
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
        ///     Start server
        /// </summary>
        public override void ServerStart()
        {
            _connections.Allocate();
            _serverActive = true;
        }

        /// <summary>
        ///     SendRelayData
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        /// <param name="payload">Payload</param>
        /// <param name="channelId">Passageway</param>
        public override void ServerSend(int connectionId, ArraySegment<byte> payload, int channelId = Channels.Reliable)
        {
            if (!_connections.TryGetKey((uint)connectionId, out var remoteEndPoint))
                return;
            ClientMaster.Send(new RawClientSendPacket(remoteEndPoint, payload));
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public override void ServerDisconnect(int connectionId)
        {
            if (!_connections.TryRemoveValue((uint)connectionId, out var remoteEndPoint))
                return;
            ClientMaster.Send(new RawClientDisconnectPacket(remoteEndPoint));
        }

        /// <summary>
        ///     Server obtains client address
        /// </summary>
        /// <param name="connectionId">ClientId</param>
        /// <returns>The client address obtained by the server</returns>
        public override string ServerGetClientAddress(int connectionId) => null;

        /// <summary>
        ///     Stop server
        /// </summary>
        public override void ServerStop() => _serverActive = false;

        /// <summary>
        ///     Get the maximum packet capacity
        /// </summary>
        /// <param name="channelId">Passageway</param>
        /// <returns>Maximum packet capacity obtained</returns>
        public override int GetMaxPacketSize(int channelId = Channels.Reliable) => 1024;

        /// <summary>
        ///     Connect
        /// </summary>
        [ClientRPC]
        private void ServerOnRawConnectRequest(RawConnectRequest rawConnectRequest)
        {
            if (NetworkServer.connections.Count >= MaxPlayers || rawConnectRequest.Password != Password)
            {
                ClientMaster.Send(new RawServerDisconnectPacket(rawConnectRequest.RemoteEndPoint));
                return;
            }

            var clientId = _connections.Add(rawConnectRequest.RemoteEndPoint);
            OnServerConnected.Invoke((int)clientId);
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
            _clientConnected = true;
            OnClientConnected.Invoke();
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        [ClientRPC]
        private void ServerOnRawDisconnectPacket(RawServerDisconnectPacket rawDisconnectPacket)
        {
            if (!_connections.TryRemoveKey(rawDisconnectPacket.RemoteEndPoint, out var clientId))
                return;
            OnServerDisconnected.Invoke((int)clientId);
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        [ClientRPC]
        private void ClientOnRawDisconnectPacket(RawClientDisconnectPacket rawDisconnectPacket)
        {
            if (HostId != rawDisconnectPacket.RemoteEndPoint)
                return;
            OnClientDisconnected.Invoke();
        }

        /// <summary>
        ///     Data
        /// </summary>
        [ClientRPC(true)]
        private void ServerOnRawSendPacket(RawServerSendPacket rawSendPacket)
        {
            if (!_serverActive)
                return;
            if (!_connections.TryGetValue(rawSendPacket.RemoteEndPoint, out var clientId))
                return;
            OnServerDataReceived.Invoke((int)clientId, rawSendPacket.Payload, 0);
        }

        /// <summary>
        ///     Data
        /// </summary>
        [ClientRPC(true)]
        private void ClientOnRawSendPacket(RawClientSendPacket rawSendPacket)
        {
            if (HostId != rawSendPacket.RemoteEndPoint)
                return;
            if (!_clientConnected)
                return;
            OnClientDataReceived.Invoke(rawSendPacket.Payload, 0);
        }

        /// <summary>
        ///     Shutdown
        /// </summary>
        public override void Shutdown()
        {
            _clientConnected = false;
            _serverActive = false;
            _connections.Clear();
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