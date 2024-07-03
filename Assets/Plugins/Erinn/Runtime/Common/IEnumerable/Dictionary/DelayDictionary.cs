//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable CS8601
#pragma warning disable CS8603

namespace Erinn
{
    /// <summary>
    ///     Framework Delay Dictionary Class
    /// </summary>
    /// <typeparam name="TKey">Key</typeparam>
    /// <typeparam name="TValue">Value</typeparam>
    public sealed class DelayDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : notnull where TValue : notnull
    {
        /// <summary>
        ///     Key value pairs to be added soon
        /// </summary>
        private readonly Dictionary<TKey, TValue> _addKeyValuePairs;

        /// <summary>
        ///     Key to be removed soon
        /// </summary>
        private readonly HashSet<TKey> _removeKeys;

        /// <summary>
        ///     Dictionary
        /// </summary>
        private readonly Dictionary<TKey, TValue> _dictionary;

        /// <summary>
        ///     Call on removal
        /// </summary>
        private readonly Action<TValue> _onRemoved;

        /// <summary>
        ///     Call during loop
        /// </summary>
        private readonly Action<TValue> _onLooped;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        /// <param name="onRemove">Call on removal</param>
        /// <param name="onLoop">Call during loop</param>
        public DelayDictionary(int capacity, Action<TValue> onRemove, Action<TValue> onLoop)
        {
            _addKeyValuePairs = new Dictionary<TKey, TValue>(capacity);
            _removeKeys = new HashSet<TKey>(capacity);
            _dictionary = new Dictionary<TKey, TValue>(capacity);
            _onRemoved = onRemove;
            _onLooped = onLoop;
        }

        /// <summary>
        ///     Key
        /// </summary>
        public Dictionary<TKey, TValue>.KeyCollection Keys => _dictionary.Keys;

        /// <summary>
        ///     Value
        /// </summary>
        public Dictionary<TKey, TValue>.ValueCollection Values => _dictionary.Values;

        /// <summary>
        ///     Get value
        /// </summary>
        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Get value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Does it contain keys</returns>
        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        /// <summary>
        ///     Get waiting to add key value pairs
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Does it contain keys</returns>
        public bool TryGetWaitValue(TKey key, out TValue value) => _addKeyValuePairs.TryGetValue(key, out value);

        /// <summary>
        ///     Waiting to add key value pairs
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void WaitAdd(TKey key, TValue value) => _addKeyValuePairs.TryAdd(key, value);

        /// <summary>
        ///     Waiting for removal key
        /// </summary>
        /// <param name="key">Key</param>
        public void WaitRemove(TKey key) => _removeKeys.Add(key);

        /// <summary>
        ///     Waiting for dictionary update
        /// </summary>
        public void OnUpdate()
        {
            if (_removeKeys.Count > 0)
            {
                foreach (var key in _removeKeys)
                {
                    if (_dictionary.TryGetValue(key, out var value))
                    {
                        _onRemoved.Invoke(value);
                        _dictionary.Remove(key);
                    }
                }

                _removeKeys.Clear();
            }

            if (_addKeyValuePairs.Count > 0)
            {
                foreach (var (key, value) in _addKeyValuePairs)
                    _dictionary[key] = value;
                _addKeyValuePairs.Clear();
            }

            if (_dictionary.Count == 0)
                return;
            foreach (var value in Values)
                _onLooped.Invoke(value);
        }

        /// <summary>
        ///     Empty
        /// </summary>
        public void Clear()
        {
            _addKeyValuePairs.Clear();
            _removeKeys.Clear();
            _dictionary.Clear();
        }

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        private ErinnEnumerator GetEnumerator() => new(_dictionary);

        /// <summary>
        ///     The enumerator for iteratively accessing a collection
        /// </summary>
        private struct ErinnEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            /// <summary>
            ///     Dictionary
            /// </summary>
            private Dictionary<TKey, TValue>.Enumerator _enumerator;

            /// <summary>
            ///     Dictionary enumeration
            /// </summary>
            /// <param name="dictionary">Dictionary</param>
            public ErinnEnumerator(Dictionary<TKey, TValue> dictionary) => _enumerator = dictionary.GetEnumerator();

            /// <summary>
            ///     Get the current node
            /// </summary>
            public KeyValuePair<TKey, TValue> Current => _enumerator.Current;

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
            readonly void IEnumerator.Reset() => ((IEnumerator<KeyValuePair<TKey, TValue>>)_enumerator).Reset();
        }
    }
}