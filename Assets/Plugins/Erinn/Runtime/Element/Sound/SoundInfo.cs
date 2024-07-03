//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    /// <summary>
    ///     Sound information
    /// </summary>
    internal readonly struct SoundInfo
    {
        /// <summary>
        ///     Audio resource component
        /// </summary>
        public readonly AudioSource Source;

        /// <summary>
        ///     Zoom
        /// </summary>
        public readonly float Scale;

        /// <summary>
        ///     Time stamp
        /// </summary>
        public readonly ulong Timestamp;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="source">Audio resource component</param>
        /// <param name="scale">Zoom</param>
        /// <param name="timestamp">Time stamp</param>
        public SoundInfo(AudioSource source, float scale, ulong timestamp)
        {
            Source = source;
            Scale = scale;
            Timestamp = timestamp;
        }

        /// <summary>
        ///     Set volume
        /// </summary>
        /// <param name="volume">Volume</param>
        public void SetVolume(float volume) => Source.volume = volume * Scale;
    }
}