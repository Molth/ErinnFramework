//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class ScriptableObjectTool : EditorWindow
    {
        private string _createName;
        private string _name;
        private string _nameSpace;

        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Input namespace");
            _nameSpace = EditorGUILayout.TextField(_nameSpace);
            GUILayout.Space(5);
            GUILayout.Label("Enter script name");
            _name = EditorGUILayout.TextField(_name);
            GUILayout.Space(5);
            GUILayout.Label("Enter Name");
            _createName = EditorGUILayout.TextField(_createName);
            GUILayout.Space(5);
            if (GUILayout.Button("Generate"))
            {
                if (string.IsNullOrEmpty(_nameSpace) || string.IsNullOrEmpty(_name))
                    return;
                Create(_nameSpace, _name, _createName);
            }

            GUILayout.Space(5);
        }

        public static void CreateSo() => GetWindow(typeof(ScriptableObjectTool)).titleContent = new GUIContent("ScriptableObjectToolTool");

        private static void Create(string nameSpace, string soName, string createName)
        {
            var fullName = nameSpace + "." + soName;
            var instance = CreateInstance(fullName);
            if (instance != null)
            {
                if (string.IsNullOrEmpty(createName))
                    createName = soName;
                if (!Directory.Exists("Assets/So"))
                    Directory.CreateDirectory("Assets/So");
                var path = "Assets/So/" + createName + ".asset";
                if (File.Exists(path))
                    File.Delete(path);
                AssetDatabase.CreateAsset(instance, path);
                AssetDatabase.Refresh();
            }
        }
    }
}