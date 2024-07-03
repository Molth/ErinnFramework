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
    ///     Application base
    /// </summary>
    public abstract partial class ApplicationBase : INetworkServerMessageEndPoint, IServerRESTListener
    {
        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs<T>(T listener) where T : IServerRESTListener => _endPoint.RegisterFuncs(listener);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs<T>(T listener, Type type) where T : IServerRESTListener => _endPoint.RegisterFuncs(listener, type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs(Type type) => _endPoint.RegisterFuncs(type);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs(Assembly assembly) => _endPoint.RegisterFuncs(assembly);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs() => _endPoint.RegisterFuncs();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public void RegisterFunc<TRequest, TResponse>(Func<uint, TRequest, TResponse> handler) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => _endPoint.RegisterFunc(handler);

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public void RegisterFunc<TRequest, TResponse>(Func<uint, TRequest, TResponse> handler, bool isAllocated) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => _endPoint.RegisterFunc(handler, isAllocated);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs<T>(T listener) where T : IServerRESTListener => _endPoint.UnregisterFuncs(listener);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs<T>(T listener, Type type) where T : IServerRESTListener => _endPoint.UnregisterFuncs(listener, type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs(Type type) => _endPoint.UnregisterFuncs(type);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs(Assembly assembly) => _endPoint.UnregisterFuncs(assembly);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs() => _endPoint.UnregisterFuncs();

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        /// <typeparam name="TRequest">Type</typeparam>
        public void UnregisterFunc<TRequest>() where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> => _endPoint.UnregisterFunc<TRequest>();

        /// <summary>
        ///     Clear Command Handlers
        /// </summary>
        public void ClearFuncs() => _endPoint.ClearFuncs();

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="request">Request</param>
        /// <param name="timeout">Timeout</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public Task<MessageResult<TResponse>> SendAsync<TRequest, TResponse>(uint id, in TRequest request, int timeout = 5000) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => _endPoint.SendAsync<TRequest, TResponse>(id, in request, timeout);
    }
}