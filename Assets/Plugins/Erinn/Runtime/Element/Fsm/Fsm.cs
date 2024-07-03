//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Normal state machine
    /// </summary>
    public abstract class Fsm<T> : Controller<T>, IFsm where T : class, IActor
    {
        /// <summary>
        ///     Dictionary of stored states
        /// </summary>
        private readonly Dictionary<Type, IFsmState> _stateDict = new();

        /// <summary>
        ///     Interface for status
        /// </summary>
        private IFsmState Current { get; set; }

        /// <summary>
        ///     Obtain the number of states in a finite state machine
        /// </summary>
        public int Count => _stateDict.Count;

        /// <summary>
        ///     Obtain whether the finite state machine currently has a state
        /// </summary>
        public bool IsRunning => Current != null;

        /// <summary>
        ///     Current state name of the state machine
        /// </summary>
        public string CurrentName => Current != null ? Current.GetType().FullName : "";

        /// <summary>
        ///     State Dictionary
        /// </summary>
        Dictionary<Type, IFsmState> IFsm.StateDict => _stateDict;

        /// <summary>
        ///     Current state
        /// </summary>
        IFsmState IFsm.Current => Current;

        /// <summary>
        ///     State Machine Update
        /// </summary>
        public void OnUpdate() => Current?.OnUpdate();

        /// <summary>
        ///     State Machine Update
        /// </summary>
        public void OnLateUpdate() => Current?.OnLateUpdate();

        /// <summary>
        ///     State Machine Update
        /// </summary>
        public void OnFixedUpdate() => Current?.OnFixedUpdate();

        /// <summary>
        ///     Is it currently in the specified state
        /// </summary>
        public bool Is<TState>() where TState : IFsmState
        {
            if (Current == null)
                return false;
            var key = typeof(TState);
            if (_stateDict.TryGetValue(key, out var state))
                return Current == state;
            return false;
        }

        /// <summary>
        ///     Is the current state specified or inherited
        /// </summary>
        public bool IsOrSub<TState>() where TState : IFsmState
        {
            if (Current == null)
                return false;
            var key = typeof(TState);
            if (_stateDict.TryGetValue(key, out var state))
                return Current == state;
            return Current.GetType().IsSubclassOf(key);
        }

        /// <summary>
        ///     Is there a specified status
        /// </summary>
        public bool Has<TState>() where TState : IFsmState => _stateDict.ContainsKey(typeof(TState));

        /// <summary>
        ///     State Machine Add State
        /// </summary>
        public void Add<TState>(params object[] args) where TState : IFsmState
        {
            var key = typeof(TState);
            if (!_stateDict.ContainsKey(key))
            {
                var state = (TState)ReferencePool.Acquire(typeof(TState));
                state.Init(this, Owner, args);
                _stateDict.Add(key, state);
            }
        }

        /// <summary>
        ///     Switching state
        /// </summary>
        public void Change<TState>() where TState : IFsmState
        {
            var key = typeof(TState);
            if (_stateDict.TryGetValue(key, out var state))
            {
                if (Current == state)
                    return;
                Current?.OnExit();
                Current = state;
                Current.OnEnter();
            }
        }

        /// <summary>
        ///     Get Status
        /// </summary>
        public TState Get<TState>() where TState : IFsmState
        {
            var key = typeof(TState);
            return _stateDict.TryGetValue(key, out var value) ? (TState)value : default;
        }

        /// <summary>
        ///     Remove status
        /// </summary>
        public void Remove<TState>() where TState : IFsmState
        {
            var key = typeof(TState);
            if (_stateDict.TryGetValue(key, out var state))
            {
                ReferencePool.Release(state);
                _stateDict.Remove(key);
            }
        }

        /// <summary>
        ///     Controller initialization
        /// </summary>
        protected override void OnInit(object[] args)
        {
        }

        /// <summary>
        ///     Release all states
        /// </summary>
        protected void Release()
        {
            foreach (var state in _stateDict.Values)
                ReferencePool.Release(state);
            _stateDict.Clear();
        }

        protected override void OnClear() => Release();
    }
}