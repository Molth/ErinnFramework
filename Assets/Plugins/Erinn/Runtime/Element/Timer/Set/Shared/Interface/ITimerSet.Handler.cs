//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer controller interface
    /// </summary>
    public partial interface ITimerSet
    {
        /// <summary>
        ///     Stop Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool Stop(TimerHandler handler);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool Play(TimerHandler handler);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="handler">Handle</param>
        bool Pause(TimerHandler handler);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="forever">Is it permanent</param>
        bool SetForever(TimerHandler handler, bool forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="loops">Frequency</param>
        bool SetLoops(TimerHandler handler, uint loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="handler">Handle</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool GetData(TimerHandler handler, out TimerData data);
    }
}