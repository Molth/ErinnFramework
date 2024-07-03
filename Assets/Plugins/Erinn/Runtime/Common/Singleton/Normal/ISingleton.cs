//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Singleton interface
    /// </summary>
    internal interface ISingleton
    {
        /// <summary>
        ///     Registration singleton
        /// </summary>
        void RegisterInstance();

        /// <summary>
        ///     Singleton of destruction
        /// </summary>
        void DisposeInstance();
    }
}