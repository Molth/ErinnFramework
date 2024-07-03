//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEditor;

namespace Erinn
{
    internal sealed partial class AddressableTool
    {
        private const string IsFullPath = "AddressableIsFullPath";
        private static bool? _enabled;

        public static EditorTriggerState Enabled
        {
            get
            {
                _enabled ??= EditorPrefs.GetBool(IsFullPath, false);
                return _enabled.Value ? EditorTriggerState.Enable : EditorTriggerState.Disable;
            }
            set
            {
                _enabled = value == EditorTriggerState.Enable;
                EditorPrefs.SetBool(IsFullPath, _enabled.Value);
            }
        }
    }
}