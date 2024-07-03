//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8618

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public abstract partial class NetworkServerPeer
    {
        /// <summary>
        ///     Information channel
        /// </summary>
        private readonly NetworkServerMessageChannel _messageChannel;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="messageChannel">Information channel</param>
        protected NetworkServerPeer(NetworkServerMessageChannel messageChannel) => _messageChannel = messageChannel;

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers<T>(T listener) where T : IServerRPCListener => _messageChannel.RegisterHandlers(listener);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers<T>(T listener, Type type) where T : IServerRPCListener => _messageChannel.RegisterHandlers(listener, type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers(Type type) => _messageChannel.RegisterHandlers(type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers(Assembly assembly) => _messageChannel.RegisterHandlers(assembly);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers() => _messageChannel.RegisterHandlers();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <typeparam name="T">Type</typeparam>
        public void RegisterHandler<T>(Action<uint, T> handler) where T : struct, INetworkMessage, IMemoryPackable<T> => _messageChannel.RegisterHandler(handler);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="T">Type</typeparam>
        public void RegisterHandler<T>(Action<uint, T> handler, bool isAllocated) where T : struct, INetworkMessage, IMemoryPackable<T> => _messageChannel.RegisterHandler(handler, isAllocated);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers<T>(T listener) where T : IServerRPCListener => _messageChannel.UnregisterHandlers(listener);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers<T>(T listener, Type type) where T : IServerRPCListener => _messageChannel.UnregisterHandlers(listener, type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers(Type type) => _messageChannel.UnregisterHandlers(type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers(Assembly assembly) => _messageChannel.UnregisterHandlers(assembly);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers() => _messageChannel.UnregisterHandlers();

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public void UnregisterHandler<T>() where T : struct, INetworkMessage, IMemoryPackable<T> => _messageChannel.UnregisterHandler<T>();

        /// <summary>
        ///     Clear Command Handlers
        /// </summary>
        public void ClearHandlers() => _messageChannel.ClearHandlers();

        /// <summary>
        ///     Call Handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="networkPacket">NetworkPacket</param>
        protected void InvokeHandler(uint id, in NetworkPacket networkPacket) => _messageChannel.InvokeHandler(id, in networkPacket);
    }
}