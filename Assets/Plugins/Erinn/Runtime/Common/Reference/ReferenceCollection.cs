//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.Collections.Generic;
using System.Threading;
#endif

#pragma warning disable CS8600
#pragma warning disable CS8603
#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Reference Collection
    /// </summary>
    internal sealed class ReferenceCollection
    {
        /// <summary>
        ///     Idle stack
        /// </summary>
        private readonly Queue<IReference> _idleQueue = new();

        /// <summary>
        ///     Type
        /// </summary>
        public readonly Type InfoType;

        /// <summary>
        ///     Lock
        /// </summary>
        private SpinLock _lock;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="referenceType">Reference type</param>
        public ReferenceCollection(Type referenceType) => InfoType = referenceType;

        /// <summary>
        ///     Idle quantity
        /// </summary>
        public int UnusedCount => _idleQueue.Count;

        /// <summary>
        ///     Obtain
        /// </summary>
        public T Acquire<T>() where T : class, IReference, new()
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                return _idleQueue.TryDequeue(out var item) ? (T)item : new T();
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }

        /// <summary>
        ///     Obtain
        /// </summary>
        public IReference Acquire()
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                return _idleQueue.TryDequeue(out var item) ? item : (IReference)Activator.CreateInstance(InfoType);
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }

        /// <summary>
        ///     Release
        /// </summary>
        public void Release(IReference reference)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                if (_idleQueue.Contains(reference))
                    return;
                _idleQueue.Enqueue(reference);
                reference.Clear();
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }

        /// <summary>
        ///     Add
        /// </summary>
        public void Add<T>(int count) where T : class, IReference, new()
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                while (count-- > 0)
                    _idleQueue.Enqueue(new T());
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }

        /// <summary>
        ///     Add
        /// </summary>
        public void Add(int count)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                while (count-- > 0)
                    _idleQueue.Enqueue((IReference)Activator.CreateInstance(InfoType));
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }

        /// <summary>
        ///     Remove
        /// </summary>
        public void Remove(int count)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                if (count > _idleQueue.Count)
                    count = _idleQueue.Count;
                while (count-- > 0)
                    _idleQueue.TryDequeue(out _);
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }

        /// <summary>
        ///     Remove all
        /// </summary>
        public void RemoveAll() => _idleQueue.Clear();
    }
}