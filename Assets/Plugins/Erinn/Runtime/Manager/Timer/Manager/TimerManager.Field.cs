//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Timer Manager
    /// </summary>
    internal sealed partial class TimerManager
    {
        /// <summary>
        ///     Timer
        /// </summary>
        private static readonly TimerLinkedList TimerLinkedList = new();

        /// <summary>
        ///     Timer
        /// </summary>
        private static readonly TimerLinkedList UnscaledTimerLinkedList = new();

        /// <summary>
        ///     Current timestamp
        /// </summary>
        public static ulong Timestamp => (ulong)(Time.time * 1000f);

        /// <summary>
        ///     Current timestamp
        /// </summary>
        public static ulong UnscaledTimestamp => (ulong)(Time.unscaledTime * 1000f);
    }
}