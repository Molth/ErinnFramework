//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
using MemoryPack;
#if UNITY_2021_3_OR_NEWER || GODOT
using System.Threading;
#endif

// ReSharper disable StaticMemberInGenericType

namespace Erinn
{
    /// <summary>
    ///     NetworkMessage pool
    /// </summary>
    public static class NetworkMessagePool<T> where T : struct, INetworkMessage, IMemoryPackable<T>
    {
        /// <summary>
        ///     Pool
        /// </summary>
        private static readonly T[] Pool = new T[64];

        /// <summary>
        ///     Lock
        /// </summary>
        private static SpinLock _lock;

        /// <summary>
        ///     Index
        /// </summary>
        private static int _index;

        /// <summary>
        ///     Rent
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Rent()
        {
            var buffer = Pool;
            var obj = new T();
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                if (_index < 64)
                {
                    obj = buffer[_index];
                    buffer[_index++] = new T();
                }
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }

            return obj;
        }

        /// <summary>
        ///     Return
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(in T obj)
        {
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                var returned = _index != 0;
                if (returned)
                    Pool[--_index] = obj;
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }
    }
}