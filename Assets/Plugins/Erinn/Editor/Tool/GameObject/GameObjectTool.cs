//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class GameObjectTool : EditorWindow
    {
        private Vector2 _scrollPosition;

        private void OnGUI()
        {
            GUILayout.Space(15);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DoAction("Renaming", Renamer.ShowWindow);
            DoAction("Sorter", HierarchySortingTool.ToggleWindow);
            GUILayout.EndScrollView();
        }

        public static void OpenGameObjectTool() => GetWindow(typeof(GameObjectTool)).titleContent = new GUIContent("Object tools");

        private static void DoAction(string method, Action action)
        {
            if (GUILayout.Button(method)) action.Invoke();
            GUILayout.Space(5);
        }
    }
}