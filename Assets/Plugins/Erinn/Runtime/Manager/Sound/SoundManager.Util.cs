//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    internal sealed partial class SoundManager
    {
        private static AudioSource GetAudioSource()
        {
            if (IdleQueue.Count > 0)
            {
                var value = IdleQueue.Dequeue();
                value.mute = Setting.ShotIsMute;
                return value;
            }

            var audioSource = _audioObj.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.mute = Setting.ShotIsMute;
            return audioSource;
        }

        private static void LoadSetting()
        {
            if (JsonManager.LoadConfig("SoundSetting", out AudioSetting setting))
            {
                Setting = setting;
            }
            else
            {
                Setting = new AudioSetting();
                SaveSetting();
            }
        }

        private static void SaveSetting() => JsonManager.SaveConfig("SoundSetting", Setting);

        public override void OnDispose()
        {
            IdleQueue.Clear();
            PlayingDict.Clear();
            _audioObj = null;
            _bgmAudioSource = null;
            Setting = null;
        }
    }
}