//------------------------------------------------------------
// Erinn Network
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
        TimerHandler ITimerManager.Create(float seconds, Action<uint> onComplete) => Create(seconds, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.Create(float seconds, uint loops, Action<uint> onComplete) => Create(seconds, loops, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.Create(float seconds, bool forever, Action<uint> onComplete) => Create(seconds, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.Create(float seconds, uint loops, bool forever, Action<uint> onComplete) => Create(seconds, loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.Create(float seconds, Action onComplete) => Create(seconds, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.Create(float seconds, uint loops, Action onComplete) => Create(seconds, loops, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.Create(float seconds, bool forever, Action onComplete) => Create(seconds, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        TimerHandler ITimerManager.Create(float seconds, uint loops, bool forever, Action onComplete) => Create(seconds, loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, Action<uint> onComplete) => Create(seconds, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, uint loops, Action<uint> onComplete) => Create(seconds, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, bool forever, Action<uint> onComplete) => Create(seconds, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, uint loops, bool forever, Action<uint> onComplete) => Create((ulong)(seconds * 1000f), loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, Action onComplete) => Create(seconds, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, uint loops, Action onComplete) => Create(seconds, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, bool forever, Action onComplete) => Create(seconds, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="seconds">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(float seconds, uint loops, bool forever, Action onComplete) => Create((ulong)(seconds * 1000f), loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, Action<uint> onComplete) => Create(tick, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, uint loops, Action<uint> onComplete) => Create(tick, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, bool forever, Action<uint> onComplete) => Create(tick, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, uint loops, bool forever, Action<uint> onComplete) => TimerLinkedList.Create(Timestamp, tick, loops, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, Action onComplete) => Create(tick, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, uint loops, Action onComplete) => Create(tick, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, bool forever, Action onComplete) => Create(tick, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public static TimerHandler Create(ulong tick, uint loops, bool forever, Action onComplete) => Create(tick, loops, forever, _ => onComplete());
    }
}