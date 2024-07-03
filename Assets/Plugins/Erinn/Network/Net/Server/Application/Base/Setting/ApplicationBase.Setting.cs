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
        ///     Get Configuration
        /// </summary>
        /// <returns>Obtained configuration</returns>
        public NetworkSetting Setting => _peer.Setting;

        /// <summary>
        ///     Set configuration
        /// </summary>
        /// <param name="setting">Allocation</param>
        public void Set(NetworkSetting setting) => _peer.Set(setting);

        /// <summary>
        ///     Set hosting
        /// </summary>
        /// <param name="managed">Hosting</param>
        public void SetManaged(bool managed) => _peer.SetManaged(managed);

        /// <summary>
        ///     Set no delay
        /// </summary>
        /// <param name="noDelay">No delay</param>
        public void SetNoDelay(bool noDelay) => _peer.SetNoDelay(noDelay);

        /// <summary>
        ///     Set interval
        /// </summary>
        /// <param name="tick">Interval</param>
        public void SetTick(uint tick) => _peer.SetTick(tick);
    }
}