//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Erinn
{
    /// <summary>
    ///     Object Pooling, Only perform access operations, Do not perform any other operations
    ///     Memory swap efficiency
    ///     Use Generics, Do not disassemble the box, Do not recycle
    /// </summary>
    [Serializable]
    internal sealed class HashPool<T> : IPool<T> where T : class
    {
        /// <summary>
        ///     Hash stack
        /// </summary>
        private readonly HashSet<T> _pool;

        /// <summary>
        ///     Stack
        /// </summary>
        [ShowInInspector] private readonly Stack<T> _stack;

        /// <summary>
        ///     Constructor
        /// </summary>
        private HashPool(T obj)
        {
            _pool = new HashSet<T> { obj };
            _stack = new Stack<T>();
            _stack.Push(obj);
        }

        /// <summary>
        ///     Number of objects
        /// </summary>
        public int Count => _pool.Count;

        /// <summary>
        ///     Pop up object
        /// </summary>
        /// <returns>Obtained object</returns>
        public T Pop()
        {
            var result = _stack.Pop();
            _pool.Remove(result);
            return result;
        }

        /// <summary>
        ///     Push Object
        /// </summary>
        /// <param name="obj">Object to be pushed in</param>
        /// <returns>Whether the object was successfully pushed</returns>
        public bool Push(T obj)
        {
            var result = _pool.Add(obj);
            if (result)
                _stack.Push(obj);
            return result;
        }

        /// <summary>
        ///     Empty object pool
        /// </summary>
        public void Clear()
        {
            _pool.Clear();
            _stack.Clear();
        }

        /// <summary>
        ///     Attempt to pop up object
        /// </summary>
        /// <param name="result">Result</param>
        /// <returns>Did it successfully pop up</returns>
        public bool TryPop(out T result)
        {
            if (_stack.Count > 0)
            {
                result = _stack.Pop();
                _pool.Remove(result);
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        ///     Generate object pool
        /// </summary>
        /// <param name="obj">Element</param>
        /// <returns>Generated object pool</returns>
        public static HashPool<T> Create(T obj) => new(obj);
    }
}