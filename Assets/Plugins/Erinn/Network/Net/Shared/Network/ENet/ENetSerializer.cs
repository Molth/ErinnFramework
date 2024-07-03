//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

#if UNITY_2021_3_OR_NEWER || GODOT
using System;
#endif
using System.Runtime.CompilerServices;
using ENet;
using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     Network Serializer
    /// </summary>
    public static class ENetSerializer
    {
        /// <summary>
        ///     Create packet
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="packet">Packet</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Created</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Create<T>(in T obj, out Packet packet) where T : struct, INetworkMessage, IMemoryPackable<T>
        {
            packet = new Packet();
            if (!NetworkSerializer.Serialize(in obj, out var payload))
                return false;
            packet.Create(payload.Array, payload.Offset, payload.Count, PacketFlags.Reliable);
            return true;
        }

        /// <summary>
        ///     Create packet
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <returns>Packet</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Packet RawCreate(in ArraySegment<byte> payload)
        {
            var packet = new Packet();
            packet.Create(payload.Array, payload.Offset, payload.Count, PacketFlags.Reliable);
            return packet;
        }
    }
}