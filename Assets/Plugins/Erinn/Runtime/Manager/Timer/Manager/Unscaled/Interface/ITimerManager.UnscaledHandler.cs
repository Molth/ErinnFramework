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
        ///     Stop Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool StopUnscaled(TimerHandler handler);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool PlayUnscaled(TimerHandler handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool PauseUnscaled(TimerHandler handler);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="forever">Is it permanent</param>
        bool SetForeverUnscaled(TimerHandler handler, bool forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="loops">Frequency</param>
        bool SetLoopsUnscaled(TimerHandler handler, uint loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool GetDataUnscaled(TimerHandler handler, out TimerData data);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool ResetUnscaled(TimerHandler handler);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool GetDeltaUnscaled(TimerHandler handler, out float delta);
    }
}