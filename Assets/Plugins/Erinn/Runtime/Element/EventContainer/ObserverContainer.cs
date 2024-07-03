//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Erinn
{
    /// <summary>
    ///     Observer Event Container
    ///     Externally usedTypeSpecify type
    ///     Not involving disassembly and assembly of boxes
    /// </summary>
    internal sealed class ObserverContainer : IReference
    {
        /// <summary>
        ///     Hash table for storing observer events
        /// </summary>
        [ShowInInspector] private readonly HashSet<Delegate> _observerEvents;

        /// <summary>
        ///     Merged events
        /// </summary>
        private Delegate _combinedEvent;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ObserverContainer()
        {
            _observerEvents = new HashSet<Delegate>();
            _combinedEvent = null;
        }

        /// <summary>
        ///     The number of elements in the hash table that store the events of the observer
        /// </summary>
        public int Count => _observerEvents.Count;

        /// <summary>
        ///     Clear Observer's Events
        /// </summary>
        void IReference.Clear()
        {
            _observerEvents.Clear();
            _combinedEvent = null;
        }

        /// <summary>
        ///     Generic event container subscribes to observer events
        /// </summary>
        /// <param name="observerEvent">Event passed to observer</param>
        /// <returns>If not repeated, add successfully</returns>
        public void Register<TMessage>(Action<TMessage> observerEvent) where TMessage : IMessage
        {
            if (_observerEvents.Add(observerEvent))
                _combinedEvent = Delegate.Combine(_combinedEvent, observerEvent);
        }

        /// <summary>
        ///     Generic event container removes observer events
        /// </summary>
        /// <param name="observerEvent">Event passed to observer</param>
        /// <returns>If it already exists, remove successfully</returns>
        public void Unregister<TMessage>(Action<TMessage> observerEvent) where TMessage : IMessage
        {
            if (_observerEvents.Remove(observerEvent))
                _combinedEvent = Delegate.Remove(_combinedEvent, observerEvent);
        }

        /// <summary>
        ///     Generic event container calls events
        /// </summary>
        /// <param name="message">Information</param>
        public void Send<TMessage>(TMessage message) where TMessage : IMessage => ((Action<TMessage>)_combinedEvent).Invoke(message);

        /// <summary>
        ///     Release
        /// </summary>
        public void Dispose() => ReferencePool.Release(this);

        /// <summary>
        ///     Observer's event container initialization
        /// </summary>
        /// <param name="observerEvent">Event</param>
        /// <typeparam name="TMessage">Type</typeparam>
        public void Init<TMessage>(Action<TMessage> observerEvent) where TMessage : IMessage
        {
            _observerEvents.Add(observerEvent);
            _combinedEvent = observerEvent;
        }

        /// <summary>
        ///     Generate event containers for observers
        /// </summary>
        /// <param name="observerEvent">Observer's events</param>
        /// <returns>Observer's Event Container</returns>
        public static ObserverContainer Create<TMessage>(Action<TMessage> observerEvent) where TMessage : IMessage
        {
            var container = ReferencePool.Acquire<ObserverContainer>();
            container.Init(observerEvent);
            return container;
        }
    }
}