//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Framework linked list range
    /// </summary>
    /// <typeparam name="T">Specify the element type of the linked list range</typeparam>
    internal readonly struct ErinnLinkedListRange<T> : IEnumerable<T>
    {
        /// <summary>
        ///     Initialize a new instance of the framework linked list range
        /// </summary>
        /// <param name="first">The starting node of the linked list range</param>
        /// <param name="terminal">Ending marker node for linked list scope</param>
        public ErinnLinkedListRange(LinkedListNode<T> first, LinkedListNode<T> terminal)
        {
            First = first;
            Terminal = terminal;
        }

        /// <summary>
        ///     Obtain whether the linked list range is valid
        /// </summary>
        public bool IsValid => First != null && Terminal != null && First != Terminal;

        /// <summary>
        ///     Get the starting node of the linked list range
        /// </summary>
        public LinkedListNode<T> First { get; }

        /// <summary>
        ///     Get the endpoint node of the linked list range
        /// </summary>
        public LinkedListNode<T> Terminal { get; }

        /// <summary>
        ///     Obtain the number of nodes in the linked list range
        /// </summary>
        public int Count
        {
            get
            {
                if (!IsValid)
                    return 0;
                var count = 0;
                for (var current = First; current != null && current != Terminal; current = current.Next)
                    count++;
                return count;
            }
        }

        /// <summary>
        ///     Check if the specified value is included
        /// </summary>
        /// <param name="value">Value to be checked</param>
        /// <returns>Does it include the specified value</returns>
        public bool Contains(T value)
        {
            for (var current = First; current != null && current != Terminal; current = current.Next)
                if (current.Value.Equals(value))
                    return true;
            return false;
        }

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        private ErinnEnumerator GetEnumerator() => new(this);

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     The enumerator for iteratively accessing a collection
        /// </summary>
        private struct ErinnEnumerator : IEnumerator<T>
        {
            private readonly ErinnLinkedListRange<T> _erinnLinkedListRange;
            private LinkedListNode<T> _current;
            private T _currentValue;

            public ErinnEnumerator(ErinnLinkedListRange<T> range)
            {
                _erinnLinkedListRange = range;
                _current = _erinnLinkedListRange.First;
                _currentValue = default;
            }

            /// <summary>
            ///     Get the current node
            /// </summary>
            public T Current => _currentValue;

            /// <summary>
            ///     Get the current number of enumerators
            /// </summary>
            object IEnumerator.Current => _currentValue;

            /// <summary>
            ///     Clean up enumerators
            /// </summary>
            public void Dispose() => _currentValue = default;

            /// <summary>
            ///     Get the next node
            /// </summary>
            /// <returns>Return to the next node</returns>
            public bool MoveNext()
            {
                if (_current == null || _current == _erinnLinkedListRange.Terminal)
                    return false;
                _currentValue = _current.Value;
                _current = _current.Next;
                return true;
            }

            /// <summary>
            ///     Reset enumerators
            /// </summary>
            void IEnumerator.Reset()
            {
                _current = _erinnLinkedListRange.First;
                _currentValue = default;
            }
        }
    }
}