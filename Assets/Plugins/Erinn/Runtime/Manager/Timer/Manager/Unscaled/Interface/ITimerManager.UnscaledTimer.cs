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
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool StopUnscaled(uint id, ulong timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool PlayUnscaled(uint id, ulong timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool PauseUnscaled(uint id, ulong timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        bool SetForeverUnscaled(uint id, ulong timestamp, bool forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        bool SetLoopsUnscaled(uint id, ulong timestamp, uint loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool GetDataUnscaled(uint id, ulong timestamp, out TimerData data);

        /// <summary>
        ///     Reset Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool ResetUnscaled(uint id, ulong timestamp);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool GetDeltaUnscaled(uint id, ulong timestamp, out float delta);
    }
}