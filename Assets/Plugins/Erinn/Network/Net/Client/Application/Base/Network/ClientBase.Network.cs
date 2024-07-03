//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Client Application base
    /// </summary>
    public abstract partial class ClientBase
    {
        /// <summary>
        ///     Call on connected
        /// </summary>
        protected abstract void OnConnected();

        /// <summary>
        ///     Call on disconnected
        /// </summary>
        protected abstract void OnDisconnected();
    }
}