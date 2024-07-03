//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net;
#if UNITY_2021_3_OR_NEWER || GODOT
using System.Collections.Generic;
#endif

#pragma warning disable CS8632

// ReSharper disable UseCollectionExpression

namespace Erinn
{
    /// <summary>
    ///     Network server
    /// </summary>
    public abstract partial class NetworkServerPeer
    {
        /// <summary>
        ///     IPAddress blacklist
        /// </summary>
        private readonly ConcurrentHashSet<string> _blacklist = new();

        /// <summary>
        ///     Blacklist path
        /// </summary>
        private static string BlacklistPath => NetworkJson.Combine("server", "blacklist.txt");

        /// <summary>
        ///     Write blacklist
        /// </summary>
        private void WriteBlacklist()
        {
            var count = _blacklist.Count;
            if (count == 0)
                return;
            var path = BlacklistPath;
            if (!NetworkJson.CreateDirectory(path))
                return;
            var blacklist = new List<string>(count);
            foreach (var ipAddress in _blacklist)
                blacklist.Add(ipAddress);
            var contents = NetworkJson.Serialize(blacklist);
            NetworkJson.Write(path, contents);
        }

        /// <summary>
        ///     Read blacklist
        /// </summary>
        private void ReadBlacklist()
        {
            var path = BlacklistPath;
            var json = NetworkJson.Read(path);
            if (string.IsNullOrEmpty(json))
                return;
            List<string>? blacklist;
            try
            {
                blacklist = NetworkJson.Deserialize<List<string>>(json);
            }
            catch
            {
                return;
            }

            if (blacklist == null || blacklist.Count == 0)
                return;
            _blacklist.AddRange(blacklist);
        }

        /// <summary>
        ///     Check whether IPAddress is banned
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        protected bool CheckBanned(string ipAddress) => string.IsNullOrEmpty(ipAddress) || _blacklist.Contains(ipAddress);

        /// <summary>
        ///     Check whether IPAddress is banned
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        protected bool CheckBanned(IPAddress ipAddress) => ipAddress == null || CheckBanned(ipAddress.ToString());

        /// <summary>
        ///     Check whether IPAddress is banned
        /// </summary>
        /// <param name="ipEndPoint">IPEndPoint</param>
        protected bool CheckBanned(IPEndPoint ipEndPoint) => ipEndPoint == null || CheckBanned(ipEndPoint.Address);

        /// <summary>
        ///     Add connection to blacklist
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        protected void AddToBlacklist(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return;
            _blacklist.Add(ipAddress);
            Log.Error($"Unexpected: [{ipAddress}]");
        }

        /// <summary>
        ///     Add connection to blacklist
        /// </summary>
        /// <param name="ipAddress">IPAddress</param>
        protected void AddToBlacklist(IPAddress ipAddress)
        {
            if (ipAddress == null)
                return;
            AddToBlacklist(ipAddress.ToString());
        }

        /// <summary>
        ///     Add connection to blacklist
        /// </summary>
        /// <param name="ipEndPoint">IPEndPoint</param>
        protected void AddToBlacklist(IPEndPoint ipEndPoint)
        {
            if (ipEndPoint == null)
                return;
            AddToBlacklist(ipEndPoint.Address);
        }
    }
}