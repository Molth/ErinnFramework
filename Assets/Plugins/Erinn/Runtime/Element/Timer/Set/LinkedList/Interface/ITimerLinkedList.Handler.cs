//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer interface
    /// </summary>
    public partial interface ITimerLinkedList
    {
        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="newTimestamp">Current timestamp</param>
        bool Reset(TimerHandler handler, ulong newTimestamp);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="currentTimestamp">Current timestamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool GetDelta(TimerHandler handler, ulong currentTimestamp, out ulong delta);
    }
}