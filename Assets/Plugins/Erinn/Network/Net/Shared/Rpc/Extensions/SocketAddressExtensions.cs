//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Buffers.Binary;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

#pragma warning disable CS8632

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace Erinn
{
    /// <summary>
    ///     SocketAddress extensions
    /// </summary>
    public static class SocketAddressExtensions
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>SocketAddress</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SocketAddress Create(this SocketAddress socketAddress)
        {
            var newSocketAddress = new SocketAddress(socketAddress.Family, socketAddress.Size);
            for (var i = 0; i < socketAddress.Size; ++i)
                newSocketAddress[i] = socketAddress[i];
            return newSocketAddress;
        }

        /// <summary>
        ///     Create IPEndPoint
        /// </summary>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>IPEndPoint</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IPEndPoint? CreateIPEndPoint(this SocketAddress socketAddress)
        {
            IPAddress? ipAddress = null;
            var family = socketAddress.Family;
#if !UNITY_2021_3_OR_NEWER && !GODOT
            var socketAddressBuffer = socketAddress.Buffer.Span;
            if (family == AddressFamily.InterNetwork)
            {
                var ipv4Address = BinaryPrimitives.ReadUInt32LittleEndian(socketAddressBuffer[4..]);
                ipAddress = new IPAddress((long)ipv4Address & 4294967295);
            }

            if (family == AddressFamily.InterNetworkV6)
            {
                Span<byte> ipv6Address = stackalloc byte[16];
                socketAddressBuffer.Slice(8, ipv6Address.Length).CopyTo(ipv6Address);
                var scope = BinaryPrimitives.ReadUInt32LittleEndian(socketAddressBuffer[24..]);
                ipAddress = new IPAddress(ipv6Address, ipv6Address[0] == 254 && (ipv6Address[1] & 192) == 128 ? (long)scope : 0);
            }

            if (ipAddress == null)
                return null;
            var port = BinaryPrimitives.ReadUInt16BigEndian(socketAddressBuffer[2..]);
            return new IPEndPoint(ipAddress, port);
#else
            Span<byte> socketAddressBuffer = stackalloc byte[socketAddress.Size];
            for (var i = 0; i < socketAddress.Size; ++i)
                socketAddressBuffer[i] = socketAddress[i];
            if (family == AddressFamily.InterNetwork)
            {
                var ipv4Address = BinaryPrimitives.ReadUInt32LittleEndian(socketAddressBuffer[4..]);
                ipAddress = new IPAddress((long)ipv4Address & 4294967295);
            }

            if (family == AddressFamily.InterNetworkV6)
            {
                Span<byte> ipv6Address = stackalloc byte[16];
                socketAddressBuffer.Slice(8, ipv6Address.Length).CopyTo(ipv6Address);
                var scope = BinaryPrimitives.ReadUInt32LittleEndian(socketAddressBuffer[24..]);
                ipAddress = new IPAddress(ipv6Address, ipv6Address[0] == 254 && (ipv6Address[1] & 192) == 128 ? (long)scope : 0);
            }

            if (ipAddress == null)
                return null;
            var port = BinaryPrimitives.ReadUInt16BigEndian(socketAddressBuffer[2..]);
            return new IPEndPoint(ipAddress, port);
#endif
        }
    }
}