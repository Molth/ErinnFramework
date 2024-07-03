//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     State interface
    /// </summary>
    public interface IFsmState : IReference
    {
        /// <summary>
        ///     Method for initializing this state
        /// </summary>
        internal void Init(IFsm fsm, IActor inputOwner, object[] args);

        /// <summary>
        ///     The method to enter this state
        /// </summary>
        void OnEnter();

        /// <summary>
        ///     UpdateMethod for updating this status
        /// </summary>
        void OnUpdate();

        /// <summary>
        ///     LateUpdateMethod for updating this status
        /// </summary>
        void OnLateUpdate();

        /// <summary>
        ///     FixedUpdateMethod for updating this status
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        ///     The method to exit this state
        /// </summary>
        void OnExit();

        /// <summary>
        ///     Is it currently in the specified state
        /// </summary>
        bool Is<TState>() where TState : IFsmState;

        /// <summary>
        ///     Is the current state specified or inherited
        /// </summary>
        bool IsOrSub<TState>() where TState : IFsmState;

        /// <summary>
        ///     Is there a specified status
        /// </summary>
        bool Has<TState>() where TState : IFsmState;

        /// <summary>
        ///     Switching state
        /// </summary>
        void Change<TState>() where TState : IFsmState;
    }
}