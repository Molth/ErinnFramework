//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer controller
    /// </summary>
    internal sealed partial class TimerController
    {
        /// <summary>
        ///     Stop Timer
        /// </summary>
        public bool Stop() => TimerManager.Stop(_handler);

        /// <summary>
        ///     Play Timer
        /// </summary>
        public bool Play() => TimerManager.Play(_handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        public bool Pause() => TimerManager.Pause(_handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        public bool Reset(TimerHandler handler) => TimerManager.Reset(handler);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="forever">Is it permanent</param>
        public bool SetForever(bool forever) => TimerManager.SetForever(_handler, forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="loops">Frequency</param>
        public bool SetLoops(uint loops) => TimerManager.SetLoops(_handler, loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        public bool GetData(out TimerData data) => TimerManager.GetData(_handler, out data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        public bool GetDelta(out float delta)
        {
            if (TimerManager.GetDelta(_handler, out var ulongDelta))
            {
                delta = ulongDelta / 1000f;
                return true;
            }

            delta = 0f;
            return false;
        }
    }
}