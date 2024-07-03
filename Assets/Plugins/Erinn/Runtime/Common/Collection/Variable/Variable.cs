//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using System;
using UnityEngine;

#pragma warning disable CS8632
#endif

namespace Erinn
{
    /// <summary>
    ///     Variable
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    [Serializable]
    public sealed class Variable<T> : IComparable, IComparable<T>, IComparable<Variable<T>>, IEquatable<T>, IEquatable<Variable<T>> where T : struct, IComparable, IComparable<T>, IEquatable<T>
    {
        /// <summary>
        ///     Internal storage for the T value
        /// </summary>
#if UNITY_2021_3_OR_NEWER
        [SerializeField]
#endif
        private T _value;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="value">The initial value</param>
        /// <param name="onValueChanged">Value changed callback</param>
        public Variable(T value = default, Action<T, T>? onValueChanged = null)
        {
            _value = value;
            OnValueChanged = onValueChanged;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="onValueChanged">Value changed callback</param>
        public Variable(Action<T, T>? onValueChanged) => OnValueChanged = onValueChanged;

        /// <summary>
        ///     Current value
        /// </summary>
        public T Value
        {
            get => _value;
            set => Set(value);
        }

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Value condition</returns>
        public int CompareTo(object? obj) => obj switch
        {
            null => 1,
            Variable<T> flag => Value.CompareTo(flag.Value),
            T value => Value.CompareTo(value),
            _ => 1
        };

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="value">The T value</param>
        /// <returns>Value condition</returns>
        public int CompareTo(T value) => Value.CompareTo(value);

        /// <summary>
        ///     Compares this instance to a specified value
        /// </summary>
        /// <param name="obj">The Variable</param>
        /// <returns>Value condition</returns>
        public int CompareTo(Variable<T>? obj) => obj == null ? 1 : Value.CompareTo(obj.Value);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="value">A value to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(T value) => Value.Equals(value);

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public bool Equals(Variable<T>? obj) => ReferenceEquals(this, obj) || (obj != null && Value.Equals(obj.Value));

        /// <summary>
        ///     Returns the hash code for this instance
        /// </summary>
        /// <returns>A hash code for the current</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        ///     Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Represents the T value as a string</returns>
        public override string? ToString() => Value.ToString();

        /// <summary>
        ///     Returns a value indicating whether this instance is equal to a specified object
        /// </summary>
        /// <param name="obj">An object to compare to this instance</param>
        /// <returns>Has the same value as this instance</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is Variable<T> other && Value.Equals(other.Value)) || (obj is T value && Value.Equals(value));

        /// <summary>
        ///     Value changed callback
        /// </summary>
        public event Action<T, T>? OnValueChanged;

        /// <summary>
        ///     Atomically sets the value
        /// </summary>
        /// <param name="newValue">The value to be set</param>
        /// <returns>Whether the value was changed successfully</returns>
        public void Set(T newValue)
        {
            if (_value.Equals(newValue))
                return;
            var value = _value;
            _value = newValue;
            OnValueChanged?.Invoke(value, newValue);
        }

        /// <summary>
        ///     Implicitly converts an Variable to a T value
        /// </summary>
        /// <param name="obj">The Variable to convert</param>
        /// <returns>The T value of the Variable</returns>
        public static implicit operator T(Variable<T> obj) => obj.Value;

        /// <summary>
        ///     Returns the left value is equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has the same value as this instance</returns>
        public static bool operator ==(Variable<T> left, T right) => left != null && left.Value.Equals(right);

        /// <summary>
        ///     Returns the left value is not equal to the right value
        /// </summary>
        /// <param name="left">Left value</param>
        /// <param name="right">Right value</param>
        /// <returns>Has a different value than this instance</returns>
        public static bool operator !=(Variable<T> left, T right) => left == null || !left.Value.Equals(right);
    }
}