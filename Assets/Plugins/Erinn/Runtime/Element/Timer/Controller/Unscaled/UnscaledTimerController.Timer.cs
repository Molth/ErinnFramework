//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer controller
    /// </summary>
    internal sealed partial class UnscaledTimerController
    {
        /// <summary>
        ///     Stop Timer
        /// </summary>
        public bool Stop() => TimerManager.StopUnscaled(_handler);

        /// <summary>
        ///     Play Timer
        /// </summary>
        public bool Play() => TimerManager.PlayUnscaled(_handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        public bool Pause() => TimerManager.PauseUnscaled(_handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public bool Reset(TimerHandler handler) => TimerManager.ResetUnscaled(handler);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="forever">Is it permanent</param>
        public bool SetForever(bool forever) => TimerManager.SetForeverUnscaled(_handler, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="loops">Frequency</param>
        public bool SetLoops(uint loops) => TimerManager.SetLoopsUnscaled(_handler, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public bool GetData(out TimerData data) => TimerManager.GetDataUnscaled(_handler, out data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public bool GetDelta(out float delta)
        {
            if (TimerManager.GetDeltaUnscaled(_handler, out var ulongDelta))
            {
                delta = ulongDelta / 1000f;
                return true;
            }

            delta = 0f;
            return false;
        }
    }
}