//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network Peer interface
    /// </summary>
    public interface INetworkPeer : ISettable, INetworkProtocol, INetworkSetting, INetworkUpdate
#if !UNITY_2021_3_OR_NEWER && !GODOT
    ;
#else
    {
    }
#endif
}