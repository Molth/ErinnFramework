//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Object Pool Interface
    /// </summary>
    internal interface IPool
    {
        /// <summary>
        ///     Number of container objects
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Clear
        /// </summary>
        void Clear();
    }
}