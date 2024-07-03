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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ClientRESTListenerAttribute : Attribute
#if !UNITY_2021_3_OR_NEWER && !GODOT
    ;
#else
    {
    }
#endif
}