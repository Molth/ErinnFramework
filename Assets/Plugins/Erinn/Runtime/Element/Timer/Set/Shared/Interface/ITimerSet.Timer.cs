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
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool Stop(uint id, ulong timestamp);

        /// <summary>
        ///     Play Timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool Play(uint id, ulong timestamp);

        /// <summary>
        ///     Pause timer
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        bool Pause(uint id, ulong timestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        bool SetForever(uint id, ulong timestamp, bool forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        bool SetLoops(uint id, ulong timestamp, uint loops);

        /// <summary>
        ///     Attempting to obtain data
        /// </summary>
        /// <param name="id">TimerId</param>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool GetData(uint id, ulong timestamp, out TimerData data);
    }
}