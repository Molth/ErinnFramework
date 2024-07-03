//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Module singleton interface
    /// </summary>
    internal interface IModuleSingleton
    {
        /// <summary>
        ///     Priority
        /// </summary>
        int Priority { get; }

        /// <summary>
        ///     Initialization
        /// </summary>
        void OnInit();

        /// <summary>
        ///     Destruction
        /// </summary>
        void OnDispose();
    }
}