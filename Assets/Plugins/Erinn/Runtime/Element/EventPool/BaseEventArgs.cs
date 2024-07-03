//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Event base class
    /// </summary>
    public abstract class BaseEventArgs : EventArgs, IReference
    {
        /// <summary>
        ///     Get type number
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        ///     Clean up references
        /// </summary>
        public abstract void Clear();
    }
}