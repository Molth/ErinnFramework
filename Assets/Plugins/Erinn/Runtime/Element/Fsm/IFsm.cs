//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     State Machine Interface
    /// </summary>
    internal interface IFsm
    {
        /// <summary>
        ///     State Dictionary
        /// </summary>
        Dictionary<Type, IFsmState> StateDict { get; }

        /// <summary>
        ///     Current state
        /// </summary>
        IFsmState Current { get; }

        /// <summary>
        ///     Obtain the number of states in a finite state machine
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Obtain whether the finite state machine currently has a state
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        ///     Current state name
        /// </summary>
        string CurrentName { get; }

        /// <summary>
        ///     Update
        /// </summary>
        void OnUpdate();

        /// <summary>
        ///     LateUpdate
        /// </summary>
        void OnLateUpdate();

        /// <summary>
        ///     FixedUpdate
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        ///     It's a state
        /// </summary>
        bool Is<TState>() where TState : IFsmState;

        /// <summary>
        ///     Is it a state or an inherited state
        /// </summary>
        bool IsOrSub<TState>() where TState : IFsmState;

        /// <summary>
        ///     Stateful
        /// </summary>
        bool Has<TState>() where TState : IFsmState;

        /// <summary>
        ///     Add Status
        /// </summary>
        void Add<TState>(params object[] args) where TState : IFsmState;

        /// <summary>
        ///     Switching states
        /// </summary>
        void Change<TState>() where TState : IFsmState;

        /// <summary>
        ///     Get Status
        /// </summary>
        TState Get<TState>() where TState : IFsmState;

        /// <summary>
        ///     Remove status
        /// </summary>
        void Remove<TState>() where TState : IFsmState;
    }
}