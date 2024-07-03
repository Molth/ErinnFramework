//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Message state
    /// </summary>
    public enum MessageState : byte
    {
        /// <summary>
        ///     Timeout
        /// </summary>
        Timeout,

        /// <summary>
        ///     Null
        /// </summary>
        Nil,

        /// <summary>
        ///     Invalid
        /// </summary>
        Invalid,

        /// <summary>
        ///     Success
        /// </summary>
        Success
    }
}