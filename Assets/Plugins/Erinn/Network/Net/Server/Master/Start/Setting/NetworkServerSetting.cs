//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Erinn
{
    /// <summary>
    ///     Server Config
    /// </summary>
    public struct NetworkServerSetting
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static NetworkServerSetting Default => new(NetworkProtocolType.Tcp, 7777, 1500U);

        /// <summary>
        ///     Default path
        /// </summary>
        public static string DefaultPath => NetworkJson.Combine("server", "setting.txt");

        /// <summary>
        ///     Protocol Type
        /// </summary>
        public NetworkProtocolType ProtocolType { get; set; }

        /// <summary>
        ///     Port
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        ///     Maximum connection
        /// </summary>
        public uint MaxClients { get; set; }

        /// <summary>
        ///     Serialize to json
        /// </summary>
        /// <returns>Json</returns>
        public override string ToString() => NetworkJson.Serialize(this);

        /// <summary>
        ///     Deserialize to setting
        /// </summary>
        /// <param name="json">Json</param>
        /// <returns>Config</returns>
        public static NetworkServerSetting Create(string json) => NetworkJson.Deserialize<NetworkServerSetting>(json);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <returns>NetworkServerConfiguration</returns>
        public static NetworkServerSetting GetValueOrDefault() => GetValueOrDefault(DefaultPath);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>NetworkServerConfiguration</returns>
        public static NetworkServerSetting GetValueOrDefault(string path) => NetworkJson.GetValueOrDefault(path, Default);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="setting">Default</param>
        /// <returns>NetworkServerConfiguration</returns>
        public static NetworkServerSetting GetValueOrDefault(NetworkServerSetting setting) => GetValueOrDefault(DefaultPath, setting);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="setting">Default</param>
        /// <returns>NetworkServerConfiguration</returns>
        public static NetworkServerSetting GetValueOrDefault(string path, NetworkServerSetting setting) => NetworkJson.GetValueOrDefault(path, setting);

        /// <summary>
        ///     Write and return value
        /// </summary>
        /// <param name="setting">Value</param>
        /// <returns>NetworkServerConfiguration</returns>
        public static NetworkServerSetting WriteValue(NetworkServerSetting setting) => WriteValue(DefaultPath, setting);

        /// <summary>
        ///     Write and return value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="setting">Value</param>
        /// <returns>NetworkServerConfiguration</returns>
        public static NetworkServerSetting WriteValue(string path, NetworkServerSetting setting) => NetworkJson.WriteValue(path, setting);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="networkProtocolType">Protocol Type</param>
        /// <param name="port">Port</param>
        /// <param name="maxClients">Maximum connection</param>
        public NetworkServerSetting(NetworkProtocolType networkProtocolType, ushort port, uint maxClients = 4095U)
        {
            ProtocolType = networkProtocolType;
            Port = port;
            MaxClients = maxClients;
        }
    }
}