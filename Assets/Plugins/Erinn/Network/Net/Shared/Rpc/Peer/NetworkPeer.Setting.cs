//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Network Peer
    /// </summary>
    public abstract partial class NetworkPeer
    {
        /// <summary>
        ///     Network tick setting
        /// </summary>
        private NetworkSetting _setting = NetworkSetting.Default;

        /// <summary>
        ///     Network tick setting
        /// </summary>
        public NetworkSetting Setting => _setting;

        /// <summary>
        ///     Set configuration
        /// </summary>
        /// <param name="setting">Allocation</param>
        public void Set(NetworkSetting setting)
        {
            if (!CheckSet())
                return;
            _setting = setting;
            Log.Info($"Setup: NoDelay[{setting.NoDelay}] Interval[{setting.Tick}]");
        }

        /// <summary>
        ///     Set hosting
        /// </summary>
        /// <param name="managed">Hosting</param>
        public void SetManaged(bool managed)
        {
#if !UNITY_2021_3_OR_NEWER && !GODOT
            if (!CheckSet())
                return;
            _setting.Managed = managed;
            Log.Warning($"Setup: Hosting[{managed}]");
#else
            Log.Warning("Unsupported");
#endif
        }

        /// <summary>
        ///     Set no delay
        /// </summary>
        /// <param name="noDelay">No delay</param>
        public void SetNoDelay(bool noDelay)
        {
            if (!CheckSet())
                return;
            _setting.NoDelay = noDelay;
            Log.Info($"Setup: NoDelay[{noDelay}]");
        }

        /// <summary>
        ///     Set interval
        /// </summary>
        /// <param name="tick">Interval</param>
        public void SetTick(uint tick)
        {
            if (!CheckSet())
                return;
            if (tick < 1U)
                tick = 1U;
            _setting.Tick = tick;
            Log.Info($"Setup: Interval[{tick}]");
        }
    }
}