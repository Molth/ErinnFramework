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
        /// <param name="handler">Handle</param>
        public bool Stop(TimerHandler handler) => Stop(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public bool Play(TimerHandler handler) => Play(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public bool Pause(TimerHandler handler) => Pause(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="newTimestamp">Current timestamp</param>
        public bool Reset(TimerHandler handler, ulong newTimestamp) => Reset(handler.Id, handler.Timestamp, newTimestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="forever">Is it permanent</param>
        public bool SetForever(TimerHandler handler, bool forever) => SetForever(handler.Id, handler.Timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="loops">Frequency</param>
        public bool SetLoops(TimerHandler handler, uint loops) => SetLoops(handler.Id, handler.Timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public bool GetData(TimerHandler handler, out TimerData data) => GetData(handler.Id, handler.Timestamp, out data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="currentTimestamp">Current timestamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public bool GetDelta(TimerHandler handler, ulong currentTimestamp, out ulong delta) => GetDelta(handler.Id, handler.Timestamp, currentTimestamp, out delta);
    }
}