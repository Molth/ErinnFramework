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
    ///     Network client information endPoint
    /// </summary>
    public sealed class NetworkClientMessageEndPoint : INetworkClientMessageEndPoint
    {
        /// <summary>
        ///     Add listening method information
        /// </summary>
        private static readonly MethodInfo RegisterMethodInfo = typeof(NetworkClientMessageEndPoint).GetMethod(nameof(RegisterFuncInternal), BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        ///     Remove listening method information
        /// </summary>
        private static readonly MethodInfo UnregisterMethodInfo = typeof(NetworkClientMessageEndPoint).GetMethod(nameof(UnregisterFunc), BindingFlags.Instance | BindingFlags.Public);

        /// <summary>
        ///     Handler buffer
        /// </summary>
        private readonly object[] _handlerBuffer = new object[2];

        /// <summary>
        ///     NetworkClient
        /// </summary>
        private readonly NetworkClient _networkClient;

        /// <summary>
        ///     MessageEndPoint
        /// </summary>
        private readonly MessageEndPoint _messageEndPoint = new();

        /// <summary>
        ///     Client Event Handler Dictionary 32
        /// </summary>
        private readonly Dictionary<uint, NetworkClientMessageFuncProcessorBase> _requestHandlers32 = new();

        /// <summary>
        ///     Client Event Handler Dictionary 32
        /// </summary>
        private readonly Dictionary<uint, MessageEndPointResultHandlerBase> _responseHandlers32 = new();

        /// <summary>
        ///     Lock
        /// </summary>
        private readonly object _lock = new();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="networkClient">NetworkClient</param>
        public NetworkClientMessageEndPoint(NetworkClient networkClient)
        {
            _networkClient = networkClient;
            _networkClient.RegisterHandler<NetworkMessageRequest>(InvokeHandler);
            _networkClient.RegisterHandler<NetworkMessageResponse>(InvokeHandler);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs<T>(T listener) where T : IClientRESTListener => RegisterFuncs(listener, listener.GetType());

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterFuncs<T>(T listener, Type type) where T : IClientRESTListener
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRESTAttribute>();
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
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRESTAttribute>();
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
                if (typeof(IClientRESTListener).IsAssignableFrom(type) || type.GetCustomAttribute<ClientRESTListenerAttribute>() != null)
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
        public void RegisterFunc<TRequest, TResponse>(Func<TRequest, TResponse> handler) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
        {
            var restAttribute = handler.GetMethodInfo().GetCustomAttribute<ClientRESTAttribute>();
            RegisterFuncInternal(restAttribute != null && restAttribute.Allocated, handler);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public void RegisterFunc<TRequest, TResponse>(Func<TRequest, TResponse> handler, bool isAllocated) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => RegisterFuncInternal(isAllocated, handler);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs<T>(T listener) where T : IClientRESTListener => UnregisterFuncs(listener, listener.GetType());

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs<T>(T listener, Type type) where T : IClientRESTListener
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRESTAttribute>();
                if (rpcAttribute != null)
                    FreeMethod(methodInfo, listener);
            }
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterFuncs(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRESTAttribute>();
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
                if (typeof(IClientRESTListener).IsAssignableFrom(type) || type.GetCustomAttribute<ClientRESTListenerAttribute>() != null)
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
        /// <param name="request">Request</param>
        /// <param name="timeout">Timeout</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        public Task<MessageResult<TResponse>> SendAsync<TRequest, TResponse>(in TRequest request, int timeout = 5000) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse> => SendAsyncInternal<TRequest, TResponse>(request, timeout);

        /// <summary>
        ///     Send packet
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="timeout">Timeout</param>
        /// <typeparam name="TRequest">Type</typeparam>
        /// <typeparam name="TResponse">Type</typeparam>
        private async Task<MessageResult<TResponse>> SendAsyncInternal<TRequest, TResponse>(TRequest request, int timeout = 5000) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
        {
            var responseHash32 = NetworkHash.GetId32<TResponse>();
            lock (_lock)
            {
                if (!_responseHandlers32.ContainsKey(responseHash32))
                    _responseHandlers32[responseHash32] = new MessageEndPointResultHandler<TResponse>(_messageEndPoint);
            }

            var task = _messageEndPoint.AcquireAsync<TResponse>(timeout, out var serialNumber);
            if (!NetworkSerializer.Create(in request, out var networkPacket))
                return new MessageResult<TResponse>(MessageState.Nil);
            _networkClient.Send(new NetworkMessageRequest(serialNumber, networkPacket));
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
        private void RegisterFuncInternal<TRequest, TResponse>(bool isAllocated, Func<TRequest, TResponse> handler) where TRequest : struct, INetworkMessage, IMemoryPackable<TRequest> where TResponse : struct, INetworkMessage, IMemoryPackable<TResponse>
        {
            var hash32 = NetworkHash.GetId32<TRequest>();
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<TRequest>())
            {
                if (_requestHandlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkClientMessageFuncProcessor<TRequest, TResponse> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkClientMessageFuncProcessor<TRequest, TResponse>(_networkClient, handler);
                _requestHandlers32[hash32] = warpedHandler;
                Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
                return;
            }

            if (isAllocated)
            {
                if (_requestHandlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkClientMessageFuncAllocator<TRequest, TResponse> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkClientMessageFuncAllocator<TRequest, TResponse>(_networkClient, handler);
                _requestHandlers32[hash32] = warpedHandler;
                Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
            }
            else
            {
                if (_requestHandlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkClientMessageFuncProcessorPooled<TRequest, TResponse> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkClientMessageFuncProcessorPooled<TRequest, TResponse>(_networkClient, handler);
                _requestHandlers32[hash32] = warpedHandler;
                Log.Info($"Register Request: [{typeof(TRequest).FullName}] [{hash32}]");
            }
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="clientRestAttribute">RestAttribute</param>
        /// <param name="methodInfo">Method</param>
        /// <param name="firstArgument">Caller </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BindMethod(ClientRESTAttribute clientRestAttribute, MethodInfo methodInfo, object firstArgument)
        {
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            var returnType = methodInfo.ReturnType;
            var handler = Delegate.CreateDelegate(typeof(Func<,>).MakeGenericType(parameterType, returnType), firstArgument, methodInfo);
            _handlerBuffer[0] = clientRestAttribute.Allocated;
            _handlerBuffer[1] = handler;
            RegisterMethodInfo.MakeGenericMethod(parameterType, returnType).Invoke(this, _handlerBuffer);
            _handlerBuffer[0] = null;
            _handlerBuffer[1] = null;
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="methodInfo">Method</param>
        /// <param name="firstArgument">Caller </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void FreeMethod(MethodInfo methodInfo, object firstArgument)
        {
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            UnregisterMethodInfo.MakeGenericMethod(parameterType).Invoke(this, null);
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="networkMessagePacket">Network message packet</param>
        [ClientRPC(true)]
        private void InvokeHandler(NetworkMessageRequest networkMessagePacket)
        {
            var serialNumber = networkMessagePacket.SerialNumber;
            var command = networkMessagePacket.Payload.Command;
            var payload = networkMessagePacket.Payload.Payload;
            if (!_requestHandlers32.TryGetValue(command, out var handler))
                return;
            handler.Invoke(serialNumber, in payload);
        }

        /// <summary>
        ///     Call handler
        /// </summary>
        /// <param name="networkMessagePacket">Network message packet</param>
        [ClientRPC(true)]
        private void InvokeHandler(NetworkMessageResponse networkMessagePacket)
        {
            var serialNumber = networkMessagePacket.SerialNumber;
            var command = networkMessagePacket.Payload.Command;
            var payload = networkMessagePacket.Payload.Payload;
            if (!_messageEndPoint.Checked(serialNumber))
                return;
            if (!_responseHandlers32.TryGetValue(command, out var handler))
                return;
            handler.SetResult(serialNumber, in payload);
        }
    }
}