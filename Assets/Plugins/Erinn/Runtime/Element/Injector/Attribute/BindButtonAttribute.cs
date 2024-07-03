//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Bind button properties
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class BindButtonAttribute : Attribute
    {
        /// <summary>
        ///     Target button Field Name
        /// </summary>
        public readonly string Target;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="target">Target button Field Name</param>
        public BindButtonAttribute(string target) => Target = target;
    }
}