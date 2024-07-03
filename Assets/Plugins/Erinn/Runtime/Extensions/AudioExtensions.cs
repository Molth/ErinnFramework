//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static void Play(this AudioSource audioSource, AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        public static void Play(this AudioSource audioSource, AudioClip clip, float volume)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }

        public static void Play(this AudioSource audioSource, AudioClip clip, bool loop)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }

        public static void Play(this AudioSource audioSource, AudioClip clip, float volume, bool loop)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();
        }

        public static async UniTask PlayAsync(this AudioSource audioSource, AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
        }

        public static async UniTask PlayAsync(this AudioSource audioSource, AudioClip clip, float volume)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
        }

        public static async UniTask PlayAsync(this AudioSource audioSource, AudioClip clip, bool loop)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
        }

        public static async UniTask PlayAsync(this AudioSource audioSource, AudioClip clip, float volume, bool loop)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
        }

        public static async void PlayAsync(this AudioSource audioSource, AudioClip clip, Action onComplete)
        {
            audioSource.clip = clip;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
            onComplete.Invoke();
        }

        public static async void PlayAsync(this AudioSource audioSource, AudioClip clip, float volume, Action onComplete)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
            onComplete.Invoke();
        }

        public static async void PlayAsync(this AudioSource audioSource, AudioClip clip, bool loop, Action onComplete)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
            onComplete.Invoke();
        }

        public static async void PlayAsync(this AudioSource audioSource, AudioClip clip, float volume, bool loop, Action onComplete)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
            onComplete.Invoke();
        }

        public static async UniTask PlayOneShotAsync(this AudioSource audioSource, AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
        }

        public static async UniTask PlayOneShotAsync(this AudioSource audioSource, AudioClip clip, float volumeScale)
        {
            audioSource.PlayOneShot(clip, volumeScale);
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
        }

        public static async void PlayOneShotAsync(this AudioSource audioSource, AudioClip clip, Action onComplete)
        {
            audioSource.PlayOneShot(clip);
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
            onComplete.Invoke();
        }

        public static async void PlayOneShotAsync(this AudioSource audioSource, AudioClip clip, float volumeScale, Action onComplete)
        {
            audioSource.PlayOneShot(clip, volumeScale);
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), true);
            onComplete.Invoke();
        }
    }
}