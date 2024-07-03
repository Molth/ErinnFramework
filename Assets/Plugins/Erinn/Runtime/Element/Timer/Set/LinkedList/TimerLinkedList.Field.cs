//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using System.Collections.Generic;
#endif

#pragma warning disable CS8618

namespace Erinn
{
    /// <summary>
    ///     Timer
    /// </summary>
    public sealed partial class TimerLinkedList
    {
        /// <summary>
        ///     Capacity
        /// </summary>
        public const int Capacity = 4096;

#if !UNITY_2021_3_OR_NEWER
        /// <summary>
        ///     Lock
        /// </summary>
        private readonly object _locker = new();
#endif

        /// <summary>
        ///     Timer queue
        /// </summary>
        private readonly Queue<Timer> _incomingTimerQueue = new(Capacity);

        /// <summary>
        ///     Buffer zone
        /// </summary>
        private readonly Stack<Timer> _timerBuffer = new(Capacity);

        /// <summary>
        ///     Index Pool
        /// </summary>
        private readonly UintIndexPool _indexPool = new(Capacity);

        /// <summary>
        ///     Timer pool
        /// </summary>
        private readonly Stack<Timer> _timerPool = new(Capacity);

        /// <summary>
        ///     Timer Dictionary
        /// </summary>
        private readonly Dictionary<uint, Timer> _timerDictionary = new(Capacity);

        /// <summary>
        ///     Chain header
        /// </summary>
        private Timer _head;

        /// <summary>
        ///     List tail
        /// </summary>
        private Timer _tail;

        /// <summary>
        ///     Quantity
        /// </summary>
        public int Count { get; private set; }
    }
}