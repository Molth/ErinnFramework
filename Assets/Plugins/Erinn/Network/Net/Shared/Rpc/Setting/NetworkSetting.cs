//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Erinn
{
    /// <summary>
    ///     Network setting
    /// </summary>
    public struct NetworkSetting
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static NetworkSetting Default => new(
#if !UNITY_2021_3_OR_NEWER && !GODOT
            true
#else
            false
#endif
            , false, 1U);

        /// <summary>
        ///     Default path
        /// </summary>
        public static string DefaultPath => NetworkJson.Combine("shared", "setting.txt");

        /// <summary>
        ///     Hosting
        /// </summary>
        public bool Managed { get; set; }

        /// <summary>
        ///     NoDelay
        /// </summary>
        public bool NoDelay { get; set; }

        /// <summary>
        ///     Interval
        /// </summary>
        public uint Tick { get; set; }

        /// <summary>
        ///     Serialize to json
        /// </summary>
        /// <returns>Json</returns>
        public override string ToString() => NetworkJson.Serialize(this);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <returns>NetworkSetting</returns>
        public static NetworkSetting GetValueOrDefault() => GetValueOrDefault(DefaultPath);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>NetworkSetting</returns>
        public static NetworkSetting GetValueOrDefault(string path) => NetworkJson.GetValueOrDefault(path, Default);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="setting">Default</param>
        /// <returns>NetworkSetting</returns>
        public static NetworkSetting GetValueOrDefault(NetworkSetting setting) => GetValueOrDefault(DefaultPath, setting);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="setting">Default</param>
        /// <returns>NetworkSetting</returns>
        public static NetworkSetting GetValueOrDefault(string path, NetworkSetting setting) => NetworkJson.GetValueOrDefault(path, setting);

        /// <summary>
        ///     Write and return value
        /// </summary>
        /// <param name="setting">Value</param>
        /// <returns>NetworkSetting</returns>
        public static NetworkSetting WriteValue(NetworkSetting setting) => WriteValue(DefaultPath, setting);

        /// <summary>
        ///     Write and return value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="setting">Value</param>
        /// <returns>NetworkSetting</returns>
        public static NetworkSetting WriteValue(string path, NetworkSetting setting) => NetworkJson.WriteValue(path, setting);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="managed">Hosting</param>
        /// <param name="noDelay">NoDelay</param>
        /// <param name="tick">Interval</param>
        public NetworkSetting(bool managed, bool noDelay, uint tick)
        {
            Managed = managed;
            NoDelay = noDelay;
            if (tick < 1U)
                tick = 1U;
            Tick = tick;
        }
    }
}