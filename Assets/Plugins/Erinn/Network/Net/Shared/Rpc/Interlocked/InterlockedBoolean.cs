//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Threading;
#endif

#nullable enable

// ReSharper disable UnusedParameter.Global

namespace Erinn
{
    /// <summary>
    ///     Represents a boolean value that can be safely accessed and modified in a multithreaded environment
    /// </summary>
    [Serializable]
    public struct InterlockedBoolean : IComparable, IComparable<bool>, IComparable<InterlockedBoolean>, IEquatable<bool>, IEquatable<InterlockedBoolean>
    {
        /// <summary>
        ///     Internal storage for the boolean value
        /// </summary>
        private int _value;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="value">The initial value</param>
        public InterlockedBoolean(bool value) => _value = value ? 1 : 0;

        /// <summary>
        ///     Current value
        /// </summary>
        public bool Value => _value == 1;

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Value condition</returns>
        public int CompareTo(object? obj) => obj switch
        {
            null => 1,
            bool flag => CompareTo(flag),
            InterlockedBoolean value => CompareTo(value),
            _ => 1
        };

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The boolean value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(bool value) => Value == value ? 0 : !Value ? -1 : 1;

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The InterlockedBoolean</param>
        /// <returns>Value condition</returns>
        public int CompareTo(InterlockedBoolean value) => CompareTo(value.Value);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(bool value) => Value == value;

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(InterlockedBoolean value) => Equals(value.Value);

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => Value ? 1 : 0;

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the boolean value as a string</returns>
        public override string ToString() => !Value ? "False" : "True";

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the uint value as a string</returns>
        public string ToString(IFormatProvider? provider) => ToString();

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public override bool Equals(object? obj) => (obj is bool flag && Equals(flag)) || (obj is InterlockedBoolean value && Equals(value));

        /// <summary>
        ///     Atomically sets the value instead of volatile
        /// </summary>
        /// <param name="value">The value to be set</param>
        /// <returns>Whether the value was changed successfully</returns>
        public bool Set(bool value) => value ? Interlocked.CompareExchange(ref _value, 1, 0) == 0 : Interlocked.CompareExchange(ref _value, 0, 1) == 1;

        /// <summary>
        ///     Implicitly converts an InterlockedBoolean to a boolean value
        /// </summary>
        /// <param name="value">The InterlockedBoolean to convert</param>
        /// <returns>The boolean value of the InterlockedBoolean</returns>
        public static implicit operator bool(InterlockedBoolean value) => value.Value;

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(InterlockedBoolean left, InterlockedBoolean right) => left.Value == right.Value;

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(InterlockedBoolean left, InterlockedBoolean right) => left.Value != right.Value;
    }
}