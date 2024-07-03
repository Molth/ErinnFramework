//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Client Application base
    /// </summary>
    public abstract partial class ClientBase
    {
        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers<T>(T listener) where T : IClientRPCListener => _peer.RegisterHandlers(listener);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers<T>(T listener, Type type) where T : IClientRPCListener => _peer.RegisterHandlers(listener, type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers(Type type) => _peer.RegisterHandlers(type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers(Assembly assembly) => _peer.RegisterHandlers(assembly);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers() => _peer.RegisterHandlers();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <typeparam name="T">Type</typeparam>
        public void RegisterHandler<T>(Action<T> handler) where T : struct, INetworkMessage, IMemoryPackable<T> => _peer.RegisterHandler(handler);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="T">Type</typeparam>
        public void RegisterHandler<T>(Action<T> handler, bool isAllocated) where T : struct, INetworkMessage, IMemoryPackable<T> => _peer.RegisterHandler(handler, isAllocated);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers<T>(T listener) where T : IClientRPCListener => _peer.UnregisterHandlers(listener);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers<T>(T listener, Type type) where T : IClientRPCListener => _peer.UnregisterHandlers(listener, type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers(Type type) => _peer.UnregisterHandlers(type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers(Assembly assembly) => _peer.UnregisterHandlers(assembly);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers() => _peer.UnregisterHandlers();

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public void UnregisterHandler<T>() where T : struct, INetworkMessage, IMemoryPackable<T> => _peer.UnregisterHandler<T>();

        /// <summary>
        ///     Clear Command Handlers
        /// </summary>
        public void ClearHandlers() => _peer.ClearHandlers();
    }
}