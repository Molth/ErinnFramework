//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Framework Multi Value Dictionary Class
    /// </summary>
    /// <typeparam name="TKey">Specify the primary key type for a multi value dictionary</typeparam>
    /// <typeparam name="TValue">Specify the value type of the multi value dictionary</typeparam>
    internal sealed class ErinnMultiDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, ErinnLinkedListRange<TValue>>>
    {
        /// <summary>
        ///     Dictionary
        /// </summary>
        private readonly Dictionary<TKey, ErinnLinkedListRange<TValue>> _dictionary;

        /// <summary>
        ///     Linked list
        /// </summary>
        private readonly ErinnLinkedList<TValue> _linkedList;

        /// <summary>
        ///     A new instance of the initialization framework's multi value dictionary class
        /// </summary>
        public ErinnMultiDictionary()
        {
            _linkedList = new ErinnLinkedList<TValue>();
            _dictionary = new Dictionary<TKey, ErinnLinkedListRange<TValue>>();
        }

        /// <summary>
        ///     Obtain the actual number of primary keys contained in a multi value dictionary
        /// </summary>
        public int Count => _dictionary.Count;

        /// <summary>
        ///     Get the range of the specified primary key in the multi value dictionary
        /// </summary>
        /// <param name="key">Designated primary key</param>
        /// <returns>Specify the range of the primary key</returns>
        public ErinnLinkedListRange<TValue> this[TKey key]
        {
            get
            {
                _dictionary.TryGetValue(key, out var range);
                return range;
            }
        }

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator<KeyValuePair<TKey, ErinnLinkedListRange<TValue>>> IEnumerable<KeyValuePair<TKey, ErinnLinkedListRange<TValue>>>.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Clean up multi value dictionaries
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
            _linkedList.Clear();
        }

        /// <summary>
        ///     Check if the multi value dictionary contains the specified primary key
        /// </summary>
        /// <param name="key">Primary key to be checked</param>
        /// <returns>Does the multi value dictionary contain the specified primary key</returns>
        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

        /// <summary>
        ///     Check if the multi value dictionary contains the specified value
        /// </summary>
        /// <param name="key">Primary key to be checked</param>
        /// <param name="value">Value to be checked</param>
        /// <returns>Does the multi value dictionary contain the specified value</returns>
        public bool Contains(TKey key, TValue value) => _dictionary.TryGetValue(key, out var range) && range.Contains(value);

        /// <summary>
        ///     Attempt to obtain the range of the specified primary key in the multi value dictionary
        /// </summary>
        /// <param name="key">Designated primary key</param>
        /// <param name="range">Specify the range of the primary key</param>
        /// <returns>Has it been successfully obtained</returns>
        public bool TryGetValue(TKey key, out ErinnLinkedListRange<TValue> range) => _dictionary.TryGetValue(key, out range);

        /// <summary>
        ///     Add the specified value to the specified primary key
        /// </summary>
        /// <param name="key">Designated primary key</param>
        /// <param name="value">The specified value</param>
        public void Add(TKey key, TValue value)
        {
            if (_dictionary.TryGetValue(key, out var range))
            {
                _linkedList.AddBefore(range.Terminal, value);
            }
            else
            {
                var first = _linkedList.AddLast(value);
                var terminal = _linkedList.AddLast(default(TValue));
                _dictionary.Add(key, new ErinnLinkedListRange<TValue>(first, terminal));
            }
        }

        /// <summary>
        ///     Remove the specified value from the specified primary key
        /// </summary>
        /// <param name="key">Designated primary key</param>
        /// <param name="value">The specified value</param>
        /// <returns>Was removal successful</returns>
        public bool Remove(TKey key, TValue value)
        {
            if (_dictionary.TryGetValue(key, out var range))
                for (var current = range.First; current != null && current != range.Terminal; current = current.Next)
                    if (current.Value.Equals(value))
                    {
                        if (current == range.First)
                        {
                            var next = current.Next;
                            if (next == range.Terminal)
                            {
                                _linkedList.Remove(next);
                                _dictionary.Remove(key);
                            }
                            else
                            {
                                _dictionary[key] = new ErinnLinkedListRange<TValue>(next, range.Terminal);
                            }
                        }

                        _linkedList.Remove(current);
                        return true;
                    }

            return false;
        }

        /// <summary>
        ///     Remove all values from the specified primary key
        /// </summary>
        /// <param name="key">Designated primary key</param>
        /// <returns>Was removal successful</returns>
        public bool RemoveAll(TKey key)
        {
            if (_dictionary.TryGetValue(key, out var range))
            {
                _dictionary.Remove(key);
                var current = range.First;
                while (current != null)
                {
                    var next = current != range.Terminal ? current.Next : null;
                    _linkedList.Remove(current);
                    current = next;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        public Enumerator GetEnumerator() => new(_dictionary);

        /// <summary>
        ///     The enumerator for iteratively accessing a collection
        /// </summary>
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, ErinnLinkedListRange<TValue>>>
        {
            /// <summary>
            ///     An enumerator that iterates through a collection
            /// </summary>
            private Dictionary<TKey, ErinnLinkedListRange<TValue>>.Enumerator _enumerator;

            /// <summary>
            ///     The enumerator constructor for iteratively accessing a collection
            /// </summary>
            public Enumerator(Dictionary<TKey, ErinnLinkedListRange<TValue>> dictionary) => _enumerator = dictionary.GetEnumerator();

            /// <summary>
            ///     Get the current node
            /// </summary>
            public KeyValuePair<TKey, ErinnLinkedListRange<TValue>> Current => _enumerator.Current;

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
            void IEnumerator.Reset() => ((IEnumerator<KeyValuePair<TKey, ErinnLinkedListRange<TValue>>>)_enumerator).Reset();
        }
    }
}