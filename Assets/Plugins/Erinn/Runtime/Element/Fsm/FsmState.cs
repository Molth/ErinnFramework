//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Abstract class of states
    /// </summary>
    public abstract class FsmState<T> : IFsmState where T : class, IActor
    {
        /// <summary>
        ///     Fsm
        /// </summary>
        private IFsm _fsm;

        /// <summary>
        ///     Owner of Status
        /// </summary>
        protected T Owner { get; private set; }

        /// <summary>
        ///     Obtain whether the finite state machine currently has a state
        /// </summary>
        protected bool IsRunning => _fsm.IsRunning;

        /// <summary>
        ///     Current state name of the state machine
        /// </summary>
        protected string CurrentName => _fsm.CurrentName;

        /// <summary>
        ///     Private state initialization method
        /// </summary>
        void IFsmState.Init(IFsm fsm, IActor inputOwner, object[] args)
        {
            _fsm = fsm;
            Owner = (T)inputOwner;
            OnInit(args);
        }

        /// <summary>
        ///     Protected state entry method
        /// </summary>
        void IFsmState.OnEnter() => OnEnter();

        /// <summary>
        ///     Protected state Update method
        /// </summary>
        void IFsmState.OnUpdate() => OnUpdate();

        /// <summary>
        ///     Protected state LateUpdate method
        /// </summary>
        void IFsmState.OnLateUpdate() => OnLateUpdate();

        /// <summary>
        ///     Protected state FixedUpdate method
        /// </summary>
        void IFsmState.OnFixedUpdate() => OnFixedUpdate();

        /// <summary>
        ///     Protected state exit method
        /// </summary>
        void IFsmState.OnExit() => OnExit();

        /// <summary>
        ///     Protected state clearing method
        /// </summary>
        void IReference.Clear()
        {
            OnClear();
            _fsm = default;
            Owner = default;
        }

        /// <summary>
        ///     Is it currently in the specified state
        /// </summary>
        bool IFsmState.Is<TState>() => Is<TState>();

        /// <summary>
        ///     Is the current state specified or inherited
        /// </summary>
        bool IFsmState.IsOrSub<TState>() => IsOrSub<TState>();

        /// <summary>
        ///     Is there a specified status
        /// </summary>
        bool IFsmState.Has<TState>() => Has<TState>();

        /// <summary>
        ///     Switching state
        /// </summary>
        void IFsmState.Change<TState>() => Change<TState>();

        /// <summary>
        ///     Initialization
        /// </summary>
        protected abstract void OnInit(object[] args);

        /// <summary>
        ///     Get into the state
        /// </summary>
        protected abstract void OnEnter();

        /// <summary>
        ///     Status update
        /// </summary>
        protected abstract void OnUpdate();

        /// <summary>
        ///     Status update
        /// </summary>
        protected virtual void OnLateUpdate()
        {
        }

        /// <summary>
        ///     Status update
        /// </summary>
        protected virtual void OnFixedUpdate()
        {
        }

        /// <summary>
        ///     Exit status
        /// </summary>
        protected abstract void OnExit();

        /// <summary>
        ///     Clear status
        /// </summary>
        protected virtual void OnClear()
        {
        }

        /// <summary>
        ///     Is it currently in the specified state
        /// </summary>
        protected bool Is<TState>() where TState : IFsmState => _fsm.Is<TState>();

        /// <summary>
        ///     Is the current state specified or inherited
        /// </summary>
        protected bool IsOrSub<TState>() where TState : IFsmState => _fsm.IsOrSub<TState>();

        /// <summary>
        ///     Is there a specified status
        /// </summary>
        protected bool Has<TState>() where TState : IFsmState => _fsm.Has<TState>();

        /// <summary>
        ///     Switching state
        /// </summary>
        protected void Change<TState>() where TState : IFsmState => _fsm.Change<TState>();
    }
}