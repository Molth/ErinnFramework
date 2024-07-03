//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

// ReSharper disable InconsistentNaming

namespace Erinn
{
    /// <summary>
    ///     Remote call properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RequireRPCAttribute : Attribute
    {
        /// <summary>
        ///     Allocated
        /// </summary>
        public readonly bool Allocated;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="allocated">Allocated</param>
        public RequireRPCAttribute(bool allocated = false) => Allocated = allocated;
    }
}