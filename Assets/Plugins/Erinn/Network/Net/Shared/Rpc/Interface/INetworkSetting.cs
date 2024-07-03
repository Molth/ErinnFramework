//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network configuration interface
    /// </summary>
    public interface INetworkSetting
    {
        /// <summary>
        ///     Get Configuration
        /// </summary>
        /// <returns>Obtained configuration</returns>
        NetworkSetting Setting { get; }

        /// <summary>
        ///     Set configuration
        /// </summary>
        /// <param name="setting">Allocation</param>
        void Set(NetworkSetting setting);

        /// <summary>
        ///     Set up hosting
        /// </summary>
        /// <param name="managed">Trusteeship</param>
        void SetManaged(bool managed);

        /// <summary>
        ///     Set interval
        /// </summary>
        /// <param name="tick">Interval</param>
        void SetTick(uint tick);

        /// <summary>
        ///     Set no delay
        /// </summary>
        /// <param name="noDelay">No delay</param>
        void SetNoDelay(bool noDelay);
    }
}