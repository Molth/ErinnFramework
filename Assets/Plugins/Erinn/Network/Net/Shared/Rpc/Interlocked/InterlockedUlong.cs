//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
#endif

#nullable enable

namespace Erinn
{
    /// <summary>
    ///     Represents an ulong value that can be safely accessed and modified in a multithreaded environment
    /// </summary>
    [Serializable]
    public struct InterlockedUlong : IComparable, IComparable<ulong>, IComparable<InterlockedUlong>, IEquatable<ulong>, IEquatable<InterlockedUlong>
    {
        /// <summary>
        ///     Internal storage for the ulong value
        /// </summary>
        private ulong _value;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="value">The initial value</param>
        public InterlockedUlong(ulong value) => _value = value;

        /// <summary>
        ///     Current value
        /// </summary>
        public ulong Value => _value;

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Value condition</returns>
        public int CompareTo(object? obj) => obj switch
        {
            null => 1,
            ulong flag => CompareTo(flag),
            InterlockedUlong value => CompareTo(value),
            _ => 1
        };

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The InterlockedUlong</param>
        /// <returns>Value condition</returns>
        public int CompareTo(InterlockedUlong value) => CompareTo(value.Value);

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The ulong value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(ulong value)
        {
            if (Value < value)
                return -1;
            if (Value > value)
                return 1;
            return 0;
        }

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(InterlockedUlong value) => Equals(value.Value);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(ulong value) => Value == value;

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the ulong value as a string</returns>
        public override string ToString() => Value.ToString();

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the uint value as a string</returns>
        public string ToString(string? format) => Value.ToString(format);

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the uint value as a string</returns>
        public string ToString(string? format, IFormatProvider? provider) => Value.ToString(format, provider);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public override bool Equals(object? obj) => (obj is ulong flag && Equals(flag)) || (obj is InterlockedUlong value && Equals(value));

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public ulong Increment() => (ulong)Interlocked.Add(ref Unsafe.As<ulong, long>(ref _value), 1L);

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public ulong Decrement() => (ulong)Interlocked.Add(ref Unsafe.As<ulong, long>(ref _value), -1L);

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <param name="value">The ulong value</param>
        /// <returns>Value</returns>
        public ulong Add(long value) => (ulong)Interlocked.Add(ref Unsafe.As<ulong, long>(ref _value), value);

        /// <summary>
        ///     Atomically sets the value
        /// </summary>
        /// <param name="value">The value to be set</param>
        /// <returns>Whether the value was changed successfully</returns>
        public bool Set(ulong value) => (ulong)Interlocked.Exchange(ref Unsafe.As<ulong, long>(ref _value), (long)value) != value;

        /// <summary>
        ///     Atomically sets the value
        /// </summary>
        /// <param name="value">The value to be set</param>
        /// <param name="comparand">The value to be compare</param>
        /// <returns>Whether the value was changed successfully</returns>
        public bool CompareSet(ulong value, ulong comparand) => (ulong)Interlocked.CompareExchange(ref Unsafe.As<ulong, long>(ref _value), (long)value, (long)comparand) == comparand;

        /// <summary>
        ///     Implicitly converts an InterlockedUlong to a ulong value
        /// </summary>
        /// <param name="value">The InterlockedUlong to convert</param>
        /// <returns>The ulong value of the InterlockedUlong</returns>
        public static implicit operator ulong(InterlockedUlong value) => value.Value;

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUlong operator ++(InterlockedUlong value)
        {
            value.Increment();
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUlong operator --(InterlockedUlong value)
        {
            value.Decrement();
            return value;
        }

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUlong operator +(InterlockedUlong value, ulong increment)
        {
            value.Add((long)increment);
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUlong operator -(InterlockedUlong value, ulong increment)
        {
            value.Add(-(long)increment);
            return value;
        }

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUlong operator +(InterlockedUlong value, InterlockedUlong increment)
        {
            value.Add((long)increment.Value);
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUlong operator -(InterlockedUlong value, InterlockedUlong increment)
        {
            value.Add(-(long)increment.Value);
            return value;
        }

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(InterlockedUlong left, ulong right) => left.Value == right;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(InterlockedUlong left, ulong right) => left.Value != right;

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(InterlockedUlong left, InterlockedUlong right) => left.Value == right.Value;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(InterlockedUlong left, InterlockedUlong right) => left.Value != right.Value;
    }
}