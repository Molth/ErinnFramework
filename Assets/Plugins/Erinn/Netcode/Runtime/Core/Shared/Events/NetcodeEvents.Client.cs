//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Network events
    /// </summary>
    public static partial class NetcodeEvents
    {
        /// <summary>
        ///     Client start event
        /// </summary>
        public static event Action OnClientStarted
        {
            add
            {
                if (NetManager != null)
                    NetManager.OnClientStarted += value;
            }
            remove
            {
                if (NetManager != null)
                    NetManager.OnClientStarted -= value;
            }
        }

        /// <summary>
        ///     Client stop event
        /// </summary>
        public static event Action<bool> OnClientStopped
        {
            add
            {
                if (NetManager != null)
                    NetManager.OnClientStopped += value;
            }
            remove
            {
                if (NetManager != null)
                    NetManager.OnClientStopped -= value;
            }
        }
    }
}