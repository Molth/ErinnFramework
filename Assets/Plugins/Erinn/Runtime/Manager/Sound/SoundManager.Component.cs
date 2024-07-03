//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class SoundManager
    {
        /// <summary>
        ///     Idle queue that can be played
        /// </summary>
        private static readonly Queue<AudioSource> IdleQueue = new();

        /// <summary>
        ///     Playing List
        /// </summary>
        private static readonly Dictionary<uint, SoundInfo> PlayingDict = new();

        /// <summary>
        ///     Audio group
        /// </summary>
        private static GameObject _audioObj;

        /// <summary>
        ///     Background audio
        /// </summary>
        private static AudioSource _bgmAudioSource;

        /// <summary>
        ///     Audio setup
        /// </summary>
        public static AudioSetting Setting { get; private set; }
    }
}