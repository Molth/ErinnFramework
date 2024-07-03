//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using MemoryPack;

namespace Erinn
{
    /// <summary>
    ///     MessageProcessor interface
    /// </summary>
    /// <typeparam name="TS">Type</typeparam>
    /// <typeparam name="T">Type</typeparam>
    internal interface INetworkServerGateProcessor<TS, in T> where TS : Service, new() where T : struct, INetworkMessage, IMemoryPackable<T>
    {
        /// <summary>
        ///     Invoke
        /// </summary>
        /// <param name="id">ClientId</param>
        /// <param name="obj">Data</param>
        void Invoke(uint id, T obj);
    }
}