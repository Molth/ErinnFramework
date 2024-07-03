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
    ///     ActionContainer
    /// </summary>
    internal sealed class ActionContainer
    {
        /// <summary>
        ///     Entrust
        /// </summary>
        [ShowInInspector] private readonly HashSet<Action> _delegates;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ActionContainer()
        {
            _delegates = new HashSet<Action>();
            CombinedEvent = null;
        }

        /// <summary>
        ///     Entrust
        /// </summary>
        private event Action CombinedEvent;

        /// <summary>
        ///     Add event
        /// </summary>
        /// <param name="handler">Entrust</param>
        public void Add(Action handler)
        {
            if (_delegates.Add(handler))
                CombinedEvent += handler;
        }

        /// <summary>
        ///     Remove event
        /// </summary>
        /// <param name="handler">Entrust</param>
        public void Remove(Action handler)
        {
            if (_delegates.Remove(handler))
                CombinedEvent -= handler;
        }

        /// <summary>
        ///     Execution event
        /// </summary>
        public void Invoke() => CombinedEvent?.Invoke();

        /// <summary>
        ///     Clear Event
        /// </summary>
        public void Clear()
        {
            _delegates.Clear();
            CombinedEvent = null;
        }
    }
}