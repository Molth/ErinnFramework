//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Erinn
{
    /// <summary>
    ///     Information system
    /// </summary>
    public static class MessageSystem
    {
        /// <summary>
        ///     Information Dictionary
        /// </summary>
        private static readonly Dictionary<Type, Delegate> MessageDictionary = new();

        /// <summary>
        ///     Add listening method information
        /// </summary>
        private static readonly MethodInfo RegisterMethodInfo = typeof(MessageSystem).GetMethod(nameof(Register), BindingFlags.Static | BindingFlags.Public);

        /// <summary>
        ///     Remove listening method information
        /// </summary>
        private static readonly MethodInfo UnregisterMethodInfo = typeof(MessageSystem).GetMethod(nameof(Unregister), BindingFlags.Static | BindingFlags.Public);

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="methodInfo">MethodInfo</param>
        /// <param name="firstArgument">Caller </param>
        private static void BindMethod(MethodInfo methodInfo, object firstArgument)
        {
            var messageType = methodInfo.GetParameters()[0].ParameterType;
            var genericHandlerType = typeof(Action<>).MakeGenericType(messageType);
            var handler = Delegate.CreateDelegate(genericHandlerType, firstArgument, methodInfo);
            var genericRegisterMethod = RegisterMethodInfo.MakeGenericMethod(messageType);
            genericRegisterMethod.Invoke(null, new object[] { handler });
        }

        /// <summary>
        ///     Free listening
        /// </summary>
        /// <param name="methodInfo">MethodInfo</param>
        private static void FreeMethod(MethodInfo methodInfo)
        {
            var messageType = methodInfo.GetParameters()[0].ParameterType;
            var genericRegisterMethod = UnregisterMethodInfo.MakeGenericMethod(messageType);
            genericRegisterMethod.Invoke(null, new object[] { methodInfo });
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void Registers<T>(T listener) where T : IMessageCallback => Registers(listener, listener.GetType());

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <param name="type">Specify listener type</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void Registers<T>(T listener, Type type) where T : IMessageCallback
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var messageAttribute = methodInfo.GetCustomAttribute<MessageCallbackAttribute>();
                if (messageAttribute != null)
                    BindMethod(methodInfo, listener);
            }
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="type">Specify listener type</param>
        public static void Registers(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var messageAttribute = methodInfo.GetCustomAttribute<MessageCallbackAttribute>();
                if (messageAttribute != null)
                    BindMethod(methodInfo, null);
            }
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static void Registers(Assembly assembly)
        {
            var types = Array.FindAll(assembly.GetTypes(), type => typeof(IMessageCallback).IsAssignableFrom(type) || type.GetCustomAttribute<MessageCallbackListenerAttribute>() != null);
            foreach (var type in types)
                Registers(type);
        }

        /// <summary>
        ///     Bind listening
        /// </summary>
        public static void Registers()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                Registers(assembly);
        }

        /// <summary>
        ///     Add listening
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Register<T>(Action<T> handler) where T : struct, IMessage => MessageDictionary[typeof(T)] = handler;

        /// <summary>
        ///     Free listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void Unregisters<T>(T listener) where T : IMessageCallback => Unregisters(listener, listener.GetType());

        /// <summary>
        ///     Free listening
        /// </summary>
        /// <param name="listener">Monitor</param>
        /// <param name="type">Specify listener type</param>
        /// <typeparam name="T">Any listener type</typeparam>
        public static void Unregisters<T>(T listener, Type type) where T : IMessageCallback
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var messageAttribute = methodInfo.GetCustomAttribute<MessageCallbackAttribute>();
                if (messageAttribute != null)
                    FreeMethod(methodInfo);
            }
        }

        /// <summary>
        ///     Free listening
        /// </summary>
        /// <param name="type">Specify listener type</param>
        public static void Unregisters(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var messageAttribute = methodInfo.GetCustomAttribute<MessageCallbackAttribute>();
                if (messageAttribute != null)
                    FreeMethod(methodInfo);
            }
        }

        /// <summary>
        ///     Free listening
        /// </summary>
        /// <param name="assembly">Assembly</param>
        public static void Unregisters(Assembly assembly)
        {
            var types = Array.FindAll(assembly.GetTypes(), type => typeof(IMessageCallback).IsAssignableFrom(type) || type.GetCustomAttribute<MessageCallbackListenerAttribute>() != null);
            foreach (var type in types)
                Unregisters(type);
        }

        /// <summary>
        ///     Free listening
        /// </summary>
        public static void Unregisters()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
                Unregisters(assembly);
        }

        /// <summary>
        ///     Remove listening
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void Unregister<T>() where T : struct, IMessage => MessageDictionary.Remove(typeof(T));

        /// <summary>
        ///     Check listening
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static bool Check<T>() where T : struct, IMessage => MessageDictionary.ContainsKey(typeof(T));

        /// <summary>
        ///     Get the current number of listeners
        /// </summary>
        /// <returns>Current listening quantity</returns>
        public static int Count() => MessageDictionary.Count;

        /// <summary>
        ///     Sending messages
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>() where T : struct, IMessage => Send<T>(default);

        /// <summary>
        ///     Sending messages
        /// </summary>
        /// <param name="message">Information</param>
        /// <typeparam name="T">Type</typeparam>
        public static void Send<T>(T message) where T : struct, IMessage
        {
            if (!MessageDictionary.TryGetValue(typeof(T), out var value))
                return;
            var handler = (Action<T>)value;
            handler.Invoke(message);
        }

        /// <summary>
        ///     Clear listening
        /// </summary>
        public static void Clear() => MessageDictionary.Clear();
    }
}