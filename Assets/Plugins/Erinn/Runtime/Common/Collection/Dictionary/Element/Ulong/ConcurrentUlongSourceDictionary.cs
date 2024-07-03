//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Mapping
    /// </summary>
    public sealed class ConcurrentUlongSourceDictionary<TValue> : ConcurrentSourceDictionary<ulong, TValue> where TValue : notnull
    {
        /// <summary>
        ///     Index Pool
        /// </summary>
        private readonly ConcurrentUlongIndexPool _indexPool;

        /// <summary>
        ///     Structure
        /// </summary>
        public ConcurrentUlongSourceDictionary() => _indexPool = new ConcurrentUlongIndexPool();

        /// <summary>
        ///     Index Pool
        /// </summary>
        protected override ConcurrentIndexPool<ulong> IndexPool => _indexPool;
    }
}