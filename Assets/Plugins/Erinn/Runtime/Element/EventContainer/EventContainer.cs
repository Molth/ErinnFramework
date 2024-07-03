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
    ///     Event container
    ///     UseTypeDetermine type
    ///     Not involving disassembly and assembly of boxes
    /// </summary>
    internal sealed class EventContainer : IReference
    {
        /// <summary>
        ///     Event type
        /// </summary>
        [ShowInInspector] private Type _eventType;

        /// <summary>
        ///     Entrust
        /// </summary>
        [ShowInInspector] private readonly HashSet<Delegate> _delegates;

        /// <summary>
        ///     Entrust
        /// </summary>
        private Delegate _combinedEvent;

        /// <summary>
        ///     Constructor
        /// </summary>
        public EventContainer()
        {
            _eventType = null;
            _delegates = new HashSet<Delegate>();
            _combinedEvent = null;
        }

        /// <summary>
        ///     Number of events
        /// </summary>
        public int Count => _delegates.Count;

        /// <summary>
        ///     Clear Event
        /// </summary>
        void IReference.Clear()
        {
            _eventType = null;
            _delegates.Clear();
            _combinedEvent = null;
        }

        /// <summary>
        ///     Add event
        /// </summary>
        /// <param name="action">Entrust</param>
        public void Add<T>(T action) where T : Delegate
        {
            if (_eventType != typeof(T))
                return;
            if (_delegates.Add(action))
                _combinedEvent = Delegate.Combine(_combinedEvent, action);
        }

        /// <summary>
        ///     Remove event
        /// </summary>
        /// <param name="action">Entrust</param>
        public void Remove<T>(T action) where T : Delegate
        {
            if (_eventType != typeof(T))
                return;
            if (_delegates.Remove(action))
                _combinedEvent = Delegate.Remove(_combinedEvent, action);
        }

        /// <summary>
        ///     Attempt to convert
        /// </summary>
        /// <param name="action">Event</param>
        /// <typeparam name="T">Any type of delegation</typeparam>
        /// <returns>Obtained commission</returns>
        public bool TryGetValue<T>(out T action) where T : Delegate
        {
            if (_eventType == typeof(T))
            {
                action = (T)_combinedEvent;
                return true;
            }

            action = null;
            return false;
        }

        /// <summary>
        ///     Empty
        /// </summary>
        public void Dispose() => ReferencePool.Release(this);

        /// <summary>
        ///     Event container initialization
        /// </summary>
        /// <param name="action">Event</param>
        /// <typeparam name="T">Event type</typeparam>
        public void Init<T>(T action) where T : Delegate
        {
            _eventType = typeof(T);
            _delegates.Add(action);
            _combinedEvent = action;
        }

        /// <summary>
        ///     Generate Event Container
        /// </summary>
        /// <param name="action">Event</param>
        /// <returns>New Event Container</returns>
        public static EventContainer Create<T>(T action) where T : Delegate
        {
            var container = ReferencePool.Acquire<EventContainer>();
            container.Init(action);
            return container;
        }
    }
}