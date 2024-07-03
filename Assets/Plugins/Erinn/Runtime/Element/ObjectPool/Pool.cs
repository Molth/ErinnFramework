//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Erinn
{
    /// <summary>
    ///     Object Pooling
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public sealed class Pool<T> : IPool
    {
        /// <summary>
        ///     Queue
        /// </summary>
        private readonly Stack<T> _objects = new();

        /// <summary>
        ///     Generate
        /// </summary>
        private readonly Func<T> _objectGenerator;

        /// <summary>
        ///     Reset
        /// </summary>
        private readonly Action<T> _objectResetter;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="objectGenerator">Generate</param>
        /// <param name="objectResetter">Reset</param>
        public Pool(Func<T> objectGenerator, Action<T> objectResetter)
        {
            _objectGenerator = objectGenerator;
            _objectResetter = objectResetter;
        }

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="objectGenerator">Generate</param>
        /// <param name="objectResetter">Reset</param>
        /// <param name="initialCapacity">Capacity</param>
        public Pool(Func<T> objectGenerator, Action<T> objectResetter, int initialCapacity)
        {
            _objectGenerator = objectGenerator;
            _objectResetter = objectResetter;
            for (var i = 0; i < initialCapacity; ++i)
                _objects.Push(objectGenerator());
        }

        /// <summary>
        ///     Number of container objects
        /// </summary>
        public int Count => _objects.Count;

        /// <summary>
        ///     Clear
        /// </summary>
        public void Clear() => _objects.Clear();

        /// <summary>
        ///     Obtain T
        /// </summary>
        /// <returns>Obtained T</returns>
        public T Rent() => _objects.Count > 0 ? _objects.Pop() : _objectGenerator();

        /// <summary>
        ///     Return T
        /// </summary>
        /// <param name="item">T</param>
        public void Return(T item)
        {
            _objectResetter(item);
            _objects.Push(item);
        }
    }
}