//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Animation state machine clearingTriggers
    /// </summary>
    public sealed class FsmClearSignals : StateMachineBehaviour
    {
        /// <summary>
        ///     When entering
        /// </summary>
        public string[] OnEnter = Array.Empty<string>();

        /// <summary>
        ///     When exiting
        /// </summary>
        public string[] OnExit = Array.Empty<string>();

        /// <summary>
        ///     When entering
        /// </summary>
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (var index = 0; index < OnEnter.Length; ++index)
                animator.ResetTrigger(OnEnter[index]);
        }

        /// <summary>
        ///     When exiting
        /// </summary>
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for (var index = 0; index < OnExit.Length; ++index)
                animator.ResetTrigger(OnExit[index]);
        }
    }
}