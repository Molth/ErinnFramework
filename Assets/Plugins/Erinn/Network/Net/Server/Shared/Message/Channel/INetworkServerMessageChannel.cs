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
    ///     Network Server information channel interface
    /// </summary>
    public interface INetworkServerMessageChannel
    {
        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterHandlers<T>(T listener) where T : IServerRPCListener;

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterHandlers<T>(T listener, Type type) where T : IServerRPCListener;

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterHandlers(Type type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterHandlers(Assembly assembly);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterHandlers();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <typeparam name="T">Type</typeparam>
        void RegisterHandler<T>(Action<uint, T> handler) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="T">Type</typeparam>
        void RegisterHandler<T>(Action<uint, T> handler, bool isAllocated) where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterHandlers<T>(T listener) where T : IServerRPCListener;

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterHandlers<T>(T listener, Type type) where T : IServerRPCListener;

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterHandlers(Type type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterHandlers(Assembly assembly);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterHandlers();

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        void UnregisterHandler<T>() where T : struct, INetworkMessage, IMemoryPackable<T>;

        /// <summary>
        ///     Clear Command Handlers
        /// </summary>
        void ClearHandlers();
    }
}