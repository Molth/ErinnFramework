//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Erinn
{
    /// <summary>
    ///     Float
    /// </summary>
    /// <typeparam name="TKey">Key</typeparam>
    public sealed class FloatDictionary<TKey> : IEnumerable<KeyValuePair<TKey, float>>
    {
        /// <summary>
        ///     Dictionary
        /// </summary>
        private readonly Dictionary<TKey, float> _dictionary = new();

        /// <summary>
        ///     Value
        /// </summary>
        [ShowInInspector]
        public float Value { get; private set; }

        /// <summary>
        ///     Key
        /// </summary>
        public Dictionary<TKey, float>.KeyCollection Keys => _dictionary.Keys;

        /// <summary>
        ///     Value
        /// </summary>
        public Dictionary<TKey, float>.ValueCollection Values => _dictionary.Values;

        /// <summary>
        ///     Obtain the actual number of primary keys contained in the dictionary
        /// </summary>
        public int Count => _dictionary.Count;

        /// <summary>
        ///     Get value
        /// </summary>
        public float this[TKey key] => _dictionary.ContainsKey(key) ? _dictionary[key] : 0f;

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator<KeyValuePair<TKey, float>> IEnumerable<KeyValuePair<TKey, float>>.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Add key value pairs
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Add(TKey key, float value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, value);
                Value += value;
            }
        }

        /// <summary>
        ///     Set key value pairs
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Set(TKey key, float value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, value);
                Value += value;
            }
            else
            {
                var delta = value - _dictionary[key];
                _dictionary[key] = value;
                Value += delta;
            }
        }

        /// <summary>
        ///     Remove key
        /// </summary>
        /// <param name="key">Key</param>
        public void Remove(TKey key)
        {
            if (_dictionary.ContainsKey(key))
            {
                var value = _dictionary[key];
                _dictionary.Remove(key);
                Value -= value;
            }
        }

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Update(TKey key, float value)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, value);
                Value += value;
            }
            else
            {
                _dictionary[key] += value;
                Value += value;
            }
        }

        /// <summary>
        ///     Temporary updates
        /// </summary>
        /// <param name="time">Time</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void TempUpdate(float time, TKey key, float value)
        {
            Update(key, value);
            TimerManager.Create(time, _ => Update(key, -value));
        }

        /// <summary>
        ///     Clean up
        /// </summary>
        public void Clean()
        {
            Value = 0f;
            var keys = MathV.ToList(_dictionary.Keys);
            var values = MathV.ToList(_dictionary.Values);
            var len = keys.Count;
            for (var i = len - 1; i >= 0; --i)
                if (values[i] == 0f)
                    _dictionary.Remove(keys[i]);
                else
                    Value += values[i];
        }

        /// <summary>
        ///     Containing keys
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Does it contain keys</returns>
        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        /// <summary>
        ///     Check if the dictionary contains the specified value
        /// </summary>
        /// <param name="key">Primary key to be checked</param>
        /// <param name="value">Value to be checked</param>
        /// <returns>Does the dictionary contain the specified value</returns>
        public bool Contains(TKey key, float value) => _dictionary.TryGetValue(key, out var outValue) && outValue.Equals(value);

        /// <summary>
        ///     Attempt to obtain the value of the specified primary key in the dictionary
        /// </summary>
        /// <param name="key">Designated primary key</param>
        /// <param name="outValue">Specify the value of the primary key</param>
        /// <returns>Has it been successfully obtained</returns>
        public bool TryGetValue(TKey key, out float outValue) => _dictionary.TryGetValue(key, out outValue);

        /// <summary>
        ///     Empty
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
            Value = 0f;
        }

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        private ErinnEnumerator GetEnumerator() => new(_dictionary);

        /// <summary>
        ///     The enumerator for iteratively accessing a collection
        /// </summary>
        private struct ErinnEnumerator : IEnumerator<KeyValuePair<TKey, float>>
        {
            private Dictionary<TKey, float>.Enumerator _enumerator;

            public ErinnEnumerator(Dictionary<TKey, float> dictionary) => _enumerator = dictionary.GetEnumerator();

            /// <summary>
            ///     Get the current node
            /// </summary>
            public KeyValuePair<TKey, float> Current => _enumerator.Current;

            /// <summary>
            ///     Get the current number of enumerators
            /// </summary>
            object IEnumerator.Current => _enumerator.Current;

            /// <summary>
            ///     Clean up enumerators
            /// </summary>
            public void Dispose() => _enumerator.Dispose();

            /// <summary>
            ///     Get the next node
            /// </summary>
            /// <returns>Return to the next node</returns>
            public bool MoveNext() => _enumerator.MoveNext();

            /// <summary>
            ///     Reset enumerators
            /// </summary>
            readonly void IEnumerator.Reset() => ((IEnumerator<KeyValuePair<TKey, float>>)_enumerator).Reset();
        }
    }
}