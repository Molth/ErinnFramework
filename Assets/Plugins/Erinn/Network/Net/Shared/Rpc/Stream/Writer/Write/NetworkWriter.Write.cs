//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace Erinn
{
    /// <summary>
    ///     Write
    /// </summary>
    public readonly partial struct NetworkWriter
    {
        /// <summary>
        ///     Write
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(in T obj) => _buffer.Write(in obj);

        /// <summary>
        ///     Write
        /// </summary>
        /// <param name="obj">object</param>
        /// <typeparam name="T">Type</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUnsafe<T>(in T obj) where T : unmanaged => _buffer.WriteUnsafe(in obj);
    }
}