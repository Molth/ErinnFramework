//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class EnumGenerator : EditorWindow
    {
        private static string _namespaceName = "";
        private readonly List<string> _enumValues = new();
        private string _enumName;
        private Vector2 _scrollPosition;
        private bool _useBitWise;

        public static string PathDataKey
        {
            get => EditorPrefs.GetString("EnumGenerator");
            private set => EditorPrefs.SetString("EnumGenerator", value);
        }

        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Input namespace");
            _namespaceName = EditorGUILayout.TextField(_namespaceName);
            GUILayout.Space(5);
            GUILayout.Label("Enter enumeration name");
            _enumName = EditorGUILayout.TextField(_enumName);
            GUILayout.Space(5);
            GUILayout.Label("Using bitwise operations");
            _useBitWise = GUILayout.Toggle(_useBitWise, "");
            GUILayout.Space(5);
            GUILayout.Label("Enter element name");
            GUILayout.Space(5);
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            for (var i = 0; i < _enumValues.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label((i + 1).ToString(), GUILayout.Width(25));
                GUILayout.Space(5);
                _enumValues[i] = EditorGUILayout.TextField(_enumValues[i]);
                if (GUILayout.Button("Remove")) _enumValues.RemoveAt(i);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            GUILayout.Space(10);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add"))
                _enumValues.Add("");
            GUILayout.Space(5);
            if (GUILayout.Button("Generate"))
            {
                if (_enumName == "" || _enumValues.Count == 0)
                    return;
                var path = OpenPanel();
                GenerateCode(path, _enumName, _enumValues, _useBitWise);
            }

            GUILayout.Space(8);
        }

        private static string OpenPanel()
        {
            var str = EditorUtility.OpenFolderPanel("Select a folder", PathDataKey, "");
            if (!string.IsNullOrEmpty(str))
                PathDataKey = str;
            return str;
        }

        public static void ShowWindow() => GetWindow(typeof(EnumGenerator)).titleContent = new GUIContent("Enumeration value generator");

        private static void GenerateCode(string generatePath, string enumName, List<string> enumValues, bool useBitWise)
        {
            var sb = new StringBuilder();
            _namespaceName = _namespaceName.Replace(" ", "");
            if (_namespaceName.IsNullOrEmpty())
                _namespaceName = "Erinn";
            sb.AppendLine($"namespace {_namespaceName}");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic enum " + enumName);
            sb.AppendLine("\t{");
            if (enumValues.Count == 1)
            {
                var valueName = enumValues[0].Replace(" ", "_");
                if (valueName != "")
                    sb.AppendLine("\t\t" + valueName + " = 0");
            }
            else
            {
                enumValues = enumValues.HashDistinct();
                var index = 0;
                if (useBitWise)
                {
                    for (var i = 0; i < enumValues.Count; i++)
                    {
                        var valueName = enumValues[i].Replace(" ", "_");
                        if (valueName == "") continue;
                        if (i == 0)
                        {
                            sb.AppendLine("\t\t" + valueName + " = 0" + ",");
                        }
                        else
                        {
                            sb.AppendLine("\t\t" + valueName + " = " + (1 << index) + ",");
                            index += 1;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < enumValues.Count; ++i)
                    {
                        var valueName = enumValues[i].Replace(" ", "_");
                        if (valueName == "") continue;
                        if (i == 0)
                        {
                            sb.AppendLine("\t\t" + valueName + " = 0" + ",");
                        }
                        else
                        {
                            index += 1;
                            sb.AppendLine("\t\t" + valueName + " = " + index + ",");
                        }
                    }
                }
            }

            var ppIndex = sb.ToString().LastIndexOf(",", StringComparison.Ordinal);
            if (ppIndex != -1) sb.Remove(ppIndex, 1);
            sb.AppendLine("\t}");
            sb.Append("}");
            string folderPath;
            if (string.IsNullOrEmpty(generatePath))
            {
                folderPath = "Assets/Scripts/Enum/";
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
            }
            else
            {
                folderPath = generatePath;
            }

            var path = folderPath + enumName + ".cs";
            File.WriteAllText(path, sb.ToString());
            Debug.Log(InspectorTextDict.SetColor("00FFFF", "Script storage location: ") + InspectorTextDict.SetColor("00FF00", folderPath + enumName));
            AssetDatabase.Refresh();
        }
    }
}