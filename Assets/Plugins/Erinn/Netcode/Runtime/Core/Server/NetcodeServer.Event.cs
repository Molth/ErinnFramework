//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using MemoryPack;
using Unity.Collections;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Server callback
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ServerMessageAttribute : Attribute
    {
    }

    /// <summary>
    ///     Server callback interface
    /// </summary>
    public interface IServerMessage
    {
    }

    /// <summary>
    ///     Network server
    /// </summary>
    public static partial class NetcodeServer
    {
        /// <summary>
        ///     Add listening method information
        /// </summary>
        private static readonly MethodInfo RegisterMethodInfo = typeof(NetcodeServer).GetMethod(nameof(RegisterHandler), BindingFlags.Static | BindingFlags.Public);

        /// <summary>
        ///     Remove listening method information
        /// </summary>
        private static readonly MethodInfo UnregisterMethodInfo = typeof(NetcodeServer).GetMethod(nameof(UnregisterHandler), BindingFlags.Static | BindingFlags.Public);

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="firstArgument">Caller </param>
        private static void BindMethod(MethodInfo method, object firstArgument)
        {
            var messageType = method.GetParameters()[0].ParameterType;
            var genericHandlerType = typeof(Action<>).MakeGenericType(messageType);
            var handler = Delegate.CreateDelegate(genericHandlerType, firstArgument, method);
            var genericRegisterMethod = RegisterMethodInfo.MakeGenericMethod(messageType);
            genericRegisterMethod.Invoke(null, new object[] { handler });
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="firstArgument">Caller </param>
        private static void FreeMethod(MethodInfo method, object firstArgument)
        {
            var messageType = method.GetParameters()[0].ParameterType;
            var genericRegisterMethod = UnregisterMethodInfo.MakeGenericMethod(messageType);
            genericRegisterMethod.Invoke(null, null);
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void RegisterHandlers<T>(T listener) where T : IServerMessage => RegisterHandlers(listener, listener.GetType());

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <param name="type">Specify listener type</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void RegisterHandlers<T>(T listener, Type type) where T : IServerMessage
        {
            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var clientCallbackAttribute = method.GetCustomAttribute<ServerMessageAttribute>();
                if (clientCallbackAttribute != null)
                    BindMethod(method, listener);
            }
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="type">Specify listener type</param>
        public static void RegisterHandlers(Type type)
        {
            foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var clientCallbackAttribute = method.GetCustomAttribute<ServerMessageAttribute>();
                if (clientCallbackAttribute != null)
                    BindMethod(method, null);
            }
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static void RegisterHandlers(Assembly assembly)
        {
            var types = Array.FindAll(assembly.GetTypes(), type => typeof(IServerMessage).IsAssignableFrom(type));
            foreach (var type in types)
                RegisterHandlers(type);
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        public static void RegisterHandlers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                RegisterHandlers(assembly);
        }

        /// <summary>
        ///     Registration Event
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="T">Type</typeparam>
        public static void RegisterHandler<T>(Action<ulong, T> handler) where T : struct, IMemoryPackable<T>
        {
            if (handler == null)
                return;
            if (!IsServer)
                return;
            MessagingManager.RegisterNamedMessageHandler(typeof(T).FullName, WrapHandler(handler));
        }

        /// <summary>
        ///     Packaging incident
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Packaged events</returns>
        private static CustomMessagingManager.HandleNamedMessageDelegate WrapHandler<T>(Action<ulong, T> handler) where T : struct, IMemoryPackable<T>
        {
            return Wrapped;
            void Wrapped(ulong senderId, FastBufferReader messagePayload) => handler.Invoke(senderId, messagePayload.ReadBytesValue<T>());
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void UnregisterHandlers<T>(T listener) where T : IServerMessage => UnregisterHandlers(listener, listener.GetType());

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <param name="type">Specify listener type</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void UnregisterHandlers<T>(T listener, Type type) where T : IServerMessage
        {
            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var clientCallbackAttribute = method.GetCustomAttribute<ServerMessageAttribute>();
                if (clientCallbackAttribute != null)
                    BindMethod(method, listener);
            }
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="type">Specify listener type</param>
        public static void UnregisterHandlers(Type type)
        {
            foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var clientCallbackAttribute = method.GetCustomAttribute<ServerMessageAttribute>();
                if (clientCallbackAttribute != null)
                    BindMethod(method, null);
            }
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static void UnregisterHandlers(Assembly assembly)
        {
            var types = Array.FindAll(assembly.GetTypes(), type => typeof(IServerMessage).IsAssignableFrom(type));
            foreach (var type in types)
                UnregisterHandlers(type);
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        public static void UnregisterHandlers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                UnregisterHandlers(assembly);
        }

        /// <summary>
        ///     Remove event
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void UnregisterHandler<T>() where T : struct, IMemoryPackable<T>
        {
            if (IsServer)
                MessagingManager.UnregisterNamedMessageHandler(typeof(T).FullName);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEvent<T>(ulong clientId, FastBufferWriter messageStream) where T : struct, IMemoryPackable<T> => SendNamedMsg(typeof(T).FullName, clientId, messageStream);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEvent<T>(ulong clientId, FastBufferWriter messageStream, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T> => SendNamedMsg(typeof(T).FullName, clientId, messageStream, networkDelivery);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">ClientIds</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEvent<T>(IReadOnlyList<ulong> clientIds, FastBufferWriter messageStream) where T : struct, IMemoryPackable<T> => SendNamedMsg(typeof(T).FullName, clientIds, messageStream);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">ClientIds</param>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEvent<T>(IReadOnlyList<ulong> clientIds, FastBufferWriter messageStream, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T> => SendNamedMsg(typeof(T).FullName, clientIds, messageStream, networkDelivery);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEventToAll<T>(FastBufferWriter messageStream) where T : struct, IMemoryPackable<T> => SendNamedMsgToAll(typeof(T).FullName, messageStream);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEventToAll<T>(FastBufferWriter messageStream, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T> => SendNamedMsgToAll(typeof(T).FullName, messageStream, networkDelivery);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEvent<T>(clientId, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEvent<T>(clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEvent<T>(clientId, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId, NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEvent<T>(clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId, T message) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEvent<T>(clientId, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId, T message, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEvent<T>(clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId, T message, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEvent<T>(clientId, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientId">TargetId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(ulong clientId, T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEvent<T>(clientId, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEvent<T>(clientIds, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEvent<T>(clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEvent<T>(clientIds, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds, NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEvent<T>(clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds, T message) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEvent<T>(clientIds, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds, T message, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEvent<T>(clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds, T message, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEvent<T>(clientIds, messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="clientIds">All targetsId</param>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(IReadOnlyList<ulong> clientIds, T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEvent<T>(clientIds, messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>() where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEventToAll<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>(NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEventToAll<T>(messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>(Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEventToAll<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>(NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEventToAll<T>(messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>(T message) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEventToAll<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>(T message, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEventToAll<T>(messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>(T message, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEventToAll<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void SendToAll<T>(T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsServer)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEventToAll<T>(messageStream, networkDelivery);
        }
    }
}