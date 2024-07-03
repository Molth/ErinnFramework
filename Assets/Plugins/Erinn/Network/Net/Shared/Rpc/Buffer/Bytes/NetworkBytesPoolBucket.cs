//------------------------------------------------------------
// Erinn Network
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Runtime.CompilerServices;
#if UNITY_2021_3_OR_NEWER || GODOT
using System.Threading;
#endif

#pragma warning disable CS8632

namespace Erinn
{
    /// <summary>
    ///     ArrayPool bucket
    /// </summary>
    internal sealed class NetworkBytesPoolBucket
    {
        /// <summary>
        ///     Buffer length
        /// </summary>
        private readonly int _bufferLength;

        /// <summary>
        ///     Buffers
        /// </summary>
        private readonly byte[]?[] _buffers;

        /// <summary>
        ///     State lock
        /// </summary>
        private SpinLock _lock;

        /// <summary>
        ///     Index
        /// </summary>
        private int _index;

        /// <summary>
        ///     Structure
        /// </summary>
        /// <param name="bufferLength">Buffer length</param>
        public NetworkBytesPoolBucket(int bufferLength)
        {
            _lock = new SpinLock();
            _buffers = new byte[4096][];
            _bufferLength = bufferLength;
        }

        /// <summary>
        ///     Allocate a new array
        /// </summary>
        /// <returns>Array</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] Allocate() => new byte[_bufferLength];

        /// <summary>
        ///     Rent array
        /// </summary>
        /// <returns>Array</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] Rent()
        {
            var buffers = _buffers;
            byte[]? buffer = null;
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                if (_index < 4096)
                {
                    buffer = buffers[_index];
                    buffers[_index++] = null;
                }
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }

            return buffer ?? Allocate();
        }

        /// <summary>
        ///     Return array
        /// </summary>
        /// <param name="length">Length</param>
        /// <param name="array">Array</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Return(int length, byte[] array)
        {
            if (length != _bufferLength)
                return;
            var lockTaken = false;
            try
            {
                _lock.Enter(ref lockTaken);
                var returned = _index != 0;
                if (returned)
                    _buffers[--_index] = array;
            }
            finally
            {
                if (lockTaken)
                    _lock.Exit(false);
            }
        }
    }
}