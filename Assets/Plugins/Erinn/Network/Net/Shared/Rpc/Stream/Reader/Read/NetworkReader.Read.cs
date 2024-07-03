//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace Erinn
{
    /// <summary>
    ///     Read
    /// </summary>
    public readonly partial struct NetworkReader
    {
        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Read<T>() => _buffer.Read<T>();

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Read<T>(ref T obj) => _buffer.Read(ref obj);

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ReadUnsafe<T>() where T : unmanaged => _buffer.ReadUnsafe<T>();

        /// <summary>
        ///     Read
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>object</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUnsafe<T>(ref T result) where T : unmanaged => _buffer.ReadUnsafe(ref result);
    }
}