//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif

// ReSharper disable UseCollectionExpression

namespace Erinn
{
    /// <summary>
    ///     Network Serializer
    /// </summary>
    public static partial class NetworkSerializer
    {
        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="payload">Payload</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Serialized</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Serialize<T>(in T obj, out ArraySegment<byte> payload) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var writer = _networkBuffer ??= new NetworkBuffer(1024);
            try
            {
                writer.WriteUnsafe((byte)2);
                writer.WriteUnsafe(NetworkHash.GetId32<T>());
                var position = writer.Position;
                writer.AdvancePosition(4);
                writer.Write(obj);
                var newPosition = writer.Position;
                writer.SetPosition(position);
                writer.WriteUnsafe(newPosition - 4 - position);
                writer.SetPosition(newPosition);
                payload = writer.ToArraySegment();
                return true;
            }
            catch (Exception e)
            {
                Log.Warning(e);
                payload = new ArraySegment<byte>();
                return false;
            }
            finally
            {
                writer.SetPosition(0);
            }
        }

        /// <summary>
        ///     Serialize
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="packet">NetworkPacket</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>bytes</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Create<T>(in T obj, out NetworkPacket packet) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            var writer = _networkWriter ??= new NetworkBuffer(1024);
            try
            {
                writer.Write(in obj);
                packet = new NetworkPacket(NetworkHash.GetId32<T>(), writer.ToArraySegment());
                return true;
            }
            catch (Exception e)
            {
                Log.Warning(e);
                packet = new NetworkPacket();
                return false;
            }
            finally
            {
                writer.SetPosition(0);
            }
        }
    }
}