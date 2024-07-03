//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Erinn
{
    /// <summary>
    ///     Client Config
    /// </summary>
    public struct NetworkClientSetting
    {
        /// <summary>
        ///     Default
        /// </summary>
        public static NetworkClientSetting Default => new(NetworkProtocolType.Tcp, "127.0.0.1", 7777);

        /// <summary>
        ///     Default path
        /// </summary>
        public static string DefaultPath => NetworkJson.Combine("client", "setting.txt");

        /// <summary>
        ///     Protocol type
        /// </summary>
        public NetworkProtocolType ProtocolType { get; set; }

        /// <summary>
        ///     Server IpAddress
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        ///     Server Port
        /// </summary>
        public ushort Port { get; set; }

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
        public static NetworkClientSetting Create(string json) => NetworkJson.Deserialize<NetworkClientSetting>(json);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <returns>NetworkClientConfiguration</returns>
        public static NetworkClientSetting GetValueOrDefault() => GetValueOrDefault(DefaultPath);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>NetworkClientConfiguration</returns>
        public static NetworkClientSetting GetValueOrDefault(string path) => NetworkJson.GetValueOrDefault(path, Default);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="setting">Default</param>
        /// <returns>NetworkClientConfiguration</returns>
        public static NetworkClientSetting GetValueOrDefault(NetworkClientSetting setting) => GetValueOrDefault(DefaultPath, setting);

        /// <summary>
        ///     Read value or write and return default value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="setting">Default</param>
        /// <returns>NetworkClientConfiguration</returns>
        public static NetworkClientSetting GetValueOrDefault(string path, NetworkClientSetting setting) => NetworkJson.GetValueOrDefault(path, setting);

        /// <summary>
        ///     Write and return value
        /// </summary>
        /// <param name="setting">Value</param>
        /// <returns>NetworkClientConfiguration</returns>
        public static NetworkClientSetting WriteValue(NetworkClientSetting setting) => WriteValue(DefaultPath, setting);

        /// <summary>
        ///     Write and return value
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="setting">Value</param>
        /// <returns>NetworkClientConfiguration</returns>
        public static NetworkClientSetting WriteValue(string path, NetworkClientSetting setting) => NetworkJson.WriteValue(path, setting);

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="networkProtocolType">Protocol Type</param>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public NetworkClientSetting(NetworkProtocolType networkProtocolType, string ipAddress, ushort port)
        {
            ProtocolType = networkProtocolType;
            IPAddress = ipAddress;
            Port = port;
        }
    }
}