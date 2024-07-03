//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Mapping
    /// </summary>
    public sealed class UintSourceDictionary<TValue> : SourceDictionary<uint, TValue> where TValue : notnull
    {
        /// <summary>
        ///     Index Pool
        /// </summary>
        private readonly UintIndexPool _indexPool;

        /// <summary>
        ///     Structure
        /// </summary>
        public UintSourceDictionary() => _indexPool = new UintIndexPool();

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="capacity">Capacity</param>
        public UintSourceDictionary(int capacity) : base(capacity) => _indexPool = new UintIndexPool(capacity);

        /// <summary>
        ///     Index Pool
        /// </summary>
        protected override IndexPool<uint> IndexPool => _indexPool;
    }
}