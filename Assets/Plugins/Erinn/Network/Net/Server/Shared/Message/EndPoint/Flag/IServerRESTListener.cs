//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

// ReSharper disable InconsistentNaming

namespace Erinn
{
    /// <summary>
    ///     Remote call interface
    /// </summary>
    public interface IServerRESTListener
#if !UNITY_2021_3_OR_NEWER && !GODOT
        ;
#else
    {
    }
#endif
}