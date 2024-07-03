//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Timer Manager
    /// </summary>
    internal sealed partial class TimerManager
    {
        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, Action<uint> onComplete) => CreateUnscaled(seconds, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, uint loops, Action<uint> onComplete) => CreateUnscaled(seconds, loops, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, bool forever, Action<uint> onComplete) => CreateUnscaled(seconds, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, uint loops, bool forever, Action<uint> onComplete) => CreateUnscaled(seconds, loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, Action onComplete) => CreateUnscaled(seconds, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, uint loops, Action onComplete) => CreateUnscaled(seconds, loops, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, bool forever, Action onComplete) => CreateUnscaled(seconds, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.CreateUnscaled(float seconds, uint loops, bool forever, Action onComplete) => CreateUnscaled(seconds, loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, Action<uint> onComplete) => CreateUnscaled(seconds, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, uint loops, Action<uint> onComplete) => CreateUnscaled(seconds, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, bool forever, Action<uint> onComplete) => CreateUnscaled(seconds, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, uint loops, bool forever, Action<uint> onComplete) => CreateUnscaled((ulong)(seconds * 1000f), loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, Action onComplete) => CreateUnscaled(seconds, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, uint loops, Action onComplete) => CreateUnscaled(seconds, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, bool forever, Action onComplete) => CreateUnscaled(seconds, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(float seconds, uint loops, bool forever, Action onComplete) => CreateUnscaled((ulong)(seconds * 1000f), loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, Action<uint> onComplete) => CreateUnscaled(tick, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, uint loops, Action<uint> onComplete) => CreateUnscaled(tick, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, bool forever, Action<uint> onComplete) => CreateUnscaled(tick, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, uint loops, bool forever, Action<uint> onComplete) => UnscaledTimerLinkedList.Create(UnscaledTimestamp, tick, loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, Action onComplete) => CreateUnscaled(tick, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, uint loops, Action onComplete) => CreateUnscaled(tick, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, bool forever, Action onComplete) => CreateUnscaled(tick, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler CreateUnscaled(ulong tick, uint loops, bool forever, Action onComplete) => CreateUnscaled(tick, loops, forever, _ => onComplete());
    }
}