//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class GeneratorTool : EditorWindow
    {
        private Vector2 _scrollPosition;

        private void OnGUI()
        {
            GUILayout.Space(15);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            DoAction("Label Name", EnumTag.GenerateTagConst);
            DoAction("Scene Name", EnumTag.GenerateSceneNameConst);
            DoAction("Scene index", EnumTag.GenerateSceneIndexConst);
            DoAction("Layer Name ", EnumTag.GenerateLayerNameConst);
            DoAction("Layer keys", EnumTag.GenerateLayerIndexConst);
            DoAction("Sort layer names", EnumTag.GenerateSortedLayerConst);
            DoAction("Sort layer keys", EnumTag.GenerateSortedLayerIndexConst);
            DoAction("Axis name", EnumTag.GenerateAxisConst);
            DoAction("Resource Path", EnumTag.ResourcesLoad);
            DoAction("State machine", FSMGenerator.GenerateFSM);
            DoAction("Enum ", EnumGenerator.ShowWindow);
            DoAction("Copyright information", CopyrightTool.GenerateCopyright);
            GUILayout.EndScrollView();
        }

        public static void OpenGenerateNormalTool() => GetWindow(typeof(GeneratorTool)).titleContent = new GUIContent("Regular Code Generator");

        private static void DoAction(string method, Action action)
        {
            if (GUILayout.Button(method))
                action.Invoke();
            GUILayout.Space(5);
        }
    }
}