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
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool Reset(uint id, ulong timestamp);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool GetDelta(uint id, ulong timestamp, out float delta);
    }
}