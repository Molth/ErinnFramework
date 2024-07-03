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
    public interface ISourceDictionary<TKey, TValue> where TKey : unmanaged where TValue : notnull
    {
        /// <summary>
        ///     Key
        /// </summary>
        ICollection<TKey> Keys { get; }

        /// <summary>
        ///     Value
        /// </summary>
        ICollection<TValue> Values { get; }

        /// <summary>
        ///     Empty
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        ///     Current quantity
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Get value
        /// </summary>
        TValue this[TKey tKey] { get; set; }

        /// <summary>
        ///     Distribution
        /// </summary>
        TKey Allocate();

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="value">Value</param>
        TKey Add(TValue value);

        /// <summary>
        ///     Remove key
        /// </summary>
        /// <param name="key">Key</param>
        bool Remove(TKey key);

        /// <summary>
        ///     Remove key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        bool TryRemove(TKey key, out TValue value);

        /// <summary>
        ///     Get value
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        TValue GetValue(TKey key);

        /// <summary>
        ///     Get value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns>Successfully obtained</returns>
        bool TryGetValue(TKey key, out TValue value);

        /// <summary>
        ///     Containing keys
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Containing keys</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        ///     Contains specified key value pairs
        /// </summary>
        /// <param name="tKey">Key</param>
        /// <param name="tValue">Value</param>
        /// <returns>Does it contain specified key value pairs</returns>
        bool Contains(TKey tKey, TValue tValue);

        /// <summary>
        ///     Empty
        /// </summary>
        void Clear();
    }
}