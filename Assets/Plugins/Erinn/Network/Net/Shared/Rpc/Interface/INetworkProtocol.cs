//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network Protocol interface
    /// </summary>
    public interface INetworkProtocol
    {
        /// <summary>
        ///     Protocol type
        /// </summary>
        NetworkProtocolType ProtocolType { get; }
    }
}