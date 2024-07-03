//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network Event Type
    /// </summary>
    public enum NetworkEventType : byte
    {
        /// <summary>
        ///     Connect
        /// </summary>
        Connect,

        /// <summary>
        ///     Receive data
        /// </summary>
        Data,

        /// <summary>
        ///     Disconnect
        /// </summary>
        Disconnect
    }
}