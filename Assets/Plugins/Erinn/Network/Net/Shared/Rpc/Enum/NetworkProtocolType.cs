//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Protocol Type
    /// </summary>
    public enum NetworkProtocolType : uint
    {
        /// <summary>
        ///     ENet
        /// </summary>
        Udp,

        /// <summary>
        ///     Onryo
        /// </summary>
        Tcp,

        /// <summary>
        ///     WebSocket
        /// </summary>
        WebSocket
    }
}