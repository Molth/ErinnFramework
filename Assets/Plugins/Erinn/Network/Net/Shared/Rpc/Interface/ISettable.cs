//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Check for valid interface
    /// </summary>
    public interface ISettable
    {
        /// <summary>
        ///     Is it valid
        /// </summary>
        bool IsSet { get; }
    }
}