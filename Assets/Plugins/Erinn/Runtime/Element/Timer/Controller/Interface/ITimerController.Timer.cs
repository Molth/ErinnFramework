//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer controller interface
    /// </summary>
    public partial interface ITimerController
    {
        /// <summary>
        ///     Stop Timer
        /// </summary>
        bool Stop();

        /// <summary>
        ///     Play Timer
        /// </summary>
        bool Play();

        /// <summary>
        ///     Pause timer
        /// </summary>
        bool Pause();

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool Reset(TimerHandler handler);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="forever">Is it permanent</param>
        bool SetForever(bool forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="loops">Frequency</param>
        bool SetLoops(uint loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool GetData(out TimerData data);

        /// <summary>
        ///     Get remaining time
        /// </summary>
        /// <param name="delta">Remaining time</param>
        /// <returns>Remaining time obtained</returns>
        bool GetDelta(out float delta);
    }
}