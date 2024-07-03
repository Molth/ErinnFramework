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
    internal partial interface ITimer
    {
        /// <summary>
        ///     Index
        /// </summary>
        uint Id { get; }

        /// <summary>
        ///     State
        /// </summary>
        TimerState State { get; }

        /// <summary>
        ///     Time stamp
        /// </summary>
        ulong Timestamp { get; }

        /// <summary>
        ///     Target timestamp
        /// </summary>
        ulong Target { get; }

        /// <summary>
        ///     Interval time
        /// </summary>
        ulong Tick { get; }

        /// <summary>
        ///     Frequency
        /// </summary>
        uint Loops { get; }

        /// <summary>
        ///     Is it permanent
        /// </summary>
        bool Forever { get; }

        /// <summary>
        ///     Callback upon completion
        /// </summary>
        Action<uint> OnComplete { get; }
    }
}