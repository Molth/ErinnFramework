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
    public sealed class ServerRESTAttribute : RESTAttribute
    {
        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="allocated">Allocated</param>
        public ServerRESTAttribute(bool allocated = false) : base(allocated)
        {
        }
    }
}