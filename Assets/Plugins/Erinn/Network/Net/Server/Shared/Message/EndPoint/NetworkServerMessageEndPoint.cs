//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// ReSharper disable PossibleNullReferenceException
#endif

#pragma warning disable CS8600
#pragma warning disable CS8601
#pragma warning disable CS8604
#pragma warning disable CS8625

namespace Erinn
{
    /// <summary>
    ///     Network server information endPoint
    /// </summary>
    public sealed class NetworkServerMessageEndPoint : INetworkServerMessageEndPoint
    {
        /// <summary>
        ///     Add listening method information
        /// </summary>
        private static readonly MethodInfo RegisterMethodInfo = typeof(NetworkServerMessageEndPoint).GetMethod(nameof(RegisterFuncInternal), BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        ///     Remove listening method information
        /// </summary>
        private static readonly MethodInfo UnregisterMethodInfo = typeof(NetworkServerMessageEndPoint).GetMethod(nameof(UnregisterFunc), BindingFlags.Instance | BindingFlags.Public);

        /// <summary>
        ///     Handler buffer
        /// </summary>
        private readonly object[] _handlerBuffer = new object[2];

        /// <summary>
        ///     NetworkServer
        /// </summary>
        private readonly NetworkServer _networkServer;

        /// <summary>
        ///     MessageEndPoint
        /// </summary>
        private readonly MessageEndPoint _messageEndPoint = new();

        /// <summary>
        ///     Server Event Handler Dictionary 32
        /// </summary>
        private readonly Dictionary<uint, NetworkServerMessageFuncProcessorBase> _requestHandlers32 = new();

        /// <summary>
        ///     Server Event Handler Dictionary 32
        /// </summary>
        private readonly Dictionary<uint, MessageEndPointResultHandlerBase> _responseHandlers32 = new();

        /// <summary>
        ///     Lock
        /// </summary>
        private readonly object _lock = new();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="networkServer">NetworkServer</param>
        public NetworkServerMessageEndPoint(NetworkServer networkServer)
        {
            _networkServer = networkServer;
            _networkServer.RegisterHandler<NetworkMessageRequest>(InvokeHandler);
            _networkServer.RegisterHandler<NetworkMessageResponse>(InvokeHandler);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs<T>(T listener) where T : IServerRESTListener => RegisterFuncs(listener, listener.GetType());

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs<T>(T listener, Type type) where T : IServerRESTListener
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ServerRESTAttribute>();
                if (rpcAttribute != null)
                    BindMethod(rpcAttribute, methodInfo, listener);
            }
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ServerRESTAttribute>();
                if (rpcAttribute != null)
                    BindMethod(rpcAttribute, methodInfo, null);
            }
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (typeof(IServerRESTListener).IsAssignableFrom(type) || type.GetCustomAttribute<ServerRESTListenerAttribute>() != null)
                    RegisterFuncs(type);
            }
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                RegisterFuncs(assembly);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public void RegisterFunc<TRequest, TResponse>(Func<uint, TRequest, TResponse> handler) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
        {
            var restAttribute = handler.GetMethodInfo().GetCustomAttribute<ServerRESTAttribute>();
            RegisterFuncInternal(restAttribute != null && restAttribute.Allocated, handler);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public void RegisterFunc<TRequest, TResponse>(Func<uint, TRequest, TResponse> handler, bool isAllocated) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => RegisterFuncInternal(isAllocated, handler);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs<T>(T listener) where T : IServerRESTListener => UnregisterFuncs(listener, listener.GetType());

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs<T>(T listener, Type type) where T : IServerRESTListener
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ServerRESTAttribute>();
                if (rpcAttribute != null)
                    FreeMethod(methodInfo, null);
            }
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ServerRESTAttribute>();
                if (rpcAttribute != null)
                    FreeMethod(methodInfo, null);
            }
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (typeof(IServerRESTListener).IsAssignableFrom(type) || type.GetCustomAttribute<ServerRESTListenerAttribute>() != null)
                    UnregisterFuncs(type);
            }
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                UnregisterFuncs(assembly);
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        /// <typeparam name="TRequest">Type</typeparam>
        public void UnregisterFunc<TRequest>() where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest>
        {
            var hash32 = NetworkHash.GetId32<TRequest>();
            if (!_requestHandlers32.Remove(hash32, out var warpedHandler))
                return;
            warpedHandler.Dispose();
            Log.Info($"Unregister Request: [{typeof(TRequest).FullName}] [{hash32}]");
        }

        /// <summary>
        ///     Clear Command Handlers
        /// </summary>
        public void ClearFuncs()
        {
            foreach (var warpedHandler in _requestHandlers32.Values)
                warpedHandler.Dispose();
            _requestHandlers32.Clear();
            Log.Info("Clear Requests");
        }

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="request">Request</param>
        /// <param name="timeout">Timeout</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public Task<MessageResult<TResponse>> SendAsync<TRequest, TResponse>(uint id, in TRequest request, int timeout = 5000) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => SendAsyncInternal<TRequest, TResponse>(id, request, timeout);

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="request">Request</param>
        /// <param name="timeout">Timeout</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        private async Task<MessageResult<TResponse>> SendAsyncInternal<TRequest, TResponse>(uint id, TRequest request, int timeout) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
        {
            if (!_networkServer.GetConnection(id, out var connection))
                return new MessageResult<TResponse>(MessageState.Nil);
            var responseHash32 = NetworkHash.GetId32<TResponse>();
            lock (_lock)
            {
                if (!_responseHandlers32.ContainsKey(responseHash32))
                    _responseHandlers32[responseHash32] = new MessageEndPointResultHandler<TResponse>(_messageEndPoint);
            }

            var cookie = connection.GetHashCode();
            var task = _messageEndPoint.AcquireAsync<TResponse>(timeout, cookie, out var serialNumber);
            if (!NetworkSerializer.Create(in request, out var networkPacket))
                return new MessageResult<TResponse>(MessageState.Nil);
            _networkServer.Send(id, new NetworkMessageRequest(serialNumber, networkPacket));
            await task;
            return task.Result;
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="isAllocated">Allocated</param>
        /// <param name="handler">Handler</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RegisterFuncInternal<TRequest, TResponse>(bool isAllocated, Func<uint, TRequest, TResponse> handler) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
        {
            var hash32 = NetworkHash.GetId32<TRequest>();
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<TRequest>())
            {
                if (_requestHandlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkServerMessageFuncProcessor<TRequest, TResponse> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkServerMessageFuncProcessor<TRequest, TResponse>(_networkServer, handler);
                _requestHandlers32[hash32] = warpedHandler;
                Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
                return;
            }

            if (isAllocated)
            {
                if (_requestHandlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkServerMessageFuncAllocator<TRequest, TResponse> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkServerMessageFuncAllocator<TRequest, TResponse>(_networkServer, handler);
                _requestHandlers32[hash32] = warpedHandler;
                Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
            }
            else
            {
                if (_requestHandlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkServerMessageFuncProcessorPooled<TRequest, TResponse> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkServerMessageFuncProcessorPooled<TRequest, TResponse>(_networkServer, handler);
                _requestHandlers32[hash32] = warpedHandler;
                Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
            }
        }

        /// <summary>
        ///     Bind listening method
        /// </summary>
        /// <param name="serverRpcAttribute">RestAttribute</param>
        /// <param name="methodInfo">Method</param>
        /// <param name="firstArgument">Caller </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BindMethod(ServerRESTAttribute serverRpcAttribute, MethodInfo methodInfo, object firstArgument)
        {
            var parameterType = methodInfo.GetParameters()[1].ParameterType;
            var returnType = methodInfo.ReturnType;
            var handler = Delegate.CreateDelegate(typeof(Func<,,>).MakeGenericType(typeof(uint), parameterType, returnType), firstArgument, methodInfo);
            _handlerBuffer[0] = serverRpcAttribute.Allocated;
            _handlerBuffer[1] = handler;
            RegisterMethodInfo.MakeGenericMethod(parameterType, returnType).Invoke(this, _handlerBuffer);
            _handlerBuffer[0] = null;
            _handlerBuffer[1] = null;
        }

        /// <summary>
        ///     Remove listening method
        /// </summary>
        /// <param name="methodInfo">Method</param>
        /// <param name="firstArgument">Caller </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FreeMethod(MethodInfo methodInfo, object firstArgument)
        {
            var parameterType = methodInfo.GetParameters()[1].ParameterType;
            UnregisterMethodInfo.MakeGenericMethod(parameterType).Invoke(this, null);
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="networkMessagePacket">Network message packet</param>
        [ServerRPC(true)]
        private void InvokeHandler(uint id, NetworkMessageRequest networkMessagePacket)
        {
            var serialNumber = networkMessagePacket.SerialNumber;
            var command = networkMessagePacket.Payload.Command;
            var payload = networkMessagePacket.Payload.Payload;
            if (!_requestHandlers32.TryGetValue(command, out var handler))
                return;
            handler.Invoke(id, serialNumber, in payload);
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="networkMessagePacket">Network message packet</param>
        [ServerRPC(true)]
        private void InvokeHandler(uint id, NetworkMessageResponse networkMessagePacket)
        {
            if (!_networkServer.GetConnection(id, out var connection))
                return;
            var serialNumber = networkMessagePacket.SerialNumber;
            var command = networkMessagePacket.Payload.Command;
            var payload = networkMessagePacket.Payload.Payload;
            if (!_messageEndPoint.Checked(serialNumber))
                return;
            if (!_responseHandlers32.TryGetValue(command, out var handler))
                return;
            var cookie = connection.GetHashCode();
            handler.SetResult(serialNumber, cookie, in payload);
        }
    }
}