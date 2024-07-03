//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    internal sealed partial class EventManager
    {
        /// <summary>
        ///     Initialize a new instance of the event pool
        /// </summary>
        private static readonly EventPool<BaseEventArgs> EventPool = new();

        /// <summary>
        ///     Get the number of event handling functions
        /// </summary>
        public static int EventHandlerCount => EventPool.EventHandlerCount;

        /// <summary>
        ///     Obtain the number of events
        /// </summary>
        public static int EventCount => EventPool.EventCount;

        int IEventManager.EventHandlerCount => EventHandlerCount;

        int IEventManager.EventCount => EventCount;

        int IEventManager.Count(int id) => Count(id);

        bool IEventManager.Check(int id, EventHandler<BaseEventArgs> handler) => Check(id, handler);

        void IEventManager.Subscribe(int id, EventHandler<BaseEventArgs> handler) => Subscribe(id, handler);

        void IEventManager.Unsubscribe(int id, EventHandler<BaseEventArgs> handler) => Unsubscribe(id, handler);

        void IEventManager.Fire(object sender, BaseEventArgs e) => Fire(sender, e);

        /// <summary>
        ///     Close and clean up the Event Manager
        /// </summary>
        void IEventManager.Shutdown() => Shutdown();

        /// <summary>
        ///     Get the number of event handling functions
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <returns>Number of event handling functions</returns>
        public static int Count(int id) => EventPool.Count(id);

        /// <summary>
        ///     Check for the existence of event handling function
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <param name="handler">Event handling functions to be checked</param>
        /// <returns>Is there an event handling function present</returns>
        public static bool Check(int id, EventHandler<BaseEventArgs> handler) => EventPool.Check(id, handler);

        /// <summary>
        ///     Subscription event handling function
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <param name="handler">Event handling functions to subscribe to</param>
        public static void Subscribe(int id, EventHandler<BaseEventArgs> handler) => EventPool.Subscribe(id, handler);

        /// <summary>
        ///     Unsubscribe event handling function
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <param name="handler">Event handler to unsubscribe from</param>
        public static void Unsubscribe(int id, EventHandler<BaseEventArgs> handler) => EventPool.Unsubscribe(id, handler);

        /// <summary>
        ///     Throw event immediate mode, This operation is not thread safe, The incident will be immediately distributed
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event parameters</param>
        public static void Fire(object sender, BaseEventArgs e) => EventPool.FireNow(sender, e);

        /// <summary>
        ///     Close and clean up the Event Manager
        /// </summary>
        public static void Shutdown() => EventPool.Shutdown();
    }
}