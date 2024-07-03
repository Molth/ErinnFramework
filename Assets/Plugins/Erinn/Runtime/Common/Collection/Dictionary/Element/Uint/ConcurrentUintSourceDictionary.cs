//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Mapping
    /// </summary>
    public sealed class ConcurrentUintSourceDictionary<TValue> : ConcurrentSourceDictionary<uint, TValue> where TValue : notnull
    {
        /// <summary>
        ///     Index Pool
        /// </summary>
        private readonly ConcurrentUintIndexPool _indexPool;

        /// <summary>
        ///     Structure
        /// </summary>
        public ConcurrentUintSourceDictionary() => _indexPool = new ConcurrentUintIndexPool();

        /// <summary>
        ///     Index Pool
        /// </summary>
        protected override ConcurrentIndexPool<uint> IndexPool => _indexPool;
    }
}