//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer Manager
    /// </summary>
    internal sealed partial class TimerManager
    {
        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ITimerSet.Stop(uint id, ulong timestamp) => Stop(id, timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ITimerSet.Play(uint id, ulong timestamp) => Play(id, timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ITimerSet.Pause(uint id, ulong timestamp) => Pause(id, timestamp);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ITimerManager.Reset(uint id, ulong timestamp) => Reset(id, timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        bool ITimerSet.SetForever(uint id, ulong timestamp, bool forever) => SetForever(id, timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        bool ITimerSet.SetLoops(uint id, ulong timestamp, uint loops) => SetLoops(id, timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool ITimerSet.GetData(uint id, ulong timestamp, out TimerData data) => GetData(id, timestamp, out data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool ITimerManager.GetDelta(uint id, ulong timestamp, out float delta) => GetDelta(id, timestamp, out delta);

        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool Stop(uint id, ulong timestamp) => TimerLinkedList.Stop(id, timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool Play(uint id, ulong timestamp) => TimerLinkedList.Play(id, timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool Pause(uint id, ulong timestamp) => TimerLinkedList.Pause(id, timestamp);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool Reset(uint id, ulong timestamp) => TimerLinkedList.Reset(id, timestamp, Timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        public static bool SetForever(uint id, ulong timestamp, bool forever) => TimerLinkedList.SetForever(id, timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        public static bool SetLoops(uint id, ulong timestamp, uint loops) => TimerLinkedList.SetLoops(id, timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public static bool GetData(uint id, ulong timestamp, out TimerData data) => TimerLinkedList.GetData(id, timestamp, out data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public static bool GetDelta(uint id, ulong timestamp, out float delta)
        {
            if (TimerLinkedList.GetDelta(id, timestamp, Timestamp, out var ulongDelta))
            {
                delta = ulongDelta / 1000f;
                return true;
            }

            delta = 0.0f;
            return false;
        }
    }
}