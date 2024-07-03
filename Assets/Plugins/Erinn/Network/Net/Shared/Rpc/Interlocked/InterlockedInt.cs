//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
#endif

#nullable enable

namespace Erinn
{
    /// <summary>
    ///     Represents an int value that can be safely accessed and modified in a multithreaded environment
    /// </summary>
    [Serializable]
    public struct InterlockedInt : IComparable, IComparable<int>, IComparable<InterlockedInt>, IEquatable<int>, IEquatable<InterlockedInt>
    {
        /// <summary>
        ///     Internal storage for the int value
        /// </summary>
        private int _value;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="value">The initial value</param>
        public InterlockedInt(int value) => _value = value;

        /// <summary>
        ///     Current value
        /// </summary>
        public int Value => _value;

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Value condition</returns>
        public int CompareTo(object? obj) => obj switch
        {
            null => 1,
            int flag => CompareTo(flag),
            InterlockedInt value => CompareTo(value),
            _ => 1
        };

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The int value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(int value)
        {
            if (Value < value)
                return -1;
            if (Value > value)
                return 1;
            return 0;
        }

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The InterlockedInt</param>
        /// <returns>Value condition</returns>
        public int CompareTo(InterlockedInt value) => CompareTo(value.Value);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(int value) => Value == value;

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(InterlockedInt value) => Equals(value.Value);

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => Value;

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the int value as a string</returns>
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
        public override bool Equals(object? obj) => (obj is int flag && Equals(flag)) || (obj is InterlockedInt value && Equals(value));

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public int Increment() => Interlocked.Add(ref _value, 1);

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public int Decrement() => Interlocked.Add(ref _value, -1);

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <param name="value">The int value</param>
        /// <returns>Value</returns>
        public int Add(int value) => Interlocked.Add(ref _value, value);

        /// <summary>
        ///     Atomically sets the value
        /// </summary>
        /// <param name="value">The value to be set</param>
        /// <returns>Whether the value was changed successfully</returns>
        public bool Set(int value) => Interlocked.Exchange(ref _value, value) != value;

        /// <summary>
        ///     Atomically sets the value
        /// </summary>
        /// <param name="value">The value to be set</param>
        /// <param name="comparand">The value to be compare</param>
        /// <returns>Whether the value was changed successfully</returns>
        public bool CompareSet(int value, int comparand) => Interlocked.CompareExchange(ref _value, value, comparand) == comparand;

        /// <summary>
        ///     Implicitly converts an InterlockedInt to an int value
        /// </summary>
        /// <param name="value">The InterlockedInt to convert</param>
        /// <returns>The int value of the InterlockedInt</returns>
        public static implicit operator int(InterlockedInt value) => value.Value;

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedInt operator ++(InterlockedInt value)
        {
            value.Increment();
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedInt operator --(InterlockedInt value)
        {
            value.Decrement();
            return value;
        }

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedInt operator +(InterlockedInt value, int increment)
        {
            value.Add(increment);
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedInt operator -(InterlockedInt value, int increment)
        {
            value.Add(-increment);
            return value;
        }

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedInt operator +(InterlockedInt value, InterlockedInt increment)
        {
            value.Add(increment.Value);
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedInt operator -(InterlockedInt value, InterlockedInt increment)
        {
            value.Add(-increment.Value);
            return value;
        }

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(InterlockedInt left, int right) => left.Value == right;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(InterlockedInt left, int right) => left.Value != right;

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(InterlockedInt left, InterlockedInt right) => left.Value == right.Value;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(InterlockedInt left, InterlockedInt right) => left.Value != right.Value;
    }
}