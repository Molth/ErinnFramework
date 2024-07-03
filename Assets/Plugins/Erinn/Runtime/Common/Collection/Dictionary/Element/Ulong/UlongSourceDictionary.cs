//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Mapping
    /// </summary>
    public sealed class UlongSourceDictionary<TValue> : SourceDictionary<ulong, TValue> where TValue : notnull
    {
        /// <summary>
        ///     Index Pool
        /// </summary>
        private readonly UlongIndexPool _indexPool;

        /// <summary>
        ///     Structure
        /// </summary>
        public UlongSourceDictionary() => _indexPool = new UlongIndexPool();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        public UlongSourceDictionary(int capacity) : base(capacity) => _indexPool = new UlongIndexPool(capacity);

        /// <summary>
        ///     Index Pool
        /// </summary>
        protected override IndexPool<ulong> IndexPool => _indexPool;
    }
}