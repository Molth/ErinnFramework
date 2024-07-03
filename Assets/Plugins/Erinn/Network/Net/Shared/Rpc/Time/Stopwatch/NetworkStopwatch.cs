//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif
using System.Runtime.CompilerServices;

#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     Stopwatch
    /// </summary>
    public struct NetworkStopwatch : IComparable, IComparable<NetworkStopwatch>, IEquatable<NetworkStopwatch>, IComparable<long>, IEquatable<long>
    {
        /// <summary>
        ///     Timestamp
        /// </summary>
        public long Timestamp;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="timestamp">Timestamp</param>
        public NetworkStopwatch(long timestamp) => Timestamp = timestamp;

        /// <summary>
        ///     Reset
        /// </summary>
        public void Reset() => Timestamp = 0L;

        /// <summary>
        ///     Restart
        /// </summary>
        public void Restart() => Timestamp = NetworkTime.ElapsedTicks;

        /// <summary>
        ///     Create
        /// </summary>
        /// <returns>Stopwatch</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NetworkStopwatch Create() => new(NetworkTime.ElapsedTicks);

        /// <summary>
        ///     Now
        /// </summary>
        public static NetworkStopwatch Now => Create();

        /// <summary>
        ///     Elapsed
        /// </summary>
        public TimeSpan Elapsed => new(ElapsedTicks);

        /// <summary>
        ///     Elapsed ticks
        /// </summary>
        public long ElapsedTicks => NetworkTime.ElapsedTicks - Timestamp;

        /// <summary>
        ///     Elapsed milliseconds
        /// </summary>
        public long ElapsedMilliseconds => ElapsedTicks / 10000L;

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => Timestamp.GetHashCode();

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="other">The value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(long other) => Timestamp.CompareTo(other);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="other">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(long other) => Timestamp == other;

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public override bool Equals(object? obj) => obj switch
        {
            null => false,
            long value => Timestamp == value,
            NetworkStopwatch other => Timestamp == other.Timestamp,
            _ => false
        };

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="obj">The value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(object? obj) => obj switch
        {
            null => 1,
            long value => CompareTo(value),
            NetworkStopwatch other => CompareTo(other),
            _ => 1
        };

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="other">The value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(NetworkStopwatch other) => Timestamp.CompareTo(other.Timestamp);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="other">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(NetworkStopwatch other) => Timestamp == other.Timestamp;

        /// <summary>
        ///     Increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static NetworkStopwatch operator +(NetworkStopwatch left, NetworkStopwatch right) => new(left.Timestamp + right.Timestamp);

        /// <summary>
        ///     Decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static NetworkStopwatch operator -(NetworkStopwatch left, NetworkStopwatch right) => new(left.Timestamp - right.Timestamp);

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(NetworkStopwatch left, NetworkStopwatch right) => left.Timestamp == right.Timestamp;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(NetworkStopwatch left, NetworkStopwatch right) => left.Timestamp != right.Timestamp;

        /// <summary>
        ///     Returns the left value is smaller than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Smaller than this instance</returns>
        public static bool operator <(NetworkStopwatch left, NetworkStopwatch right) => left.Timestamp < right.Timestamp;

        /// <summary>
        ///     Returns the left value is smaller or equal than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Smaller or equal than this instance</returns>
        public static bool operator <=(NetworkStopwatch left, NetworkStopwatch right) => left.Timestamp <= right.Timestamp;

        /// <summary>
        ///     Returns the left value is bigger than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Bigger than this instance</returns>
        public static bool operator >(NetworkStopwatch left, NetworkStopwatch right) => left.Timestamp > right.Timestamp;

        /// <summary>
        ///     Returns the left value is bigger or equal than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Bigger or equal than this instance</returns>
        public static bool operator >=(NetworkStopwatch left, NetworkStopwatch right) => left.Timestamp >= right.Timestamp;

        /// <summary>
        ///     Increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static NetworkStopwatch operator +(NetworkStopwatch left, long right) => new(left.Timestamp + right);

        /// <summary>
        ///     Decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static NetworkStopwatch operator -(NetworkStopwatch left, long right) => new(left.Timestamp - right);

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(NetworkStopwatch left, long right) => left.Timestamp == right;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(NetworkStopwatch left, long right) => left.Timestamp != right;

        /// <summary>
        ///     Returns the left value is smaller than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Smaller than this instance</returns>
        public static bool operator <(NetworkStopwatch left, long right) => left.Timestamp < right;

        /// <summary>
        ///     Returns the left value is smaller or equal than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Smaller or equal than this instance</returns>
        public static bool operator <=(NetworkStopwatch left, long right) => left.Timestamp <= right;

        /// <summary>
        ///     Returns the left value is bigger than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Bigger than this instance</returns>
        public static bool operator >(NetworkStopwatch left, long right) => left.Timestamp > right;

        /// <summary>
        ///     Returns the left value is bigger or equal than the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Bigger or equal than this instance</returns>
        public static bool operator >=(NetworkStopwatch left, long right) => left.Timestamp >= right;
    }
}