//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    /// <summary>
    ///     Read interface
    /// </summary>
    public interface INetworkReader
    {
        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        T Read<T>();

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        void Read<T>(ref T obj);

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        T ReadUnsafe<T>() where T : unmanaged;

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        void ReadUnsafe<T>(ref T result) where T : unmanaged;
    }
}