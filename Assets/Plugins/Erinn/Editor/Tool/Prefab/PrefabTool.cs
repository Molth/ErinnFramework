//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class PrefabTool : EditorWindow
    {
        private Vector2 _scrollPosition;

        private void OnGUI()
        {
            GUILayout.Space(15);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DoAction("Check memory usage", PrefabMemoryChecker.BeginMemoryCheck);
            DoAction("Check the X-ray target", UIRaycastChecker.BeginRaycastCheck);
            GUILayout.EndScrollView();
        }

        public static void OpenPrefabTool() => GetWindow(typeof(PrefabTool)).titleContent = new GUIContent("Prefabricated tool");

        private static void DoAction(string method, Action action)
        {
            if (GUILayout.Button(method)) action.Invoke();
            GUILayout.Space(5);
        }
    }
}