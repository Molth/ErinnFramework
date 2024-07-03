//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER
using System.Collections.Generic;
#endif

#pragma warning disable CS8601

namespace Erinn
{
    /// <summary>
    ///     Mapping
    /// </summary>
    public abstract class SourceDictionary<TKey, TValue> : ISourceDictionary<TKey, TValue> where TKey : unmanaged where TValue : notnull
    {
        /// <summary>
        ///     Dictionary
        /// </summary>
        private readonly Dictionary<TKey, TValue> _dictionary;

        /// <summary>
        ///     Structure
        /// </summary>
        protected SourceDictionary() => _dictionary = new Dictionary<TKey, TValue>();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        protected SourceDictionary(int capacity) => _dictionary = new Dictionary<TKey, TValue>(capacity);

        /// <summary>
        ///     Index Pool
        /// </summary>
        protected abstract IndexPool<TKey> IndexPool { get; }

        /// <summary>
        ///     Key
        /// </summary>
        public ICollection<TKey> Keys => _dictionary.Keys;

        /// <summary>
        ///     Value
        /// </summary>
        public ICollection<TValue> Values => _dictionary.Values;

        /// <summary>
        ///     Empty
        /// </summary>
        public bool IsEmpty => _dictionary.Count == 0;

        /// <summary>
        ///     Current quantity
        /// </summary>
        public int Count => _dictionary.Count;

        /// <summary>
        ///     Get value
        /// </summary>
        public TValue this[TKey tKey]
        {
            get => _dictionary[tKey];
            set => _dictionary[tKey] = value;
        }

        /// <summary>
        ///     Distribution
        /// </summary>
        public TKey Allocate() => IndexPool.Rent();

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="value">Value</param>
        public TKey Add(TValue value)
        {
            var key = IndexPool.Rent();
            _dictionary[key] = value;
            return key;
        }

        /// <summary>
        ///     Remove key
        /// </summary>
        /// <param name="key">Key</param>
        public bool Remove(TKey key)
        {
            if (!_dictionary.Remove(key, out _))
                return false;
            IndexPool.Return(key);
            return true;
        }

        /// <summary>
        ///     Remove key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public bool TryRemove(TKey key, out TValue value)
        {
            if (!_dictionary.Remove(key, out value))
                return false;
            IndexPool.Return(key);
            return true;
        }

        /// <summary>
        ///     Get value
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public TValue GetValue(TKey key) => _dictionary[key];

        /// <summary>
        ///     Get value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Successfully obtained</returns>
        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        /// <summary>
        ///     Containing keys
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Containing keys</returns>
        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        /// <summary>
        ///     Contains specified key value pairs
        /// </summary>
        /// <param name="tKey">Key</param>
        /// <param name="tValue">Value</param>
        /// <returns>Does it contain specified key value pairs</returns>
        public bool Contains(TKey tKey, TValue tValue) => _dictionary.TryGetValue(tKey, out var value) && value.Equals(tValue);

        /// <summary>
        ///     Empty
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
            IndexPool.Clear();
        }
    }
}