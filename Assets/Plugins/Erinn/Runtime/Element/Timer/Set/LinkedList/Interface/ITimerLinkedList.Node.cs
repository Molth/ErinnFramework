//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Timer interface
    /// </summary>
    public partial interface ITimerLinkedList : ITimerSet
    {
        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler Create(ulong timestamp, ulong tick, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler Create(ulong timestamp, ulong tick, uint loops, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler Create(ulong timestamp, ulong tick, bool forever, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler Create(ulong timestamp, ulong tick, uint loops, bool forever, Action<uint> onComplete);
    }
}