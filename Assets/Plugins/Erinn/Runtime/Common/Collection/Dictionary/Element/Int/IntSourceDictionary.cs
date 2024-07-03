//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Mapping
    /// </summary>
    public sealed class IntSourceDictionary<TValue> : SourceDictionary<int, TValue> where TValue : notnull
    {
        /// <summary>
        ///     Index Pool
        /// </summary>
        private readonly IntIndexPool _indexPool;

        /// <summary>
        ///     Structure
        /// </summary>
        public IntSourceDictionary() => _indexPool = new IntIndexPool();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        public IntSourceDictionary(int capacity) : base(capacity) => _indexPool = new IntIndexPool(capacity);

        /// <summary>
        ///     Index Pool
        /// </summary>
        protected override IndexPool<int> IndexPool => _indexPool;
    }
}