//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class ExcelTool : EditorWindow
    {
        private Vector2 _scrollPosition;

        private void OnGUI()
        {
            GUILayout.Space(15);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DoAction("Read Excel Convert into scripts and generate resources", ExcelParser.ExcelToScripts);
            GUILayout.Label("Support types: \r\n\r\nint\r\nbool\r\nlong\r\nfloat\r\ndouble\r\nstring\r\nenum\r\nT(struct)\r\nVector2\r\nVector3\r\nVector4\r\nVector2Int\r\nVector3Int\r\nColor\r\nT(struct)[]");
            GUILayout.EndScrollView();
        }

        public static void OpenExcelTool() => GetWindow(typeof(ExcelTool)).titleContent = new GUIContent("ExcelTool");

        private static void DoAction(string method, Action action)
        {
            if (GUILayout.Button(method))
                action.Invoke();
            GUILayout.Space(5);
        }
    }
}