//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections;
#if UNITY_2021_3_OR_NEWER
using System.Collections.Generic;
using System.Threading;
#endif

#pragma warning disable CS8601

namespace Erinn
{
    /// <summary>
    ///     Concurrent HashSet
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public sealed class ConcurrentHashSet<T> : IEnumerable<T>
    {
        /// <summary>
        ///     HashSet
        /// </summary>
        private readonly HashSet<T> _hashSet;

        /// <summary>
        ///     State lock
        /// </summary>
        private readonly ReaderWriterLockSlim _stateLock = new(LockRecursionPolicy.NoRecursion);

        /// <summary>
        ///     Structure
        /// </summary>
        public ConcurrentHashSet() => _hashSet = new HashSet<T>();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        public ConcurrentHashSet(int capacity) => _hashSet = new HashSet<T>(capacity);

        /// <summary>
        ///     Count
        /// </summary>
        public int Count
        {
            get
            {
                _stateLock.EnterReadLock();
                var result = _hashSet.Count;
                _stateLock.ExitReadLock();
                return result;
            }
        }

        /// <summary>
        ///     Get Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            _stateLock.EnterReadLock();
            try
            {
                return _hashSet.GetEnumerator();
            }
            finally
            {
                _stateLock.ExitReadLock();
            }
        }

        /// <summary>
        ///     Get Enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Added</returns>
        public bool Add(T item)
        {
            _stateLock.EnterWriteLock();
            var result = _hashSet.Add(item);
            _stateLock.ExitWriteLock();
            return result;
        }

        /// <summary>
        ///     Add range
        /// </summary>
        /// <param name="items">Items</param>
        public void AddRange(IEnumerable<T> items)
        {
            _stateLock.EnterWriteLock();
            foreach (var item in items)
                _hashSet.Add(item);
            _stateLock.ExitWriteLock();
        }

        /// <summary>
        ///     Remove
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Removed</returns>
        public bool Remove(T item)
        {
            _stateLock.EnterWriteLock();
            var result = _hashSet.Remove(item);
            _stateLock.ExitWriteLock();
            return result;
        }

        /// <summary>
        ///     Remove range
        /// </summary>
        /// <param name="items">Items</param>
        public void RemoveRange(IEnumerable<T> items)
        {
            _stateLock.EnterWriteLock();
            foreach (var item in items)
                _hashSet.Remove(item);
            _stateLock.ExitWriteLock();
        }

        /// <summary>
        ///     Contains
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Contains</returns>
        public bool Contains(T item)
        {
            _stateLock.EnterReadLock();
            var result = _hashSet.Contains(item);
            _stateLock.ExitReadLock();
            return result;
        }

        /// <summary>
        ///     Clear
        /// </summary>
        public void Clear()
        {
            _stateLock.EnterWriteLock();
            _hashSet.Clear();
            _stateLock.ExitWriteLock();
        }

        /// <summary>
        ///     Ensure capacity
        /// </summary>
        /// <param name="capacity">Capacity</param>
        /// <returns>Size</returns>
        public int EnsureCapacity(int capacity)
        {
            _stateLock.EnterWriteLock();
            var result = _hashSet.EnsureCapacity(capacity);
            _stateLock.ExitWriteLock();
            return result;
        }
    }
}