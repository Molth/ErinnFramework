//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Unity.Netcode;

namespace Erinn
{
    /// <summary>
    ///     Online information interface
    /// </summary>
    public interface INetcodeMessage : IMessage, INetworkSerializable
    {
    }
}