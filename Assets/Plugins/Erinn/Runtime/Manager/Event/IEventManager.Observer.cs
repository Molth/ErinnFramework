//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    public partial interface IEventManager
    {
        /// <summary>
        ///     Subscribe to observer event
        /// </summary>
        /// <param name="observer">Observer</param>
        /// <typeparam name="TMessage">Type</typeparam>
        void Register<TMessage>(IMessage<TMessage> observer) where TMessage : struct, IMessage;

        /// <summary>
        ///     Remove Observer's Event
        /// </summary>
        /// <param name="observer">Observer</param>
        /// <typeparam name="TMessage">Type</typeparam>
        void Unregister<TMessage>(IMessage<TMessage> observer) where TMessage : struct, IMessage;

        /// <summary>
        ///     Subscribe to observer event
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="TMessage">Type</typeparam>
        void Register<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage;

        /// <summary>
        ///     Remove Observer Event
        /// </summary>
        /// <param name="handler">Event</param>
        /// <typeparam name="TMessage">Type</typeparam>
        void Unregister<TMessage>(Action<TMessage> handler) where TMessage : struct, IMessage;

        /// <summary>
        ///     Send messages to all observer event
        /// </summary>
        /// <typeparam name="TMessage">Type</typeparam>
        void Send<TMessage>() where TMessage : struct, IMessage;

        /// <summary>
        ///     Send messages to all observer event
        /// </summary>
        /// <param name="message">Information</param>
        /// <typeparam name="TMessage">Type</typeparam>
        void Send<TMessage>(TMessage message) where TMessage : struct, IMessage;

        /// <summary>
        ///     Clear Observer Event
        /// </summary>
        /// <typeparam name="TMessage">Type</typeparam>
        void Unregister<TMessage>() where TMessage : struct, IMessage;

        /// <summary>
        ///     Clear all observer event
        /// </summary>
        void UnregisterAll();
    }
}