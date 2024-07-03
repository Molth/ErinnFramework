//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MemoryPack;
using Unity.Collections;
using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Client Callback
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ClientMessageAttribute : Attribute
    {
    }

    /// <summary>
    ///     Client callback interface
    /// </summary>
    public interface IClientMessage
    {
    }

    /// <summary>
    ///     Network client
    /// </summary>
    public static partial class NetcodeClient
    {
        /// <summary>
        ///     Add listening method information
        /// </summary>
        private static readonly MethodInfo RegisterMethodInfo = typeof(NetworkClient).GetMethod(nameof(RegisterHandler), BindingFlags.Static | BindingFlags.Public);

        /// <summary>
        ///     Remove listening method information
        /// </summary>
        private static readonly MethodInfo UnregisterMethodInfo = typeof(NetworkClient).GetMethod(nameof(UnregisterHandler), BindingFlags.Static | BindingFlags.Public);

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
        public static void RegisterHandlers<T>(T listener) where T : IClientMessage => RegisterHandlers(listener, listener.GetType());

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <param name="type">Specify listener type</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void RegisterHandlers<T>(T listener, Type type) where T : IClientMessage
        {
            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var clientCallbackAttribute = method.GetCustomAttribute<ClientMessageAttribute>();
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
                var clientCallbackAttribute = method.GetCustomAttribute<ClientMessageAttribute>();
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
            var types = Array.FindAll(assembly.GetTypes(), type => typeof(IClientMessage).IsAssignableFrom(type));
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
        public static void RegisterHandler<T>(Action<T> handler) where T : struct, IMemoryPackable<T>
        {
            if (handler == null)
                return;
            if (!IsClient)
                return;
            MessagingManager.RegisterNamedMessageHandler(typeof(T).FullName, WrapHandler(handler));
        }

        /// <summary>
        ///     Packaging incident
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Packaged events</returns>
        private static CustomMessagingManager.HandleNamedMessageDelegate WrapHandler<T>(Action<T> handler) where T : struct, IMemoryPackable<T>
        {
            return Wrapped;
            void Wrapped(ulong _, FastBufferReader messagePayload) => handler.Invoke(messagePayload.ReadBytesValue<T>());
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void UnregisterHandlers<T>(T listener) where T : IClientMessage => UnregisterHandlers(listener, listener.GetType());

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <param name="type">Specify listener type</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void UnregisterHandlers<T>(T listener, Type type) where T : IClientMessage
        {
            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var clientCallbackAttribute = method.GetCustomAttribute<ClientMessageAttribute>();
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
                var clientCallbackAttribute = method.GetCustomAttribute<ClientMessageAttribute>();
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
            var types = Array.FindAll(assembly.GetTypes(), type => typeof(IClientMessage).IsAssignableFrom(type));
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
            if (IsClient)
                MessagingManager.UnregisterNamedMessageHandler(typeof(T).FullName);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEvent<T>(FastBufferWriter messageStream) where T : struct, IMemoryPackable<T> => SendNamedMsg(typeof(T).FullName, messageStream);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="messageStream">FastBufferWriter</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SendEvent<T>(FastBufferWriter messageStream, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T> => SendNamedMsg(typeof(T).FullName, messageStream, networkDelivery);

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>() where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEvent<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T));
            SendEvent<T>(messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEvent<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(default(T), allocator);
            SendEvent<T>(messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(T message) where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEvent<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(T message, NetworkDelivery networkDelivery) where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message);
            SendEvent<T>(messageStream, networkDelivery);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(T message, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEvent<T>(messageStream);
        }

        /// <summary>
        ///     Sending events
        /// </summary>
        /// <param name="message">Information</param>
        /// <param name="networkDelivery">Transmission mode</param>
        /// <param name="allocator">Allocation type</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(T message, NetworkDelivery networkDelivery, Allocator allocator) where T : struct, IMemoryPackable<T>
        {
            if (!IsClient)
                return;
            var messageStream = NetcodeSerializer.GetEventWriter(message, allocator);
            SendEvent<T>(messageStream, networkDelivery);
        }
    }
}