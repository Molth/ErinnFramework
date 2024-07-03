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
        ///     Server start event
        /// </summary>
        public static event Action OnServerStarted
        {
            add
            {
                if (NetManager != null)
                    NetManager.OnServerStarted += value;
            }
            remove
            {
                if (NetManager != null)
                    NetManager.OnServerStarted -= value;
            }
        }

        /// <summary>
        ///     Server stop event
        /// </summary>
        public static event Action<bool> OnServerStopped
        {
            add
            {
                if (NetManager != null)
                    NetManager.OnServerStopped += value;
            }
            remove
            {
                if (NetManager != null)
                    NetManager.OnServerStopped -= value;
            }
        }
    }
}