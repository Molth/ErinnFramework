//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;

namespace Erinn
{
    /// <summary>
    ///     Sound Manager
    /// </summary>
    public interface ISoundManager
    {
        /// <summary>
        ///     Set volume
        /// </summary>
        /// <param name="volume">Volume</param>
        void SetVolume(float volume);

        /// <summary>
        ///     Set volume
        /// </summary>
        /// <param name="volume">Volume</param>
        /// <param name="save">Do you want to save it</param>
        void SetVolume(float volume, bool save);

        /// <summary>
        ///     Set whether to loop
        /// </summary>
        /// <param name="loop">Is it a loop</param>
        void SetLoop(bool loop);

        /// <summary>
        ///     Play
        /// </summary>
        /// <param name="path">Path</param>
        void Play(string path);

        /// <summary>
        ///     Play
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="isLoop">Is it a loop</param>
        void Play(string path, bool isLoop);

        /// <summary>
        ///     Play
        /// </summary>
        /// <param name="path">Path</param>
        void PlayAsync(string path);

        /// <summary>
        ///     Play
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="isLoop">Is it a loop</param>
        void PlayAsync(string path, bool isLoop);

        /// <summary>
        ///     Suspend
        /// </summary>
        void Pause();

        /// <summary>
        ///     Recovery
        /// </summary>
        void Resume();

        /// <summary>
        ///     Cease
        /// </summary>
        void Stop();

        /// <summary>
        ///     Mute
        /// </summary>
        /// <param name="mute">Is it silent</param>
        void Mute(bool mute);

        /// <summary>
        ///     Set volume
        /// </summary>
        /// <param name="volume">Volume</param>
        void SetShotVolume(float volume);

        /// <summary>
        ///     Set volume
        /// </summary>
        /// <param name="volume">Volume</param>
        /// <param name="save">Do you want to save it</param>
        void SetShotVolume(float volume, bool save);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        int PlayOneShot(string path);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="onComplete">Call upon completion</param>
        int PlayOneShot(string path, Action onComplete);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="scale">Zoom</param>
        int PlayOneShot(string path, float scale);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="scale">Zoom</param>
        /// <param name="onComplete">Call upon completion</param>
        int PlayOneShot(string path, float scale, Action onComplete);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        void PlayOneShotAsync(string path);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="onComplete">Call upon completion</param>
        void PlayOneShotAsync(string path, Action onComplete);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="scale">Zoom</param>
        void PlayOneShotAsync(string path, float scale);

        /// <summary>
        ///     Play sound effects
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="scale">Zoom</param>
        /// <param name="onComplete">Call upon completion</param>
        void PlayOneShotAsync(string path, float scale, Action onComplete);

        /// <summary>
        ///     Suspend
        /// </summary>
        void PauseShots();

        /// <summary>
        ///     Recovery
        /// </summary>
        void ResumeShots();

        /// <summary>
        ///     Cease
        /// </summary>
        void StopShots();

        /// <summary>
        ///     Mute
        /// </summary>
        /// <param name="mute">Is it silent</param>
        void MuteShots(bool mute);

        /// <summary>
        ///     Play
        /// </summary>
        /// <param name="id">Index</param>
        void PlayOneShot(int id);

        /// <summary>
        ///     Suspend
        /// </summary>
        /// <param name="id">Index</param>
        void PauseOneShot(int id);

        /// <summary>
        ///     Cease
        /// </summary>
        /// <param name="id">Index</param>
        void StopOneShot(int id);

        /// <summary>
        ///     Mute
        /// </summary>
        /// <param name="id">Index</param>
        void MuteOneShot(int id);

        /// <summary>
        ///     Mute
        /// </summary>
        /// <param name="id">Index</param>
        /// <param name="mute">Mute</param>
        void MuteOneShot(int id, bool mute);
    }
}