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
    ///     Timer
    /// </summary>
    public sealed partial class TimerLinkedList
    {
        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="onComplete">Callback upon completion</param>
        public TimerHandler Create(ulong timestamp, ulong tick, Action<uint> onComplete) => Create(timestamp, tick, 0U, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="onComplete">Callback upon completion</param>
        public TimerHandler Create(ulong timestamp, ulong tick, uint loops, Action<uint> onComplete) => Create(timestamp, tick, loops, false, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public TimerHandler Create(ulong timestamp, ulong tick, bool forever, Action<uint> onComplete) => Create(timestamp, tick, 0U, forever, onComplete);

        /// <summary>
        ///     Create a timer
        /// </summary>
        /// <param name="timestamp">Current timestamp</param>
        /// <param name="tick">Interval</param>
        /// <param name="loops">Frequency</param>
        /// <param name="forever">Is it permanent</param>
        /// <param name="onComplete">Callback upon completion</param>
        public TimerHandler Create(ulong timestamp, ulong tick, uint loops, bool forever, Action<uint> onComplete)
        {
#if !UNITY_2021_3_OR_NEWER
            lock (_locker)
            {
#endif
                var id = _indexPool.Rent();
                var timer = Pop();
                timer.Create(id, timestamp, tick, loops, forever, onComplete);
                _timerDictionary[id] = timer;
                Enqueue(timer);
                return new TimerHandler(id, timestamp);
#if !UNITY_2021_3_OR_NEWER
            }
#endif
        }
    }
}