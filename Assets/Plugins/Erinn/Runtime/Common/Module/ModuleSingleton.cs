//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Module singleton
    /// </summary>
    internal abstract class ModuleSingleton : IModuleSingleton
    {
        /// <summary>
        ///     Framework module priority
        /// </summary>
        public abstract int Priority { get; }

        /// <summary>
        ///     Call during initialization
        /// </summary>
        public virtual void OnInit()
        {
        }

        /// <summary>
        ///     Call upon deletion
        /// </summary>
        public virtual void OnDispose()
        {
        }
    }
}