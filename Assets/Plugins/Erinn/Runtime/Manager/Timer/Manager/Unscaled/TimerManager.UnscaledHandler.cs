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
        /// <param name="handler">Handle</param>
        bool ITimerManager.StopUnscaled(TimerHandler handler) => StopUnscaled(handler);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ITimerManager.PlayUnscaled(TimerHandler handler) => PlayUnscaled(handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ITimerManager.PauseUnscaled(TimerHandler handler) => PauseUnscaled(handler);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="forever">Is it permanent</param>
        bool ITimerManager.SetForeverUnscaled(TimerHandler handler, bool forever) => SetForeverUnscaled(handler, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="loops">Frequency</param>
        bool ITimerManager.SetLoopsUnscaled(TimerHandler handler, uint loops) => SetLoopsUnscaled(handler, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool ITimerManager.GetDataUnscaled(TimerHandler handler, out TimerData data) => GetDataUnscaled(handler, out data);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ITimerManager.ResetUnscaled(TimerHandler handler) => ResetUnscaled(handler);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool ITimerManager.GetDeltaUnscaled(TimerHandler handler, out float delta) => GetDeltaUnscaled(handler, out delta);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool ResetUnscaled(TimerHandler handler) => ResetUnscaled(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public static bool GetDeltaUnscaled(TimerHandler handler, out float delta)
        {
            if (GetDeltaUnscaled(handler.Id, handler.Timestamp, out var ulongDelta))
            {
                delta = ulongDelta / 1000f;
                return true;
            }

            delta = 0.0f;
            return false;
        }

        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool StopUnscaled(TimerHandler handler) => StopUnscaled(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool PlayUnscaled(TimerHandler handler) => PlayUnscaled(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public static bool PauseUnscaled(TimerHandler handler) => PauseUnscaled(handler.Id, handler.Timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="forever">Is it permanent</param>
        public static bool SetForeverUnscaled(TimerHandler handler, bool forever) => SetForeverUnscaled(handler.Id, handler.Timestamp, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="loops">Frequency</param>
        public static bool SetLoopsUnscaled(TimerHandler handler, uint loops) => SetLoopsUnscaled(handler.Id, handler.Timestamp, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public static bool GetDataUnscaled(TimerHandler handler, out TimerData data) => GetDataUnscaled(handler.Id, handler.Timestamp, out data);
    }
}