//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Erinn
{
    internal sealed class OpenFolderTool : EditorWindow
    {
        private Vector2 _scrollPosition;

        //Current project
        private static string CurrentDirectory => Environment.CurrentDirectory;

        //Persistent data
        private static string PersistentDataPath => Application.persistentDataPath;

        //Flowing assets
        private static string StreamingAssetsPath => Application.streamingAssetsPath;

        //Project Resources
        private static string DataPath => Application.dataPath;

        //Temporary cache
        private static string TemporaryCachePath => Application.temporaryCachePath;

        //Console log 
        private static string ConsoleLogPath => Path.GetDirectoryName(Application.consoleLogPath);

        private void OnGUI()
        {
            GUILayout.Space(15);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            Execute("Project Resources", DataPath);
            Execute("Persistent data", PersistentDataPath);
            Execute("Current project", CurrentDirectory);
            Execute("Streaming assets", StreamingAssetsPath);
            Execute("Temporary cache", TemporaryCachePath);
            Execute("Console log ", ConsoleLogPath);
            GUILayout.EndScrollView();
        }

        public static void OpenFolderSelect() => GetWindow(typeof(OpenFolderTool)).titleContent = new GUIContent("Open a folder");

        private static void Execute(string folder, string path)
        {
            if (GUILayout.Button(folder))
            {
                if (!Directory.Exists(path))
                {
                    Debug.Log($"Not present {folder} Path! <color=#FFAA00>{path}</color>");
                    return;
                }

                Process.Start(path);
            }

            GUILayout.Space(5);
        }
    }
}