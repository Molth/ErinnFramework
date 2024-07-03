//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer
    /// </summary>
    public sealed partial class TimerLinkedList
    {
        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public bool Stop(uint id, ulong timestamp) => TryGet(id, out var timer) && timer.Stop(timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public bool Play(uint id, ulong timestamp) => TryGet(id, out var timer) && timer.Play(timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public bool Pause(uint id, ulong timestamp) => TryGet(id, out var timer) && timer.Pause(timestamp);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="newTimestamp">Current timestamp</param>
        public bool Reset(uint id, ulong timestamp, ulong newTimestamp) => TryGet(id, out var timer) && timer.Reset(timestamp, newTimestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        public bool SetForever(uint id, ulong timestamp, bool forever) => TryGet(id, out var timer) && timer.SetForever(timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        public bool SetLoops(uint id, ulong timestamp, uint loops) => TryGet(id, out var timer) && timer.SetLoops(timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public bool GetData(uint id, ulong timestamp, out TimerData data)
        {
            if (TryGet(id, out var timer))
                return timer.GetData(timestamp, out data);
            data = default;
            return false;
        }

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="currentTimestamp">Current timestamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public bool GetDelta(uint id, ulong timestamp, ulong currentTimestamp, out ulong delta)
        {
            if (TryGet(id, out var timer))
                return timer.GetDelta(timestamp, currentTimestamp, out delta);
            delta = 0UL;
            return false;
        }
    }
}