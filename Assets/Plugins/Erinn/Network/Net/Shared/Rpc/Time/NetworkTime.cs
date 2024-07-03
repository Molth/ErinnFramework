//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif
using System.Diagnostics;

namespace Erinn
{
    /// <summary>
    ///     Network time
    /// </summary>
    public static class NetworkTime
    {
        /// <summary>
        ///     Frequency
        /// </summary>
        public static readonly long Frequency = Stopwatch.Frequency;

        /// <summary>
        ///     Frequency interval
        /// </summary>
        public static readonly double FrequencyInterval = 1.0 / Stopwatch.Frequency;

        /// <summary>
        ///     Is high resolution
        /// </summary>
        public static readonly bool IsHighResolution = Stopwatch.IsHighResolution;

        /// <summary>
        ///     Elapsed
        /// </summary>
        public static TimeSpan Elapsed => new(ElapsedTicks);

        /// <summary>
        ///     Elapsed ticks
        /// </summary>
        public static long ElapsedTicks => Stopwatch.GetTimestamp();

        /// <summary>
        ///     Elapsed milliseconds
        /// </summary>
        public static long ElapsedMilliseconds => ElapsedTicks / 10000L;

        /// <summary>
        ///     Utc elapsed
        /// </summary>
        public static TimeSpan UtcElapsed => new(UtcElapsedTicks);

        /// <summary>
        ///     Utc elapsed ticks
        /// </summary>
        public static long UtcElapsedTicks => DateTime.UtcNow.Ticks;

        /// <summary>
        ///     Utc elapsed milliseconds
        /// </summary>
        public static long UtcElapsedMilliseconds => UtcElapsedTicks / 10000L;

        /// <summary>
        ///     Now
        /// </summary>
        public static double Now => ElapsedTicks * FrequencyInterval;
    }
}