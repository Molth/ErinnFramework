//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Online permission verification interface
    /// </summary>
    public interface INetworkIdentity
    {
        /// <summary>
        ///     Server permission verification
        /// </summary>
        /// <returns>Do you have permission</returns>
        bool IsServerAuthoritative();
    }
}