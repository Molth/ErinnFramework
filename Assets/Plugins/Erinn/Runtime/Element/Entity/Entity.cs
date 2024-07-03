//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Entity
    /// </summary>
    public abstract class Entity : MonoBehaviour, IActor, IEntity
    {
        /// <summary>
        ///     Controller Dictionary
        /// </summary>
        private Dictionary<Type, IController> _controllerDict;

        /// <summary>
        ///     Get controller
        /// </summary>
        /// <param name="args">Parameter</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Obtained controller</returns>
        public T GetController<T>(params object[] args) where T : IController
        {
            var key = typeof(T);
            if (_controllerDict.TryGetValue(key, out var value))
                return (T)value;
            var controller = ControllerFactory.GetController<T>(this, args);
            _controllerDict.Add(key, controller);
            return controller;
        }

        /// <summary>
        ///     Remove controller
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        public void RemoveController<T>() where T : IController
        {
            var key = typeof(T);
            if (_controllerDict.TryGetValue(key, out var value))
            {
                ReferencePool.Release(value);
                _controllerDict.Remove(key);
            }
        }

        /// <summary>
        ///     Release controller
        /// </summary>
        public void ReleaseControllers()
        {
            foreach (var controller in _controllerDict.Values)
                ReferencePool.Release(controller);
            _controllerDict.Clear();
        }

        /// <summary>
        ///     Initialization
        /// </summary>
        public void InitializeStateMachineBehaviours<T>(Animator animator) where T : StateMachineBehaviour, IStateMachineBehaviourInitialize
        {
            foreach (var behaviour in animator.GetBehaviours<T>())
                behaviour.Initialize(this);
        }

        /// <summary>
        ///     Clean up
        /// </summary>
        public void DisposeStateMachineBehaviours<T>(Animator animator) where T : StateMachineBehaviour, IStateMachineBehaviourInitialize
        {
            foreach (var behaviour in animator.GetBehaviours<T>())
                behaviour.Dispose();
        }

        /// <summary>
        ///     Initialization
        /// </summary>
        public virtual void OnInit() => _controllerDict = new Dictionary<Type, IController>();

        /// <summary>
        ///     DestroyWhen calling
        /// </summary>
        public virtual void OnClear() => ReleaseControllers();
    }
}