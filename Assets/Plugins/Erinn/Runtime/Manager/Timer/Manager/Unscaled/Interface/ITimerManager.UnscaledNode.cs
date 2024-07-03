//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Timer Manager Interface
    /// </summary>
    public partial interface ITimerManager
    {
        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, uint loops, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, bool forever, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, uint loops, bool forever, Action<uint> onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, Action onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, uint loops, Action onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, bool forever, Action onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler CreateUnscaled(float seconds, uint loops, bool forever, Action onComplete);
    }
}