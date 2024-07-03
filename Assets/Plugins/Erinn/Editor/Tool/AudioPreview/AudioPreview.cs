//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Erinn
{
    internal sealed class AudioPreview
    {
        private const string EnabledEditorPref = "AudioPreviewEnabled";
        private static int? _lastPlayedAudioClipId;
        private static bool? _enabled;

        public static EditorTriggerState Enabled
        {
            get
            {
                _enabled ??= EditorPrefs.GetBool(EnabledEditorPref, true);
                return _enabled.Value ? EditorTriggerState.Enable : EditorTriggerState.Disable;
            }
            set
            {
                _enabled = value == EditorTriggerState.Enable;
                EditorPrefs.SetBool(EnabledEditorPref, _enabled.Value);
            }
        }

        [OnOpenAsset]
        private static bool OnOpenAssetCallback(int instanceId)
        {
            if (Enabled == EditorTriggerState.Disable)
                return false;
            var obj = EditorUtility.InstanceIDToObject(instanceId);
            if (obj is AudioClip audioClip)
            {
                if (IsPreviewClipPlaying())
                {
                    StopAllPreviewClips();
                    if (_lastPlayedAudioClipId.HasValue && _lastPlayedAudioClipId.Value != instanceId)
                        PlayPreviewClip(audioClip);
                }
                else
                {
                    PlayPreviewClip(audioClip);
                }

                _lastPlayedAudioClipId = instanceId;
                return true;
            }

            return false;
        }

        private static void PlayPreviewClip(AudioClip audioClip)
        {
            var unityAssembly = typeof(AudioImporter).Assembly;
            var audioUtil = unityAssembly.GetType("UnityEditor.AudioUtil");
            var methodInfo = audioUtil.GetMethod("PlayPreviewClip", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(AudioClip), typeof(int), typeof(bool) }, null);
            methodInfo?.Invoke(null, new object[] { audioClip, 0, false });
        }

        private static bool IsPreviewClipPlaying()
        {
            var unityAssembly = typeof(AudioImporter).Assembly;
            var audioUtil = unityAssembly.GetType("UnityEditor.AudioUtil");
            var methodInfo = audioUtil.GetMethod("IsPreviewClipPlaying", BindingFlags.Static | BindingFlags.Public);
            var isPlaying = methodInfo != null && (bool)methodInfo.Invoke(null, null);
            return isPlaying;
        }

        private static void StopAllPreviewClips()
        {
            var unityAssembly = typeof(AudioImporter).Assembly;
            var audioUtil = unityAssembly.GetType("UnityEditor.AudioUtil");
            var methodInfo = audioUtil.GetMethod("StopAllPreviewClips", BindingFlags.Static | BindingFlags.Public);
            methodInfo?.Invoke(null, null);
        }
    }
}