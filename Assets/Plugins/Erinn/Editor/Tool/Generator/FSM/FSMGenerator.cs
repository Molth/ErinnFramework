//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class FSMGenerator : EditorWindow
    {
        private static string _namespaceName = "";
        private readonly List<string> _stateNames = new();
        private bool _generateBaseState;
        private bool _generateMachine;
        private Vector2 _scrollPosition;
        private string _stateMachineName = "";

        public static string PathDataKey
        {
            get => EditorPrefs.GetString("FSMGenerator");
            private set => EditorPrefs.SetString("FSMGenerator", value);
        }

        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Input namespace");
            _namespaceName = EditorGUILayout.TextField(_namespaceName);
            GUILayout.Space(5);
            GUILayout.Label("Enter owner name");
            _stateMachineName = EditorGUILayout.TextField(_stateMachineName);
            GUILayout.Space(5);
            GUILayout.Label("Generate State Machine");
            _generateMachine = GUILayout.Toggle(_generateMachine, "");
            GUILayout.Space(5);
            GUILayout.Label("Generate basic state");
            _generateBaseState = GUILayout.Toggle(_generateBaseState, "");
            GUILayout.Space(5);
            GUILayout.Label("Enter status name");
            GUILayout.Space(5);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            for (var i = 0; i < _stateNames.Count; ++i)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label((i + 1).ToString(), GUILayout.Width(25));
                GUILayout.Space(5);
                _stateNames[i] = EditorGUILayout.TextField(_stateNames[i]);
                if (GUILayout.Button("Remove"))
                    _stateNames.RemoveAt(i);
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
            GUILayout.Space(10);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add"))
                _stateNames.Add("");
            GUILayout.Space(5);
            if (GUILayout.Button("Generate"))
            {
                if (string.IsNullOrEmpty(_stateMachineName))
                    return;
                if (_stateNames.Count == 0)
                    return;
                var path = OpenPanel();
                GenerateFsmCode(path, _stateMachineName, _stateNames, _generateBaseState, _generateMachine);
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

        public static void GenerateFSM() => GetWindow(typeof(FSMGenerator)).titleContent = new GUIContent("State Machine Generator");

        private static void GenerateFsmCode(string generatePath, string ownerName, List<string> ownerStateNames, bool isGenerateBaseState, bool isGenerateMachine)
        {
            if (!isGenerateBaseState && !isGenerateMachine)
                return;
            _namespaceName = _namespaceName.Replace(" ", "");
            if (_namespaceName.IsNullOrEmpty())
                _namespaceName = "Erinn";
            string folderPath;
            if (string.IsNullOrEmpty(generatePath))
                folderPath = "Assets/Scripts/Fsm/" + ownerName;
            else
                folderPath = generatePath + $"/{ownerName}";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            var sb = new StringBuilder();
            if (isGenerateBaseState)
            {
                if (_namespaceName != "Erinn")
                    sb.Append("using Erinn;\r\n\r\n");
                sb.Append($"namespace {_namespaceName}\r\n");
                sb.Append("{\r\n");
                sb.Append($"\tpublic abstract class {ownerName}BaseState : FsmState<{ownerName}>\r\n");
                sb.Append("\t{\r\n");
                sb.Append("\t\tprotected override void OnInit(object[] args)\r\n");
                sb.Append("\t\t{\r\n");
                sb.Append("\t\t}\r\n\r\n");
                sb.Append("\t\tprotected override void OnEnter()\r\n");
                sb.Append("\t\t{\r\n");
                sb.Append("\t\t}\r\n\r\n");
                sb.Append("\t\tprotected override void OnUpdate()\r\n");
                sb.Append("\t\t{\r\n");
                sb.Append("\t\t}\r\n\r\n");
                sb.Append("\t\tprotected override void OnLateUpdate()\r\n");
                sb.Append("\t\t{\r\n");
                sb.Append("\t\t}\r\n\r\n");
                sb.Append("\t\tprotected override void OnFixedUpdate()\r\n");
                sb.Append("\t\t{\r\n");
                sb.Append("\t\t}\r\n\r\n");
                sb.Append("\t\tprotected override void OnExit()\r\n");
                sb.Append("\t\t{\r\n");
                sb.Append("\t\t}\r\n");
                sb.Append("\t}\r\n");
                sb.Append("}");
                var baseStateFilePath = Path.Combine(folderPath, $"{ownerName}BaseState.cs");
                File.WriteAllText(baseStateFilePath, sb.ToString());
            }

            ownerStateNames = ownerStateNames.HashDistinct();
            foreach (var stateName in ownerStateNames)
            {
                if (stateName.Equals("")) continue;
                sb.Clear();
                sb.Append($"namespace {_namespaceName}\r\n");
                sb.Append("{\r\n");
                sb.Append($"\tpublic sealed class {ownerName + stateName}State : {ownerName}BaseState\r\n");
                sb.Append("\t{\r\n");
                sb.Append("\t}\r\n");
                sb.Append("}");
                var stateFilePath = Path.Combine(folderPath, $"{ownerName + stateName}State.cs");
                File.WriteAllText(stateFilePath, sb.ToString());
            }

            if (isGenerateMachine)
            {
                sb.Clear();
                if (_namespaceName != "Erinn")
                    sb.Append("using Erinn;\r\n\r\n");
                sb.Append($"namespace {_namespaceName}\r\n");
                sb.Append("{\r\n");
                sb.Append($"\tpublic sealed class {ownerName}Fsm : Fsm<{ownerName}>\r\n");
                sb.Append("\t{\r\n");
                sb.Append("\t\tprotected override void OnInit(object[] args)\r\n");
                sb.Append("\t\t{\r\n");
                sb.Append("\t\t}\r\n");
                sb.Append("\t}\r\n");
                sb.Append("}");
                var stateMachineFilePath = Path.Combine(folderPath, $"{ownerName}Fsm.cs");
                File.WriteAllText(stateMachineFilePath, sb.ToString());
            }

            Debug.Log(InspectorTextDict.SetColor("00FFFF", "Script storage location: ") + InspectorTextDict.SetColor("00FF00", folderPath));
            AssetDatabase.Refresh();
        }
    }
}