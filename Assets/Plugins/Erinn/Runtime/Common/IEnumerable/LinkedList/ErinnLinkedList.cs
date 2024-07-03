//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Framework linked list class
    /// </summary>
    /// <typeparam name="T">Specify the element type of the linked list</typeparam>
    internal sealed class ErinnLinkedList<T> : ICollection<T>, ICollection
    {
        /// <summary>
        ///     Cache node
        /// </summary>
        private readonly Queue<LinkedListNode<T>> _cachedNodes;

        /// <summary>
        ///     Linked list
        /// </summary>
        private readonly LinkedList<T> _linkedList;

        /// <summary>
        ///     Initialize a new instance of the framework linked list class
        /// </summary>
        public ErinnLinkedList()
        {
            _linkedList = new LinkedList<T>();
            _cachedNodes = new Queue<LinkedListNode<T>>();
        }

        /// <summary>
        ///     Get the number of cached linked list nodes
        /// </summary>
        public int CachedNodeCount => _cachedNodes.Count;

        /// <summary>
        ///     Get the first node of the linked list
        /// </summary>
        public LinkedListNode<T> First => _linkedList.First;

        /// <summary>
        ///     Get the last node of the linked list
        /// </summary>
        public LinkedListNode<T> Last => _linkedList.Last;

        /// <summary>
        ///     Get available for synchronizing pairs ICollection Objects accessed by
        /// </summary>
        public object SyncRoot => ((ICollection)_linkedList).SyncRoot;

        /// <summary>
        ///     Get a value, This value indicates whether to synchronize with the ICollection Access to（Thread Safe）
        /// </summary>
        public bool IsSynchronized => ((ICollection)_linkedList).IsSynchronized;

        /// <summary>
        ///     From specific ICollection Index Start, Copy elements of an array into an array
        /// </summary>
        /// <param name="array">
        ///     One-dimensional array, It is derived from ICollection The target array of copied elements must have
        ///     a zero based index
        /// </param>
        /// <param name="index">array Zero based indexing in, Start copying from here</param>
        public void CopyTo(Array array, int index) => ((ICollection)_linkedList).CopyTo(array, index);

        /// <summary>
        ///     Obtain the actual number of nodes included in the linked list
        /// </summary>
        public int Count => _linkedList.Count;

        /// <summary>
        ///     Get a value, This value indicates ICollection`1 Is it read-only
        /// </summary>
        public bool IsReadOnly => ((ICollection<T>)_linkedList).IsReadOnly;

        /// <summary>
        ///     Remove all nodes from the linked list
        /// </summary>
        public void Clear()
        {
            var current = _linkedList.First;
            while (current != null)
            {
                ReleaseNode(current);
                current = current.Next;
            }

            _linkedList.Clear();
        }

        /// <summary>
        ///     Determine whether a certain value is in the linked list
        /// </summary>
        /// <param name="value">Specify value</param>
        /// <returns>Is a certain value in the linked list</returns>
        public bool Contains(T value) => _linkedList.Contains(value);

        /// <summary>
        ///     Starting from the specified index of the target array, copy the entire linked list to a compatible one-dimensional
        ///     array
        /// </summary>
        /// <param name="array">
        ///     One-dimensional array, It is a target array of elements copied from a linked list that must have a
        ///     zero based index
        /// </param>
        /// <param name="index">array Zero based indexing in, Start copying from here</param>
        public void CopyTo(T[] array, int index) => _linkedList.CopyTo(array, index);

        /// <summary>
        ///     Remove the first match of the specified value from the linked list
        /// </summary>
        /// <param name="value">Specify value</param>
        /// <returns>Was removal successful</returns>
        public bool Remove(T value)
        {
            var node = _linkedList.Find(value);
            if (node != null)
            {
                _linkedList.Remove(node);
                ReleaseNode(node);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Add value to ICollection`1 At the end of
        /// </summary>
        /// <param name="value">Value to be added</param>
        void ICollection<T>.Add(T value) => AddLast(value);

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
        ///     Add a new node containing the specified value after the existing node specified in the linked list
        /// </summary>
        /// <param name="node">Existing node specified</param>
        /// <param name="value">Specify value</param>
        /// <returns>A new node containing the specified value</returns>
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            var newNode = AcquireNode(value);
            _linkedList.AddAfter(node, newNode);
            return newNode;
        }

        /// <summary>
        ///     Add the specified new node after the existing node specified in the linked list
        /// </summary>
        /// <param name="node">Existing node specified</param>
        /// <param name="newNode">Designated new node</param>
        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode) => _linkedList.AddAfter(node, newNode);

        /// <summary>
        ///     Add a new node containing the specified value before the existing node specified in the linked list
        /// </summary>
        /// <param name="node">Existing node specified</param>
        /// <param name="value">Specify value</param>
        /// <returns>A new node containing the specified value</returns>
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            var newNode = AcquireNode(value);
            _linkedList.AddBefore(node, newNode);
            return newNode;
        }

        /// <summary>
        ///     Add the specified new node before the existing node specified in the linked list
        /// </summary>
        /// <param name="node">Existing node specified</param>
        /// <param name="newNode">Designated new node</param>
        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode) => _linkedList.AddBefore(node, newNode);

        /// <summary>
        ///     Add a new node containing the specified value at the beginning of the linked list
        /// </summary>
        /// <param name="value">Specify value</param>
        /// <returns>A new node containing the specified value</returns>
        public LinkedListNode<T> AddFirst(T value)
        {
            var node = AcquireNode(value);
            _linkedList.AddFirst(node);
            return node;
        }

        /// <summary>
        ///     Add a specified new node at the beginning of the linked list
        /// </summary>
        /// <param name="node">Designated new node</param>
        public void AddFirst(LinkedListNode<T> node) => _linkedList.AddFirst(node);

        /// <summary>
        ///     Add a new node containing the specified value at the end of the linked list
        /// </summary>
        /// <param name="value">Specify value</param>
        /// <returns>A new node containing the specified value</returns>
        public LinkedListNode<T> AddLast(T value)
        {
            var node = AcquireNode(value);
            _linkedList.AddLast(node);
            return node;
        }

        /// <summary>
        ///     Add the specified new node at the end of the linked list
        /// </summary>
        /// <param name="node">Designated new node</param>
        public void AddLast(LinkedListNode<T> node) => _linkedList.AddLast(node);

        /// <summary>
        ///     Clear linked list node cache
        /// </summary>
        public void ClearCachedNodes() => _cachedNodes.Clear();

        /// <summary>
        ///     Find the first node containing the specified value
        /// </summary>
        /// <param name="value">The specified value to search for</param>
        /// <returns>The first node containing the specified value</returns>
        public LinkedListNode<T> Find(T value) => _linkedList.Find(value);

        /// <summary>
        ///     Find the last node containing the specified value
        /// </summary>
        /// <param name="value">The specified value to search for</param>
        /// <returns>The last node containing the specified value</returns>
        public LinkedListNode<T> FindLast(T value) => _linkedList.FindLast(value);

        /// <summary>
        ///     Remove the specified node from the linked list
        /// </summary>
        /// <param name="node">Designated node</param>
        public void Remove(LinkedListNode<T> node)
        {
            _linkedList.Remove(node);
            ReleaseNode(node);
        }

        /// <summary>
        ///     Remove the node located at the beginning of the linked list
        /// </summary>
        public void RemoveFirst()
        {
            var first = _linkedList.First;
            if (first == null)
                return;
            _linkedList.RemoveFirst();
            ReleaseNode(first);
        }

        /// <summary>
        ///     Remove nodes at the end of the linked list
        /// </summary>
        public void RemoveLast()
        {
            var last = _linkedList.Last;
            if (last == null)
                return;
            _linkedList.RemoveLast();
            ReleaseNode(last);
        }

        /// <summary>
        ///     Returns the enumerator for loop access to a collection
        /// </summary>
        /// <returns>The enumerator for iteratively accessing a collection</returns>
        private ErinnEnumerator GetEnumerator() => new(_linkedList);

        private LinkedListNode<T> AcquireNode(T value)
        {
            LinkedListNode<T> node;
            if (_cachedNodes.Count > 0)
            {
                node = _cachedNodes.Dequeue();
                node.Value = value;
            }
            else
            {
                node = new LinkedListNode<T>(value);
            }

            return node;
        }

        private void ReleaseNode(LinkedListNode<T> node)
        {
            node.Value = default;
            _cachedNodes.Enqueue(node);
        }

        /// <summary>
        ///     The enumerator for iteratively accessing a collection
        /// </summary>
        private struct ErinnEnumerator : IEnumerator<T>
        {
            private LinkedList<T>.Enumerator _enumerator;

            public ErinnEnumerator(LinkedList<T> linkedList) => _enumerator = linkedList.GetEnumerator();

            /// <summary>
            ///     Get the current node
            /// </summary>
            public T Current => _enumerator.Current;

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
            readonly void IEnumerator.Reset() => ((IEnumerator<T>)_enumerator).Reset();
        }
    }
}