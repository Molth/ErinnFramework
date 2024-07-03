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
        bool ITimerManager.StopUnscaled(uint id, ulong timestamp) => StopUnscaled(id, timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ITimerManager.PlayUnscaled(uint id, ulong timestamp) => PlayUnscaled(id, timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ITimerManager.PauseUnscaled(uint id, ulong timestamp) => PauseUnscaled(id, timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        bool ITimerManager.SetForeverUnscaled(uint id, ulong timestamp, bool forever) => SetForeverUnscaled(id, timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        bool ITimerManager.SetLoopsUnscaled(uint id, ulong timestamp, uint loops) => SetLoopsUnscaled(id, timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool ITimerManager.GetDataUnscaled(uint id, ulong timestamp, out TimerData data) => GetDataUnscaled(id, timestamp, out data);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ITimerManager.ResetUnscaled(uint id, ulong timestamp) => ResetUnscaled(id, timestamp);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool ITimerManager.GetDeltaUnscaled(uint id, ulong timestamp, out float delta) => GetDeltaUnscaled(id, timestamp, out delta);

        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool StopUnscaled(uint id, ulong timestamp) => UnscaledTimerLinkedList.Stop(id, timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool PlayUnscaled(uint id, ulong timestamp) => UnscaledTimerLinkedList.Play(id, timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool PauseUnscaled(uint id, ulong timestamp) => UnscaledTimerLinkedList.Pause(id, timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        public static bool SetForeverUnscaled(uint id, ulong timestamp, bool forever) => UnscaledTimerLinkedList.SetForever(id, timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        public static bool SetLoopsUnscaled(uint id, ulong timestamp, uint loops) => UnscaledTimerLinkedList.SetLoops(id, timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public static bool GetDataUnscaled(uint id, ulong timestamp, out TimerData data) => UnscaledTimerLinkedList.GetData(id, timestamp, out data);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        public static bool ResetUnscaled(uint id, ulong timestamp) => UnscaledTimerLinkedList.Reset(id, timestamp, UnscaledTimestamp);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public static bool GetDeltaUnscaled(uint id, ulong timestamp, out float delta)
        {
            if (UnscaledTimerLinkedList.GetDelta(id, timestamp, UnscaledTimestamp, out var ulongDelta))
            {
                delta = ulongDelta / 1000f;
                return true;
            }

            delta = 0.0f;
            return false;
        }
    }
}