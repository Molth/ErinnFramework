//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Udp extensions
    /// </summary>
    public static class UdpExtensions
    {
        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendNonBlocking(this Socket socket, byte[] sendBuffer)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.Send(sendBuffer, 0, sendBuffer.Length, SocketFlags.None);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="size">Size</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendNonBlocking(this Socket socket, byte[] sendBuffer, int size)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.Send(sendBuffer, 0, size, SocketFlags.None);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="size">Size</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendNonBlocking(this Socket socket, byte[] sendBuffer, int offset, int size)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.Send(sendBuffer, offset, size, SocketFlags.None);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="remoteEndPoint">Remote endPoint</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendToNonBlocking(this Socket socket, byte[] sendBuffer, EndPoint remoteEndPoint)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.SendTo(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, remoteEndPoint);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="size">Size</param>
        /// <param name="remoteEndPoint">Remote endPoint</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendToNonBlocking(this Socket socket, byte[] sendBuffer, int size, EndPoint remoteEndPoint)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.SendTo(sendBuffer, 0, size, SocketFlags.None, remoteEndPoint);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="size">Size</param>
        /// <param name="remoteEndPoint">Remote endPoint</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendToNonBlocking(this Socket socket, byte[] sendBuffer, int offset, int size, EndPoint remoteEndPoint)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.SendTo(sendBuffer, offset, size, SocketFlags.None, remoteEndPoint);
            return true;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="count">Count</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveNonBlocking(this Socket socket, byte[] receiveBuffer, out int count)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.Receive(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="size">Size</param>
        /// <param name="count">Count</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveNonBlocking(this Socket socket, byte[] receiveBuffer, int size, out int count)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.Receive(receiveBuffer, 0, size, SocketFlags.None);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="size">Size</param>
        /// <param name="count">Count</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveNonBlocking(this Socket socket, byte[] receiveBuffer, int offset, int size, out int count)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.Receive(receiveBuffer, offset, size, SocketFlags.None);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="count">Count</param>
        /// <param name="remoteEndPoint">Remote endPoint</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveFromNonBlocking(this Socket socket, byte[] receiveBuffer, out int count, ref EndPoint remoteEndPoint)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.ReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None, ref remoteEndPoint);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="size">Size</param>
        /// <param name="count">Count</param>
        /// <param name="remoteEndPoint">Remote endPoint</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveFromNonBlocking(this Socket socket, byte[] receiveBuffer, int size, out int count, ref EndPoint remoteEndPoint)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.ReceiveFrom(receiveBuffer, 0, size, SocketFlags.None, ref remoteEndPoint);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="size">Size</param>
        /// <param name="count">Count</param>
        /// <param name="remoteEndPoint">Remote endPoint</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveFromNonBlocking(this Socket socket, byte[] receiveBuffer, int offset, int size, out int count, ref EndPoint remoteEndPoint)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.ReceiveFrom(receiveBuffer, offset, size, SocketFlags.None, ref remoteEndPoint);
            return count > 0;
        }

#if !UNITY_2021_3_OR_NEWER && !GODOT
        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendToNonBlocking(this Socket socket, byte[] sendBuffer, SocketAddress socketAddress)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.SendTo(sendBuffer, SocketFlags.None, socketAddress);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="size">Size</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendToNonBlocking(this Socket socket, byte[] sendBuffer, int size, SocketAddress socketAddress)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.SendTo(sendBuffer.AsSpan(0, size), SocketFlags.None, socketAddress);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="size">Size</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendToNonBlocking(this Socket socket, byte[] sendBuffer, int offset, int size, SocketAddress socketAddress)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.SendTo(sendBuffer.AsSpan(offset, size), SocketFlags.None, socketAddress);
            return true;
        }

        /// <summary>
        ///     Send to non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Sent</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SendToNonBlocking(this Socket socket, ReadOnlySpan<byte> sendBuffer, SocketAddress socketAddress)
        {
            if (!socket.Poll(0, SelectMode.SelectWrite))
                return false;
            socket.SendTo(sendBuffer, SocketFlags.None, socketAddress);
            return true;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="count">Count</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveFromNonBlocking(this Socket socket, byte[] receiveBuffer, out int count, SocketAddress socketAddress)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.ReceiveFrom(receiveBuffer, SocketFlags.None, socketAddress);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="size">Size</param>
        /// <param name="count">Count</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveFromNonBlocking(this Socket socket, byte[] receiveBuffer, int size, out int count, SocketAddress socketAddress)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.ReceiveFrom(receiveBuffer.AsSpan(0, size), SocketFlags.None, socketAddress);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="offset">Offset</param>
        /// <param name="size">Size</param>
        /// <param name="count">Count</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveFromNonBlocking(this Socket socket, byte[] receiveBuffer, int offset, int size, out int count, SocketAddress socketAddress)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.ReceiveFrom(receiveBuffer.AsSpan(offset, size), SocketFlags.None, socketAddress);
            return count > 0;
        }

        /// <summary>
        ///     Receive from non-blocking
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="count">Count</param>
        /// <param name="socketAddress">SocketAddress</param>
        /// <returns>Received</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReceiveFromNonBlocking(this Socket socket, Span<byte> receiveBuffer, out int count, SocketAddress socketAddress)
        {
            count = !socket.Poll(0, SelectMode.SelectRead) ? 0 : socket.ReceiveFrom(receiveBuffer, SocketFlags.None, socketAddress);
            return count > 0;
        }
#endif
    }
}