//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer data
    /// </summary>
    public readonly struct TimerData
    {
        /// <summary>
        ///     Index
        /// </summary>
        public readonly uint Id;

        /// <summary>
        ///     State
        /// </summary>
        public readonly TimerState State;

        /// <summary>
        ///     Time stamp
        /// </summary>
        public readonly ulong Timestamp;

        /// <summary>
        ///     Target timestamp
        /// </summary>
        public readonly ulong Target;

        /// <summary>
        ///     Interval time
        /// </summary>
        public readonly ulong Tick;

        /// <summary>
        ///     Frequency
        /// </summary>
        public readonly uint Loops;

        /// <summary>
        ///     Is it permanent
        /// </summary>
        public readonly bool Forever;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="timer">Timer</param>
        internal TimerData(ITimer timer)
        {
            Id = timer.Id;
            State = timer.State;
            Timestamp = timer.Timestamp;
            Target = timer.Target;
            Tick = timer.Tick;
            Loops = timer.Loops;
            Forever = timer.Forever;
        }
    }
}