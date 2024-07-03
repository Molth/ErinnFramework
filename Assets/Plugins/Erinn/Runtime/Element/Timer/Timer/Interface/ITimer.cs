//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Timer interface
    /// </summary>
    internal partial interface ITimer
    {
        /// <summary>
        ///     Cease
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <returns>Whether successful</returns>
        bool Stop(ulong timestamp);

        /// <summary>
        ///     Play
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <returns>Successfully stopped</returns>
        bool Play(ulong timestamp);

        /// <summary>
        ///     Suspend
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <returns>Whether successful</returns>
        bool Pause(ulong timestamp);

        /// <summary>
        ///     Reset
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="newTimestamp">Current timestamp</param>
        /// <returns>Whether successful</returns>
        bool Reset(ulong timestamp, ulong newTimestamp);

        /// <summary>
        ///     Set whether it is permanent
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="forever">Is it permanent</param>
        /// <returns>Whether successful</returns>
        bool SetForever(ulong timestamp, bool forever);

        /// <summary>
        ///     Set frequency
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="loops">Frequency</param>
        /// <returns>Whether successful</returns>
        bool SetLoops(ulong timestamp, uint loops);

        /// <summary>
        ///     Get data
        /// </summary>
        /// <param name="timestamp">Time stamp</param>
        /// <param name="data">Obtained data</param>
        /// <returns>Successfully obtained</returns>
        bool GetData(ulong timestamp, out TimerData data);
    }
}