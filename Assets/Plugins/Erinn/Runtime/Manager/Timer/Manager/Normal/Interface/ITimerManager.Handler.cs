//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer Manager Interface
    /// </summary>
    public partial interface ITimerManager
    {
        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool Reset(TimerHandler handler);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool GetDelta(TimerHandler handler, out float delta);
    }
}