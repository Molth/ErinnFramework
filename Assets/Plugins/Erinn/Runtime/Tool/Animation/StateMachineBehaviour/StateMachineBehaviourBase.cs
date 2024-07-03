//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Animation state base class
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public abstract class StateMachineBehaviourBase<T> : StateMachineBehaviour, IStateMachineBehaviourInitialize where T : class, IActor
    {
        /// <summary>
        ///     Owner
        /// </summary>
        protected T Owner { get; private set; }

        /// <summary>
        ///     Already initialized
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        ///     Initialization
        /// </summary>
        void IStateMachineBehaviourInitialize.Initialize(IActor owner)
        {
            Owner = (T)owner;
            Initialized = true;
            OnInitialize();
        }

        /// <summary>
        ///     Clean up
        /// </summary>
        void IStateMachineBehaviourInitialize.Dispose()
        {
            Initialized = false;
            Owner = null;
            OnDispose();
        }

        /// <summary>
        ///     Initialization
        /// </summary>
        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnDispose()
        {
        }
    }
}