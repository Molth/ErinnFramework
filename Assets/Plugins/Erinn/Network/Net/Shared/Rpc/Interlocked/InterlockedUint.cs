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
    ///     Represents an uint value that can be safely accessed and modified in a multithreaded environment
    /// </summary>
    [Serializable]
    public struct InterlockedUint : IComparable, IComparable<uint>, IComparable<InterlockedUint>, IEquatable<uint>, IEquatable<InterlockedUint>
    {
        /// <summary>
        ///     Internal storage for the uint value
        /// </summary>
        private uint _value;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="value">The initial value</param>
        public InterlockedUint(uint value) => _value = value;

        /// <summary>
        ///     Current value
        /// </summary>
        public uint Value => _value;

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Value condition</returns>
        public int CompareTo(object? obj) => obj switch
        {
            null => 1,
            uint flag => CompareTo(flag),
            InterlockedUint value => CompareTo(value),
            _ => 1
        };

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The InterlockedUint</param>
        /// <returns>Value condition</returns>
        public int CompareTo(InterlockedUint value) => CompareTo(value.Value);

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The int value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(uint value)
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
        public bool Equals(InterlockedUint value) => Equals(value.Value);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(uint value) => Value == value;

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => (int)Value;

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the uint value as a string</returns>
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
        public override bool Equals(object? obj) => (obj is uint flag && Equals(flag)) || (obj is InterlockedUint value && Equals(value));

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public uint Increment() => (uint)Interlocked.Add(ref Unsafe.As<uint, int>(ref _value), 1);

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public uint Decrement() => (uint)Interlocked.Add(ref Unsafe.As<uint, int>(ref _value), -1);

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <param name="value">The int value</param>
        /// <returns>Value</returns>
        public uint Add(int value) => (uint)Interlocked.Add(ref Unsafe.As<uint, int>(ref _value), value);

        /// <summary>
        ///     Atomically sets the value
        /// </summary>
        /// <param name="value">The value to be set</param>
        /// <returns>Whether the value was changed successfully</returns>
        public bool Set(uint value) => (uint)Interlocked.Exchange(ref Unsafe.As<uint, int>(ref _value), (int)value) != value;

        /// <summary>
        ///     Atomically sets the value
        /// </summary>
        /// <param name="value">The value to be set</param>
        /// <param name="comparand">The value to be compare</param>
        /// <returns>Whether the value was changed successfully</returns>
        public bool CompareSet(uint value, uint comparand) => (uint)Interlocked.CompareExchange(ref Unsafe.As<uint, int>(ref _value), (int)value, (int)comparand) == comparand;

        /// <summary>
        ///     Implicitly converts an InterlockedUint to a uint value
        /// </summary>
        /// <param name="value">The InterlockedUint to convert</param>
        /// <returns>The int value of the InterlockedUint</returns>
        public static implicit operator uint(InterlockedUint value) => value.Value;

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUint operator ++(InterlockedUint value)
        {
            value.Increment();
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUint operator --(InterlockedUint value)
        {
            value.Decrement();
            return value;
        }

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUint operator +(InterlockedUint value, uint increment)
        {
            value.Add((int)increment);
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUint operator -(InterlockedUint value, uint increment)
        {
            value.Add(-(int)increment);
            return value;
        }

        /// <summary>
        ///     Atomically increments the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUint operator +(InterlockedUint value, InterlockedUint increment)
        {
            value.Add((int)increment.Value);
            return value;
        }

        /// <summary>
        ///     Atomically decrement the value
        /// </summary>
        /// <returns>Value</returns>
        public static InterlockedUint operator -(InterlockedUint value, InterlockedUint increment)
        {
            value.Add(-(int)increment.Value);
            return value;
        }

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(InterlockedUint left, uint right) => left.Value == right;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(InterlockedUint left, uint right) => left.Value != right;

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(InterlockedUint left, InterlockedUint right) => left.Value == right.Value;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(InterlockedUint left, InterlockedUint right) => left.Value != right.Value;
    }
}