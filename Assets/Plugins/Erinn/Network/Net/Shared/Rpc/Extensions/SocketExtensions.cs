//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CS8632

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace Erinn
{
    /// <summary>
    ///     Socket extensions
    /// </summary>
    public static class SocketExtensions
    {
        /// <summary>
        ///     Bind local endPoint
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="port">Port</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Bind(this Socket socket, ushort port)
        {
            if (socket.AddressFamily == AddressFamily.InterNetwork)
            {
                socket.Bind(new IPEndPoint(IPAddress.Any, port));
            }
            else if (socket.AddressFamily == AddressFamily.InterNetworkV6)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    socket.IOControl(-1744830452, new byte[1], null);
                socket.Bind(new IPEndPoint(IPAddress.IPv6Any, port));
            }
        }

        /// <summary>
        ///     Create
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <returns>SocketAddress</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SocketAddress? CreateSocketAddress(this Socket socket)
        {
            if (socket.AddressFamily == AddressFamily.InterNetwork)
                return new SocketAddress(AddressFamily.InterNetwork, 16);
            if (socket.AddressFamily == AddressFamily.InterNetworkV6)
                return new SocketAddress(AddressFamily.InterNetworkV6, 28);
            return null;
        }
    }
}