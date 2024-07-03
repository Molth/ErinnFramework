//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEditor;

namespace Erinn
{
    [InitializeOnLoad]
    internal static class EditorManager
    {
        private static Action<EditorWindow> _onMaximizedChanged;
        private static bool _isMaximized;

        static EditorManager()
        {
            EditorApplication.update -= Update;
            Selection.selectionChanged -= Select;
            MathV.Combine(ref EditorApplication.update, Update);
            MathV.Combine(ref Selection.selectionChanged, Select);
            if (FocusedWindow != null)
                _isMaximized = FocusedWindow.maximized;
        }

        private static EditorWindow FocusedWindow => EditorWindow.focusedWindow;
        private static event Action OnUpdate;
        private static event Action OnSelectChanged;

        public static void TryCombineOnUpdate(Action action)
        {
            if (!OnUpdate.Contains(action))
                OnUpdate += action;
        }

        public static void RemoveOnUpdate(Action action) => OnUpdate -= action;

        public static void TryCombineOnSelectChanged(Action action)
        {
            if (!OnSelectChanged.Contains(action))
                OnSelectChanged += action;
        }

        public static void TryCombineOnMaximizedChanged(Action<EditorWindow> action)
        {
            if (!_onMaximizedChanged.Contains(action))
                _onMaximizedChanged += action;
        }

        private static void Update()
        {
            if (FocusedWindow != null)
            {
                var flag = FocusedWindow.maximized;
                if (_isMaximized != flag)
                {
                    _isMaximized = flag;
                    var maximizedChanged = _onMaximizedChanged;
                    maximizedChanged?.Invoke(FocusedWindow);
                }
            }

            var onUpdate = OnUpdate;
            onUpdate?.Invoke();
        }

        private static void Select()
        {
            var onSelectChanged = OnSelectChanged;
            onSelectChanged?.Invoke();
        }
    }
}