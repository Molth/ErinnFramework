//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer handle
    /// </summary>
    public readonly struct TimerHandler
    {
        /// <summary>
        ///     TimerId
        /// </summary>
        public readonly uint Id;

        /// <summary>
        ///     Timestamp
        /// </summary>
        public readonly ulong Timestamp;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Timestamp</param>
        public TimerHandler(uint id, ulong timestamp)
        {
            Id = id;
            Timestamp = timestamp;
        }
    }
}