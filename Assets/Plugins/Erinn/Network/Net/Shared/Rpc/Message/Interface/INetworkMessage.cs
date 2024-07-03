//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network Information interface
    /// </summary>
    public interface INetworkMessage
#if !UNITY_2021_3_OR_NEWER && !GODOT
        ;
#else
    {
    }
#endif
}