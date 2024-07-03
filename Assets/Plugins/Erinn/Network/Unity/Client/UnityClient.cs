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
    public sealed class UnityClient : MonoBehaviour
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
        public static UnityClient Singleton { get; private set; }

        /// <summary>
        ///     Charge
        /// </summary>
        public NetworkClient Master { get; private set; }

        /// <summary>
        ///     Listening
        /// </summary>
        public bool IsListening => Master.IsListening;

        /// <summary>
        ///     Connected
        /// </summary>
        public bool Connected => Master.Connected;

        /// <summary>
        ///     Call on load
        /// </summary>
        private void Awake()
        {
            ProtocolType = _initializeProtocolType;
            Application.runInBackground = true;
            Master = new NetworkClient();
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
        public event Action OnConnectedCallback
        {
            add => Master.OnConnectedCallback += value;
            remove => Master.OnConnectedCallback -= value;
        }

        /// <summary>
        ///     Disconnect event
        /// </summary>
        public event Action OnDisconnectedCallback
        {
            add => Master.OnDisconnectedCallback += value;
            remove => Master.OnDisconnectedCallback -= value;
        }

        /// <summary>
        ///     Disconnect
        /// </summary>
        public bool Shutdown() => Master.Shutdown();

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        public void RegisterHandlers<T>(T listener) where T : IClientRPCListener => Master.RegisterHandlers(listener);

        /// <summary>
        ///     Register Command Handle
        /// </summary>
        public void RegisterHandlers<T>(T listener, Type type) where T : IClientRPCListener => Master.RegisterHandlers(listener, type);

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
        public void RegisterHandler<T>(Action<T> handler) where T : struct, INetworkMessage, IMemoryPackable<T> => Master.RegisterHandler(handler);

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        public void UnregisterHandlers<T>(T listener) where T : IClientRPCListener => Master.UnregisterHandlers(listener);

        /// <summary>
        ///     Remove Command Handle
        /// </summary>
        public void UnregisterHandlers<T>(T listener, Type type) where T : IClientRPCListener => Master.UnregisterHandlers(listener, type);

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
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        public void Send<T>(in T obj) where T : struct, INetworkMessage, IMemoryPackable<T> => Master.Send(in obj);

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">ServerIpAddress</param>
        /// <param name="port">Server Port</param>
        public bool StartClient(string ipAddress, ushort port) => Master.Start(ipAddress, port);

        /// <summary>
        ///     Obtain round-trip delay time
        /// </summary>
        /// <returns>Obtained round-trip delay time</returns>
        public uint GetRoundTripTime() => Master.GetRoundTripTime();
    }
}