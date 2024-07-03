//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading.Tasks;
#endif

namespace Erinn
{
    /// <summary>
    ///     Network Server information endPoint interface
    /// </summary>
    public interface INetworkServerMessageEndPoint
    {
        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterFuncs<T>(T listener) where T : IServerRESTListener;

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterFuncs<T>(T listener, Type type) where T : IServerRESTListener;

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterFuncs(Type type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterFuncs(Assembly assembly);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        void RegisterFuncs();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        void RegisterFunc<TRequest, TResponse>(Func<uint, TRequest, TResponse> handler) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>;

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        void RegisterFunc<TRequest, TResponse>(Func<uint, TRequest, TResponse> handler, bool isAllocated) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>;

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterFuncs<T>(T listener) where T : IServerRESTListener;

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterFuncs<T>(T listener, Type type) where T : IServerRESTListener;

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterFuncs(Type type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterFuncs(Assembly assembly);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        void UnregisterFuncs();

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        /// <typeparam name="TRequest">Type</typeparam>
        void UnregisterFunc<TRequest>() where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest>;

        /// <summary>
        ///     Clear Command Handlers
        /// </summary>
        void ClearFuncs();

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="request">Request</param>
        /// <param name="timeout">Timeout</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        Task<MessageResult<TResponse>> SendAsync<TRequest, TResponse>(uint id, in TRequest request, int timeout = 5000) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>;
    }
}