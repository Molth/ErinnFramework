//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class SoundManager : ModuleSingleton, ISoundManager
    {
        public override int Priority => 4;

        void ISoundManager.SetVolume(float volume) => SetVolume(volume, true);

        void ISoundManager.SetVolume(float volume, bool save) => SetVolume(volume, save);

        void ISoundManager.SetLoop(bool loop) => SetLoop(loop);

        void ISoundManager.Play(string path) => Play(path, false);

        void ISoundManager.Play(string path, bool isLoop) => Play(path, isLoop);

        void ISoundManager.PlayAsync(string path) => PlayAsync(path, false);

        void ISoundManager.PlayAsync(string path, bool isLoop) => PlayAsync(path, isLoop);

        void ISoundManager.Pause() => Pause();

        void ISoundManager.Resume() => Resume();

        void ISoundManager.Stop() => Stop();

        void ISoundManager.Mute(bool mute) => Mute(mute);

        void ISoundManager.SetShotVolume(float volume) => SetShotVolume(volume, true);

        void ISoundManager.SetShotVolume(float volume, bool save) => SetShotVolume(volume, save);

        int ISoundManager.PlayOneShot(string path) => PlayOneShot(path);

        int ISoundManager.PlayOneShot(string path, Action onComplete) => PlayOneShot(path, onComplete);

        int ISoundManager.PlayOneShot(string path, float scale) => PlayOneShot(path, scale);

        int ISoundManager.PlayOneShot(string path, float scale, Action onComplete) => PlayOneShot(path, scale, onComplete);

        void ISoundManager.PlayOneShotAsync(string path) => PlayOneShotAsync(path);

        void ISoundManager.PlayOneShotAsync(string path, Action onComplete) => PlayOneShotAsync(path, onComplete);

        void ISoundManager.PlayOneShotAsync(string path, float scale) => PlayOneShotAsync(path, scale);

        void ISoundManager.PlayOneShotAsync(string path, float scale, Action onComplete) => PlayOneShotAsync(path, scale, onComplete);

        void ISoundManager.PauseShots() => PauseShots();

        void ISoundManager.ResumeShots() => ResumeShots();

        void ISoundManager.StopShots() => StopShots();

        void ISoundManager.MuteShots(bool mute) => MuteShots(mute);

        void ISoundManager.PlayOneShot(int id) => PlayOneShot(id);

        void ISoundManager.PauseOneShot(int id) => PauseOneShot(id);

        void ISoundManager.StopOneShot(int id) => StopOneShot(id);

        void ISoundManager.MuteOneShot(int id) => MuteOneShot(id, true);

        void ISoundManager.MuteOneShot(int id, bool mute) => MuteOneShot(id, mute);

        public static void SetVolume(float volume, bool save)
        {
            volume = MathV.Clamp(volume, 0f, 1f);
            _bgmAudioSource.volume = volume;
            Setting.Volume = volume;
            if (save)
                SaveSetting();
        }

        public static void SetLoop(bool loop) => _bgmAudioSource.loop = loop;

        public static void Play(string path, bool isLoop)
        {
            var clip = ResourceManager.Load<AudioClip>(path);
            _bgmAudioSource.loop = isLoop;
            _bgmAudioSource.clip = clip;
            _bgmAudioSource.volume = Setting.Volume;
            _bgmAudioSource.Play();
        }

        public static void PlayAsync(string path, bool isLoop) => ResourceManager.LoadAsync<AudioClip>(path, clip =>
        {
            _bgmAudioSource.loop = isLoop;
            _bgmAudioSource.clip = clip;
            _bgmAudioSource.volume = Setting.Volume;
            _bgmAudioSource.Play();
        });

        public static void Pause() => _bgmAudioSource.Pause();

        public static void Resume() => _bgmAudioSource.UnPause();

        public static void Stop() => _bgmAudioSource.Stop();

        public static void Mute(bool mute)
        {
            _bgmAudioSource.mute = mute;
            Setting.IsMute = mute;
            SaveSetting();
        }

        public static void SetShotVolume(float volume, bool save)
        {
            volume = MathV.Clamp(volume, 0f, 1f);
            foreach (var info in PlayingDict.Values)
                info.SetVolume(volume);
            Setting.ShotVolume = volume;
            if (save)
                SaveSetting();
        }

        public static int PlayOneShot(string path) => PlayOneShot(path, 1f, null);

        public static int PlayOneShot(string path, Action onComplete) => PlayOneShot(path, 1f, onComplete);

        public static int PlayOneShot(string path, float scale) => PlayOneShot(path, scale, null);

        public static int PlayOneShot(string path, float scale, Action onComplete)
        {
            var clip = ResourceManager.Load<AudioClip>(path);
            if (clip == null)
                return -1;
            scale = MathV.Clamp(scale, 0f, 1f);
            var audioSource = GetAudioSource();
            audioSource.clip = clip;
            audioSource.volume = Setting.ShotVolume * scale;
            var timerHandler = TimerManager.CreateUnscaled(clip.length, id =>
            {
                audioSource.Stop();
                audioSource.clip = null;
                PlayingDict.Remove(id);
                IdleQueue.Enqueue(audioSource);
                onComplete?.Invoke();
            });
            var id = timerHandler.Id;
            PlayingDict.Add(id, new SoundInfo(audioSource, scale, timerHandler.Timestamp));
            audioSource.Play();
            return (int)id;
        }

        public static void PlayOneShotAsync(string path) => PlayOneShotAsync(path, 1f, null);

        public static void PlayOneShotAsync(string path, Action onComplete) => PlayOneShotAsync(path, 1f, onComplete);

        public static void PlayOneShotAsync(string path, float scale) => PlayOneShotAsync(path, scale, null);

        public static void PlayOneShotAsync(string path, float scale, Action onComplete) => ResourceManager.LoadAsync<AudioClip>(path, clip =>
        {
            scale = MathV.Clamp(scale, 0f, 1f);
            var audioSource = GetAudioSource();
            audioSource.clip = clip;
            audioSource.volume = Setting.ShotVolume * scale;
            var timerHandler = TimerManager.CreateUnscaled(clip.length, id =>
            {
                audioSource.Stop();
                audioSource.clip = null;
                PlayingDict.Remove(id);
                IdleQueue.Enqueue(audioSource);
                onComplete?.Invoke();
            });
            var id = timerHandler.Id;
            PlayingDict.Add(id, new SoundInfo(audioSource, scale, timerHandler.Timestamp));
            audioSource.Play();
        });

        public static void PauseShots()
        {
            foreach (var (key, info) in PlayingDict)
            {
                info.Source.Pause();
                TimerManager.Pause(key, info.Timestamp);
            }
        }

        public static void ResumeShots()
        {
            foreach (var (key, info) in PlayingDict)
            {
                info.Source.UnPause();
                TimerManager.Play(key, info.Timestamp);
            }
        }

        public static void StopShots()
        {
            foreach (var (key, info) in PlayingDict)
            {
                TimerManager.Stop(key, info.Timestamp);
                var audioSource = info.Source;
                audioSource.Stop();
                audioSource.clip = null;
                IdleQueue.Enqueue(audioSource);
            }

            PlayingDict.Clear();
        }

        public static void MuteShots(bool mute)
        {
            foreach (var info in PlayingDict.Values)
                info.Source.mute = mute;
            Setting.ShotIsMute = mute;
            SaveSetting();
        }

        public static void PlayOneShot(int id)
        {
            if (PlayingDict.TryGetValue((uint)id, out var info))
            {
                info.Source.Play();
                TimerManager.Play((uint)id, info.Timestamp);
            }
        }

        public static void PauseOneShot(int id)
        {
            if (PlayingDict.TryGetValue((uint)id, out var info))
            {
                info.Source.Pause();
                TimerManager.Pause((uint)id, info.Timestamp);
            }
        }

        public static void StopOneShot(int id)
        {
            if (PlayingDict.TryGetValue((uint)id, out var info))
            {
                info.Source.Stop();
                TimerManager.Stop((uint)id, info.Timestamp);
            }
        }

        public static void MuteOneShot(int id, bool mute)
        {
            if (PlayingDict.TryGetValue((uint)id, out var info))
                info.Source.mute = mute;
        }

        public static void SoundSourceInit(GameObject obj)
        {
            _audioObj = obj;
            _bgmAudioSource = _audioObj.GetComponent<AudioSource>();
            if (_bgmAudioSource == null)
                _bgmAudioSource = _audioObj.AddComponent<AudioSource>();
            _bgmAudioSource.playOnAwake = false;
            _bgmAudioSource.mute = Setting.IsMute;
        }

        public override void OnInit() => LoadSetting();
    }
}