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
#endif

#pragma warning disable CS8601
#pragma warning disable CS8625
#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     Network client information channel
    /// </summary>
    public sealed class NetworkClientMessageChannel : INetworkClientMessageChannel
    {
        /// <summary>
        ///     Add listening method information
        /// </summary>
        private static readonly MethodInfo RegisterMethodInfo = typeof(NetworkClientMessageChannel).GetMethod(nameof(RegisterHandlerInternal), BindingFlags.Instance | BindingFlags.NonPublic);

        /// <summary>
        ///     Remove listening method information
        /// </summary>
        private static readonly MethodInfo UnregisterMethodInfo = typeof(NetworkClientMessageChannel).GetMethod(nameof(UnregisterHandler), BindingFlags.Instance | BindingFlags.Public);

        /// <summary>
        ///     Handler buffer
        /// </summary>
        private readonly object[] _handlerBuffer = new object[2];

        /// <summary>
        ///     Client Event Handler Dictionary 32
        /// </summary>
        private readonly Dictionary<uint, NetworkClientMessageProcessorBase> _handlers32 = new();

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers<T>(T listener) where T : IClientRPCListener => RegisterHandlers(listener, listener.GetType());

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers<T>(T listener, Type type) where T : IClientRPCListener
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRPCAttribute>();
                if (rpcAttribute != null)
                    BindMethod(rpcAttribute, methodInfo, listener);
            }
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRPCAttribute>();
                if (rpcAttribute != null)
                    BindMethod(rpcAttribute, methodInfo, null);
            }
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (typeof(IClientRPCListener).IsAssignableFrom(type) || type.GetCustomAttribute<ClientRPCListenerAttribute>() != null)
                    RegisterHandlers(type);
            }
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        public void RegisterHandlers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                RegisterHandlers(assembly);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <typeparam name="T">Type</typeparam>
        public void RegisterHandler<T>(Action<T> handler) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var rpcAttribute = handler.GetMethodInfo().GetCustomAttribute<ClientRPCAttribute>();
            RegisterHandlerInternal(rpcAttribute != null && rpcAttribute.Allocated, handler);
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="handler">Handler</param>
        /// <param name="isAllocated">Allocated</param>
        /// <typeparam name="T">Type</typeparam>
        public void RegisterHandler<T>(Action<T> handler, bool isAllocated) where T : struct, INetworkMessage, IMemoryPackable<T> => RegisterHandlerInternal(isAllocated, handler);

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers<T>(T listener) where T : IClientRPCListener => UnregisterHandlers(listener, listener.GetType());

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers<T>(T listener, Type type) where T : IClientRPCListener
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRPCAttribute>();
                if (rpcAttribute != null)
                    FreeMethod(methodInfo, listener);
            }
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var rpcAttribute = methodInfo.GetCustomAttribute<ClientRPCAttribute>();
                if (rpcAttribute != null)
                    FreeMethod(methodInfo, null);
            }
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (typeof(IClientRPCListener).IsAssignableFrom(type) || type.GetCustomAttribute<ClientRPCListenerAttribute>() != null)
                    UnregisterHandlers(type);
            }
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        public void UnregisterHandlers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                UnregisterHandlers(assembly);
        }

        /// <summary>
        ///     Remove Command Handler
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public void UnregisterHandler<T>() where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var hash32 = NetworkHash.GetId32<T>();
            if (!_handlers32.Remove(hash32, out var warpedHandler))
                return;
            warpedHandler.Dispose();
            Log.Info($"Unregister Command: [{typeof(T).FullName}] [{hash32}]");
        }

        /// <summary>
        ///     Clear Command Handlers
        /// </summary>
        public void ClearHandlers()
        {
            foreach (var warpedHandler in _handlers32.Values)
                warpedHandler.Dispose();
            _handlers32.Clear();
            Log.Info("Clear Commands");
        }

        /// <summary>
        ///     Register Command Handler
        /// </summary>
        /// <param name="isAllocated">Allocated</param>
        /// <param name="handler">Handler</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RegisterHandlerInternal<T>(bool isAllocated, Action<T> handler) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var hash32 = NetworkHash.GetId32<T>();
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                if (_handlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    var value = (NetworkClientMessageProcessor<T>)warpedHandler;
                    value.Replace(hash32, handler);
                    return;
                }

                warpedHandler = new NetworkClientMessageProcessor<T>(handler);
                _handlers32[hash32] = warpedHandler;
                Log.Info($"Register Command: [{typeof(T).FullName}] [{hash32}]");
                return;
            }

            if (isAllocated)
            {
                if (_handlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkClientMessageAllocator<T> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkClientMessageAllocator<T>(handler);
                _handlers32[hash32] = warpedHandler;
                Log.Info($"Register Command: [{typeof(T).FullName}] [{hash32}]");
            }
            else
            {
                if (_handlers32.TryGetValue(hash32, out var warpedHandler))
                {
                    if (warpedHandler is NetworkClientMessageProcessorPooled<T> value)
                    {
                        value.Replace(hash32, handler);
                        return;
                    }
                }

                warpedHandler = new NetworkClientMessageProcessorPooled<T>(handler);
                _handlers32[hash32] = warpedHandler;
                Log.Info($"Register Command: [{typeof(T).FullName}] [{hash32}]");
            }
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="clientRpcAttribute">RpcAttribute</param>
        /// <param name="methodInfo">Method</param>
        /// <param name="firstArgument">Caller </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void BindMethod(ClientRPCAttribute clientRpcAttribute, MethodInfo methodInfo, object firstArgument)
        {
            var parameterType = methodInfo.GetParameters()[0].ParameterType;
            var handler = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(parameterType), firstArgument, methodInfo);
            _handlerBuffer[0] = clientRpcAttribute.Allocated;
            _handlerBuffer[1] = handler;
            RegisterMethodInfo.MakeGenericMethod(parameterType).Invoke(this, _handlerBuffer);
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
        /// <param name="networkPacket">NetworkPacket</param>
        public void InvokeHandler(in NetworkPacket networkPacket)
        {
            if (!_handlers32.TryGetValue(networkPacket.Command, out var handler))
                return;
            handler.Invoke(in networkPacket.Payload);
        }
    }
}