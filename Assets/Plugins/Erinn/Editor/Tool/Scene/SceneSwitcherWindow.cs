//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Erinn
{
    internal sealed class SceneSwitcherWindow : EditorWindow
    {
        private static List<(string, string)> _scenePaths = new();
        private bool _openSceneAdditive;
        private Vector2 _scrollPos;

        private void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            ShowWindow();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            _openSceneAdditive = EditorGUILayout.Toggle("Additional Open", _openSceneAdditive);
            if (GUILayout.Button("Refresh", GUILayout.Width(100))) ShowWindow();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, false, false, GUILayout.Width(230));
            foreach (var (sceneName, scenePath) in _scenePaths)
            {
                if (!GUILayout.Button(sceneName, GUILayout.Width(180f)))
                    continue;
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    continue;
                if (_openSceneAdditive)
                    EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                else
                    EditorSceneManager.OpenScene(scenePath);
            }

            EditorGUILayout.EndScrollView();
            GUILayout.FlexibleSpace();
            EditorGUILayout.Space(8);
        }

        private void OnBecameVisible() => ShowWindow();

        public static void ShowWindow()
        {
            var window = GetWindow<SceneSwitcherWindow>("Scene selector");
            window.Show();
            PopulateScenes();
        }

        private static void PopulateScenes()
        {
            _scenePaths = new List<(string, string)>();
            var files = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });
            foreach (var t in files)
            {
                var path = AssetDatabase.GUIDToAssetPath(t);
                var sceneName = path[(path.LastIndexOf('/') + 1)..];
                sceneName = sceneName.Replace(".unity", "");
                _scenePaths.Add((sceneName, path));
            }

            _scenePaths.Sort();
        }
    }
}