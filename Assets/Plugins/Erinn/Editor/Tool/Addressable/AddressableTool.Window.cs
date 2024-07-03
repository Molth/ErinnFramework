//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class AddressableEditorTool : EditorWindow
    {
        private Vector2 _scrollPosition;

        private void OnGUI()
        {
            GUILayout.Space(15);
            AddressableTool.Enabled = (EditorTriggerState)EditorGUILayout.EnumPopup("Use full path", AddressableTool.Enabled);
            GUILayout.Space(5);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DoAction("Open Settings", AddressableTool.AddressableOpen);
            DoAction("Update settings", AddressableTool.AddressableUpdate);
            DoAction("Generate Path", AddressableTool.UpdateAddressableNames);
            DoAction("Delete Settings", AddressableTool.AddressableDelete);
            GUILayout.EndScrollView();
        }

        public static void OpenAddressableTool() => GetWindow(typeof(AddressableEditorTool)).titleContent = new GUIContent("AddressableTool");

        private static void DoAction(string method, Action action)
        {
            if (GUILayout.Button(method)) action.Invoke();
            GUILayout.Space(5);
        }
    }
}