//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
// ReSharper disable PossibleNullReferenceException
#endif
#pragma warning disable CS8602

namespace Erinn
{
    /// <summary>
    ///     Network client
    /// </summary>
    public sealed partial class NetworkClient
    {
        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public bool Start(string ipAddress, ushort port)
        {
            if (_peer == null)
            {
                Log.Warning("Uninitialized");
                return false;
            }

            return _peer.Start(ipAddress, port);
        }

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="setting">Client Setting</param>
        public bool Start(NetworkClientSetting setting) => Start(setting.ProtocolType, setting.IPAddress, setting.Port);

        /// <summary>
        ///     Start client
        /// </summary>
        /// <param name="networkProtocolType">Protocol type</param>
        /// <param name="ipAddress">Server IpAddress</param>
        /// <param name="port">Server Port</param>
        public bool Start(NetworkProtocolType networkProtocolType, string ipAddress, ushort port)
        {
            if (_peer != null)
            {
                if (networkProtocolType != _peer.ProtocolType)
                {
                    Log.Warning($"Inconsistent types: [{_peer.ProtocolType}]");
                    return false;
                }

                return _peer.Start(ipAddress, port);
            }

            if (!Create(networkProtocolType))
                return false;
            _peer.Start(ipAddress, port);
            return true;
        }
    }
}