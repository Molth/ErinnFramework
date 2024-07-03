//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Framework entrance
    /// </summary>
    public static partial class Entry
    {
        /// <summary>
        ///     The linked list of storage modules
        /// </summary>
        private static readonly ErinnLinkedList<IModuleSingleton> ModuleSingletons = new();

        /// <summary>
        ///     Running
        /// </summary>
        public static bool IsRuntime { get; private set; }
    }
}