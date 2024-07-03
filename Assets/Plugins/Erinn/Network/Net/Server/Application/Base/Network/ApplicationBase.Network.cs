//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Application base
    /// </summary>
    public abstract partial class ApplicationBase
    {
        /// <summary>
        ///     Call on connected
        /// </summary>
        /// <param name="id">ClientId</param>
        protected abstract void OnConnected(uint id);

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        /// <param name="id">ClientId</param>
        protected abstract void OnDisconnected(uint id);
    }
}