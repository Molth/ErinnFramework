//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

namespace Erinn
{
    /// <summary>
    ///     Rpc Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NotifyAttribute : Attribute
    {
        /// <summary>
        ///     Command
        /// </summary>
        public readonly uint Command;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="command">Command</param>
        public NotifyAttribute(uint command) => Command = command;
    }
}