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
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="newTimestamp">Current timestamp</param>
        bool Reset(uint id, ulong timestamp, ulong newTimestamp);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="currentTimestamp">Current timestamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool GetDelta(uint id, ulong timestamp, ulong currentTimestamp, out ulong delta);
    }
}