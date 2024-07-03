//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Event Pool
    /// </summary>
    /// <typeparam name="T">Event type</typeparam>
    internal sealed partial class EventPool<T> where T : BaseEventArgs
    {
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> _cachedNodes;
        private readonly ErinnMultiDictionary<int, EventHandler<T>> _eventHandlers;
        private readonly Queue<EventNode> _events;
        private readonly Dictionary<object, LinkedListNode<EventHandler<T>>> _tempNodes;

        /// <summary>
        ///     Initialize a new instance of the event pool
        /// </summary>
        public EventPool()
        {
            _eventHandlers = new ErinnMultiDictionary<int, EventHandler<T>>();
            _events = new Queue<EventNode>();
            _cachedNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
            _tempNodes = new Dictionary<object, LinkedListNode<EventHandler<T>>>();
        }

        /// <summary>
        ///     Get the number of event handling functions
        /// </summary>
        public int EventHandlerCount => _eventHandlers.Count;

        /// <summary>
        ///     Obtain the number of events
        /// </summary>
        public int EventCount
        {
            get
            {
                lock (_events)
                {
                    return _events.Count;
                }
            }
        }

        /// <summary>
        ///     Event Pool Polling
        /// </summary>
        public void OnUpdate()
        {
            lock (_events)
            {
                while (_events.Count > 0)
                {
                    var eventNode = _events.Dequeue();
                    HandleEvent(eventNode.Sender, eventNode.EventArgs);
                }
            }
        }

        /// <summary>
        ///     Close and clean up the event pool
        /// </summary>
        public void Shutdown()
        {
            Clear();
            _eventHandlers.Clear();
            _cachedNodes.Clear();
            _tempNodes.Clear();
        }

        /// <summary>
        ///     Clean up events
        /// </summary>
        public void Clear()
        {
            lock (_events)
            {
                _events.Clear();
            }
        }

        /// <summary>
        ///     Get the number of event handling functions
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <returns>Number of event handling functions</returns>
        public int Count(int id) => _eventHandlers.TryGetValue(id, out var range) ? range.Count : 0;

        /// <summary>
        ///     Check for the existence of event handling functions
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <param name="handler">Event handling functions to be checked</param>
        /// <returns>Is there an event handling function present</returns>
        public bool Check(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                Log.Error("Event handler is invalid.");
                return false;
            }

            return _eventHandlers.Contains(id, handler);
        }

        /// <summary>
        ///     Subscription event handling function
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <param name="handler">Event handling functions to subscribe to</param>
        public void Subscribe(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                Log.Error("Event handler is invalid.");
                return;
            }

            if (!_eventHandlers.ContainsKey(id))
                _eventHandlers.Add(id, handler);
            else if (Check(id, handler))
                Log.Warning($"Event '{id}' not allow duplicate handler.");
            else
                _eventHandlers.Add(id, handler);
        }

        /// <summary>
        ///     Unsubscribe event handling function
        /// </summary>
        /// <param name="id">Event type number</param>
        /// <param name="handler">Event handler to unsubscribe from</param>
        public void Unsubscribe(int id, EventHandler<T> handler)
        {
            if (handler == null)
            {
                Log.Error("Event handler is invalid.");
                return;
            }

            if (_cachedNodes.Count > 0)
            {
                foreach (var cachedNode in _cachedNodes)
                    if (cachedNode.Value != null && cachedNode.Value.Value == handler)
                        _tempNodes.Add(cachedNode.Key, cachedNode.Value.Next);
                if (_tempNodes.Count > 0)
                {
                    foreach (var cachedNode in _tempNodes)
                        _cachedNodes[cachedNode.Key] = cachedNode.Value;
                    _tempNodes.Clear();
                }
            }

            if (!_eventHandlers.Remove(id, handler))
                Log.Warning($"Event '{id}' not exists specified handler.");
        }

        /// <summary>
        ///     Throw an event, This operation is thread safe, Even if not thrown in the main thread, It can also ensure the
        ///     callback of event handling functions in the main thread, But the event will be distributed in the next frame after
        ///     being thrown
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event parameters</param>
        public void Fire(object sender, T e)
        {
            if (e == null)
            {
                Log.Error("Event is invalid.");
                return;
            }

            var eventNode = EventNode.Create(sender, e);
            lock (_events)
            {
                _events.Enqueue(eventNode);
            }
        }

        /// <summary>
        ///     Throw event immediate mode, This operation is not thread safe, The incident will be immediately distributed
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event parameters</param>
        public void FireNow(object sender, T e)
        {
            if (e == null)
            {
                Log.Error("Event is invalid.");
                return;
            }

            HandleEvent(sender, e);
        }

        /// <summary>
        ///     Handling event nodes
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event parameters</param>
        private void HandleEvent(object sender, T e)
        {
            if (_eventHandlers.TryGetValue(e.Id, out var range))
            {
                var current = range.First;
                while (current != null && current != range.Terminal)
                {
                    _cachedNodes[e] = current.Next != range.Terminal ? current.Next : null;
                    current.Value.Invoke(sender, e);
                    current = _cachedNodes[e];
                }

                _cachedNodes.Remove(e);
            }

            ReferencePool.Release(e);
        }
    }
}