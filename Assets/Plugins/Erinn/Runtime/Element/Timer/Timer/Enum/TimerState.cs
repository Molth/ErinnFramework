//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer status
    /// </summary>
    public enum TimerState : byte
    {
        /// <summary>
        ///     Working
        /// </summary>
        Run,

        /// <summary>
        ///     Suspend
        /// </summary>
        Pause,

        /// <summary>
        ///     Cease
        /// </summary>
        Stop
    }
}