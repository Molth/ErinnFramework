//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using System;
#endif

#pragma warning disable CS8618

namespace Erinn
{
    /// <summary>
    ///     Timer
    /// </summary>
    internal sealed partial class Timer : ITimer
    {
        /// <summary>
        ///     Next node
        /// </summary>
        public Timer Next { get; set; }

        /// <summary>
        ///     Index
        /// </summary>
        public uint Id { get; private set; }

        /// <summary>
        ///     State
        /// </summary>
        public TimerState State { get; private set; }

        /// <summary>
        ///     Time stamp
        /// </summary>
        public ulong Timestamp { get; private set; }

        /// <summary>
        ///     Target timestamp
        /// </summary>
        public ulong Target { get; private set; }

        /// <summary>
        ///     Interval time
        /// </summary>
        public ulong Tick { get; private set; }

        /// <summary>
        ///     Frequency
        /// </summary>
        public uint Loops { get; private set; }

        /// <summary>
        ///     Is it permanent
        /// </summary>
        public bool Forever { get; private set; }

        /// <summary>
        ///     Callback upon completion
        /// </summary>
        public Action<uint> OnComplete { get; set; }
    }
}