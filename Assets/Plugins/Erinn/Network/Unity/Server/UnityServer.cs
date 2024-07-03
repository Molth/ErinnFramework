//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Reflection;
using MemoryPack;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Network transmission
    /// </summary>
    public sealed class UnityServer : MonoBehaviour
    {
        /// <summary>
        ///     Protocol type
        /// </summary>
        [Header("Protocol Type")] [SerializeField]
        private NetworkProtocolType _initializeProtocolType;

        /// <summary>
        ///     Protocol type
        /// </summary>
        public NetworkProtocolType ProtocolType { get; private set; }

        /// <summary>
        ///     Singleton
        /// </summary>
        public static UnityServer Singleton { get; private set; }

        /// <summary>
        ///     Charge
        /// </summary>
        public NetworkServer Master { get; private set; }

        /// <summary>
        ///     Listening
        /// </summary>
        public bool IsListening => Master.IsListening;

        /// <summary>
        ///     Call on load
        /// </summary>
        private void Awake()
        {
            ProtocolType = _initializeProtocolType;
            Application.runInBackground = true;
            Master = new NetworkServer();
            Master.Initialize(ProtocolType);
            Singleton = this;
        }

        /// <summary>
        ///     Release upon destruction
        /// </summary>
        private void OnDestroy() => Shutdown();

        /// <summary>
        ///     Release when exiting the application
        /// </summary>
        private void OnApplicationQuit() => Shutdown();

        /// <summary>
        ///     Connection Event
        /// </summary>
        public event Action<uint> OnConnectedCallback
        {
            add => Master.OnConnectedCallback += value;
            remove => Master.OnConnectedCallback -= value;
        }

        /// <summary>
        ///     Disconnect event
        /// </summary>
        public event Action<uint> OnDisconnectedCallback
        {
            add => Master.OnDisconnectedCallback += value;
            remove => Master.OnDisconnectedCallback -= value;
        }

        /// <summary>
        ///     Destruction
        /// </summary>
        public bool Shutdown() => Master.Shutdown();

        /// <summary>
        ///     Disconnect
        /// </summary>
        /// <param name="id">ClientId</param>
        public void Disconnect(uint id) => Master.Disconnect(id);

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        public void RegisterHandlers<T>(T listener) where T : IServerRPCListener => Master.RegisterHandlers(listener);

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        public void RegisterHandlers<T>(T listener, Type type) where T : IServerRPCListener => Master.RegisterHandlers(listener, type);

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        public void RegisterHandlers(Type type) => Master.RegisterHandlers(type);

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        public void RegisterHandlers(Assembly assembly) => Master.RegisterHandlers(assembly);

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        public void RegisterHandlers() => Master.RegisterHandlers();

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <typeparam name="T">Type</typeparam>
        public void RegisterHandler<T>(Action<uint, T> handler) where T : struct, INetworkMessage, IMemoryPackable<T> => Master.RegisterHandler(handler);

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        public void UnregisterHandlers<T>(T listener) where T : IServerRPCListener => Master.UnregisterHandlers(listener);

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        public void UnregisterHandlers<T>(T listener, Type type) where T : IServerRPCListener => Master.UnregisterHandlers(listener, type);

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        public void UnregisterHandlers(Type type) => Master.UnregisterHandlers(type);

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        public void UnregisterHandlers(Assembly assembly) => Master.UnregisterHandlers(assembly);

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        public void UnregisterHandlers() => Master.UnregisterHandlers();

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public void UnregisterHandler<T>() where T : struct, INetworkMessage, IMemoryPackable<T> => Master.UnregisterHandler<T>();

        /// <summary>
        ///     Clear command handle
        /// </summary>
        public void ClearHandlers() => Master.ClearHandlers();

        /// <summary>
        ///     Sending packets
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Send<T>(uint id, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => Master.Send(id, in obj);

        /// <summary>
        ///     Sending packets
        /// </summary>
        /// <param name="ids">ClientIds</param>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Broadcast<T>(uint[] ids, in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => Master.Broadcast(ids, in obj);

        /// <summary>
        ///     Sending packets
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Broadcast<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => Master.Broadcast(in obj);

        /// <summary>
        ///     Start server
        /// </summary>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public bool StartServer(ushort port, uint maxClients = 4095U) => Master.Start(port, maxClients);

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <returns>Obtained round-trip delay time</returns>
        public uint GetRoundTripTime(uint clientId) => Master.GetRoundTripTime(clientId);
    }
}