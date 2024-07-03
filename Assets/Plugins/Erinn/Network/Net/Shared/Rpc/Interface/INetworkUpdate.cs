//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network polling update interface
    /// </summary>
    public interface INetworkUpdate
    {
        /// <summary>
        ///     Listening
        /// </summary>
        bool IsListening { get; }

        /// <summary>
        ///     Running
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        ///     Destruction
        /// </summary>
        bool Shutdown();

        /// <summary>
        ///     Update
        /// </summary>
        void Update();
    }
}