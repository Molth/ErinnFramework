//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Sirenix.OdinInspector;

namespace Erinn
{
    internal sealed class AudioSetting
    {
        [LabelText("Background volume")] public float Volume = 1f;
        [LabelText("Effect volume")] public float ShotVolume = 1f;
        [LabelText("Background Mute")] public bool IsMute;
        [LabelText("Effect Mute")] public bool ShotIsMute;
    }
}