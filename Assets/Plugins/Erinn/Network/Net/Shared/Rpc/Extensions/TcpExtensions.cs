//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
using System.IO;
#endif

#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Tcp extensions
    /// </summary>
    public static class TcpExtensions
    {
        /// <summary>
        ///     Send
        /// </summary>
        /// <param name="stream">Flow</param>
        /// <param name="sendBuffer">Send buffer</param>
        /// <param name="headerBuffer">Header buffer</param>
        /// <param name="outgoing">Outgoing</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RawWrite(this NetworkStream stream, byte[] sendBuffer, byte[] headerBuffer, ConcurrentQueue<ArraySegment<byte>> outgoing)
        {
            var count = 0;
            while (outgoing.TryDequeue(out var payload))
            {
                try
                {
                    var size = payload.Count;
                    var newCount = count + 4;
                    switch (newCount)
                    {
                        case < 1024:
                            NetworkSerializer.Write(sendBuffer, count, size);
                            count += 4;
                            break;
                        case > 1024:
                            NetworkSerializer.Write(headerBuffer, size);
                            var overflow = newCount - 1024;
                            var remaining = 4 - overflow;
                            NetworkSerializer.BlockCopy(headerBuffer, 0, sendBuffer, count, remaining);
                            stream.Write(sendBuffer, 0, 1024);
                            NetworkSerializer.BlockCopy(headerBuffer, remaining, sendBuffer, 0, overflow);
                            count = overflow;
                            break;
                        default:
                            NetworkSerializer.Write(sendBuffer, count, size);
                            stream.Write(sendBuffer, 0, 1024);
                            count = 0;
                            break;
                    }

                    newCount = count + size;
                    switch (newCount)
                    {
                        case < 1024:
                            NetworkSerializer.BlockCopy(in payload, 0, sendBuffer, count, size);
                            count += size;
                            break;
                        case > 1024:
                            var overflow = newCount - 1024;
                            var remaining = size - overflow;
                            NetworkSerializer.BlockCopy(in payload, 0, sendBuffer, count, remaining);
                            stream.Write(sendBuffer, 0, 1024);
                            NetworkSerializer.BlockCopy(in payload, remaining, sendBuffer, 0, overflow);
                            count = overflow;
                            break;
                        default:
                            NetworkSerializer.BlockCopy(in payload, 0, sendBuffer, count, size);
                            stream.Write(sendBuffer, 0, 1024);
                            count = 0;
                            break;
                    }
                }
                finally
                {
                    NetworkBytesPool.Return(payload.Array);
                }
            }

            if (count == 0)
                return;
            stream.Write(sendBuffer, 0, count);
        }

        /// <summary>
        ///     Read exactly
        /// </summary>
        /// <param name="stream">Flow</param>
        /// <param name="receiveBuffer">Receive buffer</param>
        /// <param name="minimumBytes">MinimumBytes</param>
        /// <exception cref="IOException">Receive failed</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RawReadExactly(this NetworkStream stream, byte[] receiveBuffer, int minimumBytes)
        {
            var num = stream.Read(receiveBuffer, 0, minimumBytes);
            if (num == minimumBytes)
                return;
            if (num == 0)
                throw new IOException("End of stream.");
            var start = num;
            do
            {
                var remaining = minimumBytes - start;
                num = stream.Read(receiveBuffer, start, remaining);
                if (num == 0)
                    throw new IOException("End of stream.");
                start += num;
            } while (start < minimumBytes);
        }
    }
}