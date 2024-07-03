//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    internal partial class EventManager
    {
        /// <summary>
        ///     Observer Event Dictionary
        /// </summary>
        public static readonly Dictionary<Type, ObserverContainer> ObserverDict = new();

        void IEventManager.Register<TMessage>(IMessage<TMessage> observer) => Register<TMessage>(observer.Execute);

        void IEventManager.Unregister<TMessage>(IMessage<TMessage> observer) => Unregister<TMessage>(observer.Execute);

        void IEventManager.Register<TMessage>(Action<TMessage> handler) => Register(handler);

        void IEventManager.Unregister<TMessage>(Action<TMessage> handler) => Unregister(handler);

        void IEventManager.Send<TMessage>() => Send(default(TMessage));

        void IEventManager.Send<TMessage>(TMessage message) => Send(message);

        void IEventManager.Unregister<TMessage>() => Unregister<TMessage>();

        void IEventManager.UnregisterAll() => UnregisterAll();

        /// <summary>
        ///     Subscribe to observer event
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="TMessage">Type</typeparam>
        public static void Register<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage
        {
            if (handler == null)
                return;
            var key = typeof(TMessage);
            if (ObserverDict.TryGetValue(key, out var value))
            {
                value.Register(handler);
                return;
            }

            value = ObserverContainer.Create(handler);
            ObserverDict.Add(key, value);
        }

        /// <summary>
        ///     Remove Observer Event
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="TMessage">Type</typeparam>
        public static void Unregister<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage
        {
            if (handler == null)
                return;
            var key = typeof(TMessage);
            if (ObserverDict.TryGetValue(key, out var value))
            {
                value.Unregister(handler);
                if (value.Count == 0)
                {
                    value.Dispose();
                    ObserverDict.Remove(key);
                }
            }
        }

        /// <summary>
        ///     Send messages to all observer event
        /// </summary>
        /// <param name="message">Information</param>
        /// <typeparam name="TMessage">Type</typeparam>
        public static void Send<TMessage>(TMessage message) where TMessage : struct, IMessage
        {
            var key = typeof(TMessage);
            if (ObserverDict.TryGetValue(key, out var value))
                value.Send(message);
        }

        /// <summary>
        ///     Clear Observer Event
        /// </summary>
        /// <typeparam name="TMessage">Type</typeparam>
        public static void Unregister<TMessage>() where TMessage : struct, IMessage
        {
            var key = typeof(TMessage);
            if (ObserverDict.TryGetValue(key, out var value))
            {
                value.Dispose();
                ObserverDict.Remove(key);
            }
        }

        /// <summary>
        ///     Clear all observer event
        /// </summary>
        public static void UnregisterAll()
        {
            foreach (var value in ObserverDict.Values)
                value.Dispose();
            ObserverDict.Clear();
        }
    }
}