//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;

#pragma warning disable CS8604

namespace Erinn
{
    /// <summary>
    ///     Network Hash catch
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    internal static class NetworkHashCache<T> where T : struct, INetworkMessage, IMemoryPackable<T>
    {
        /// <summary>
        ///     Id32
        /// </summary>
        public static readonly uint Id32 = NetworkHash.Hash32(typeof(T).FullName);
    }
}