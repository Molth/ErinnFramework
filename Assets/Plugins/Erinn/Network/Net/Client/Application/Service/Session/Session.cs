//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8618

namespace Erinn
{
    /// <summary>
    ///     Service Session
    /// </summary>
    public abstract partial class Session : ISession, IClientRPCListener, IClientRESTListener
    {
        /// <summary>
        ///     Network client
        /// </summary>
        private NetworkClient _networkClient;

        /// <summary>
        ///     EndPoint
        /// </summary>
        private NetworkClientMessageEndPoint _endPoint;

        /// <summary>
        ///     Stay connected
        /// </summary>
        protected bool Connected => _networkClient.Connected;

        /// <summary>
        ///     Call on connected
        /// </summary>
        void ISession.OnConnected() => OnConnected();

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        void ISession.OnDisconnected() => OnDisconnected();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void ISession.RegisterHandlers(NetworkClient networkClient, NetworkClientMessageEndPoint endPoint, Action<uint, NetworkClientServiceHandler> addHandlerCallback, Action<uint, NetworkClientServiceFuncBase> addFuncCallback)
        {
            _networkClient = networkClient;
            _endPoint = endPoint;
            var type = GetType();
            RegisterHandlers(type, addHandlerCallback, addFuncCallback);
        }

        /// <summary>
        ///     Unregister Command Handler
        /// </summary>
        void ISession.UnregisterHandlers(Action<uint> removeHandlerCallback, Action<uint> removeFuncCallback)
        {
            var type = GetType();
            UnregisterHandlers(type, removeHandlerCallback, removeFuncCallback);
        }

        /// <summary>
        ///     Call on connected
        /// </summary>
        protected abstract void OnConnected();

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        protected abstract void OnDisconnected();
    }
}