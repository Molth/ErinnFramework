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
    internal sealed class CopyrightTool : EditorWindow
    {
        public static string ProjectName
        {
            get => EditorPrefs.GetString("CopyrightProjectName");
            private set => EditorPrefs.SetString("CopyrightProjectName", value);
        }

        public static string AuthorName
        {
            get => EditorPrefs.GetString("CopyrightAuthorName");
            private set => EditorPrefs.SetString("CopyrightAuthorName", value);
        }

        public static string PathDataKey
        {
            get => EditorPrefs.GetString("CopyrightPathData");
            private set => EditorPrefs.SetString("CopyrightPathData", value);
        }

        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Enter project name");
            ProjectName = EditorGUILayout.TextField(ProjectName);
            GUILayout.Space(5);
            GUILayout.Label("Enter author name");
            AuthorName = EditorGUILayout.TextField(AuthorName);
            GUILayout.Space(5);
            if (GUILayout.Button("Generate"))
                GenerateCopyrightButton();
        }

        public static void GenerateCopyrightButton()
        {
            var str = OpenPanel();
            if (string.IsNullOrEmpty(str))
                return;
            AddPrefixToScriptFiles(str);
        }

        private static void AddPrefixToScriptFiles(string folder)
        {
            var sb = new StringBuilder();
            var scriptFiles = GetScriptFilesByFolderPath(folder);
            if (scriptFiles == null)
                return;
            var projectName = $"// {ProjectName}";
            var authorName = $"// Copyright © 2024 {AuthorName}. All rights reserved.";
            foreach (var filePath in scriptFiles)
            {
                sb.Clear();
                var content = File.ReadAllText(filePath);
                if (content.StartsWith("//------------------------------------------------------------") || content.StartsWith("#"))
                    continue;
                sb.AppendLine("//------------------------------------------------------------");
                sb.AppendLine(projectName);
                sb.AppendLine(authorName);
                sb.AppendLine("//------------------------------------------------------------");
                sb.AppendLine();
                sb.Append(content);
                File.WriteAllText(filePath, sb.ToString());
            }
        }

        public static void GenerateCopyright() => GetWindow(typeof(CopyrightTool)).titleContent = new GUIContent("Copyright Information Generator");

        private static string OpenPanel()
        {
            var str = EditorUtility.OpenFolderPanel("Select a folder", PathDataKey, "");
            if (!string.IsNullOrEmpty(str))
                PathDataKey = str;
            return str;
        }

        private static List<string> GetScriptFilesByFolderPath(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return null;
            folder = folder.TrimEnd('/');
            var length = Environment.CurrentDirectory.Length + 1;
            folder = folder[length..];
            var scriptFiles = new List<string>();
            var scriptExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".cs" };
            var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);
                if (scriptExtensions.Contains(extension))
                    scriptFiles.Add(file);
            }

            return scriptFiles;
        }
    }
}