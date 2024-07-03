//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Connection status
    /// </summary>
    public enum NetcodeConnectState : byte
    {
        None,
        Host,
        Server,
        ConnectedClient,
        Client
    }
}