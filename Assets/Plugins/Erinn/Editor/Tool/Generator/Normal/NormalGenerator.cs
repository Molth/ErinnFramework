//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Erinn
{
    internal sealed class EnumTag
    {
        public static void GenerateTagConst() => GenerateConst("Tags", "\t\tpublic const string {0} = \"{1}\";\r\n", InternalEditorUtility.tags, 0);

        public static void GenerateSortedLayerConst() => GenerateConst("SortedLayerNames", "\t\tpublic const string {0} = \"{1}\";\r\n", GetSortingLayers(), 0);

        public static void GenerateSortedLayerIndexConst() => GenerateConst("SortedLayers", "\t\tpublic const int {0} = {1};\r\n", GetSortingLayers(), 3);

        public static void GenerateLayerIndexConst() => GenerateConst("Layers", "\t\tpublic const int {0} = {1};\r\n", InternalEditorUtility.layers, 2);

        public static void GenerateLayerNameConst() => GenerateConst("LayerNames", "\t\tpublic const string {0} = \"{1}\";\r\n", InternalEditorUtility.layers, 0);

        public static void GenerateSceneNameConst() => GenerateConst("Scenes", "\t\tpublic const string {0} = \"{1}\";\r\n", MathV.ToList(GetSceneNames()), 0);

        public static void GenerateSceneIndexConst() => GenerateConst("SortedScenes", "\t\tpublic const int {0} = {1};\r\n", MathV.ToList(GetSceneNames()), 1);

        public static void GenerateAxisConst() => GenerateConst("Axises", "\t\tpublic const string {0} = \"{1}\";\r\n", MathV.ToList(InputAxis()), 0);

        private static void GenerateConst(string name, string pattern, IReadOnlyList<string> names, int type)
        {
            if (names.Count == 0)
            {
                Debug.Log("<color=#00FFFF>No name</color> <color=#00FF00>Do not generate</color>");
                return;
            }

            if (!Directory.Exists("Assets/Scripts/Const/Enum"))
                Directory.CreateDirectory("Assets/Scripts/Const/Enum");
            var stringBuilder = new StringBuilder(1024);
            stringBuilder.Append("namespace Erinn\r\n{\r\n");
            stringBuilder.AppendFormat("\tpublic readonly struct {0}\r\n", name);
            stringBuilder.Append("\t{\r\n");
            var list = new List<string>();
            for (var index = 0; index < names.Count; ++index)
            {
                var str = names[index].Replace(" ", "");
                var newStr = Replace(str);
                if (newStr.Equals("_"))
                {
                    Debug.Log("Name is empty, invalid, or is\"_\" <color=#FF0000>" + str + "</color> Please change the name!");
                    continue;
                }

                if (list.Contains(newStr))
                {
                    Debug.Log("Duplicate name <color=#FF0000>" + str + "</color> Please change the name!");
                    continue;
                }

                list.Add(newStr);
                switch (type)
                {
                    case 0:
                        stringBuilder.AppendFormat(pattern, newStr, names[index]);
                        break;
                    case 1:
                        stringBuilder.AppendFormat(pattern, newStr, index);
                        break;
                    case 2:
                        stringBuilder.AppendFormat(pattern, newStr, LayerMask.NameToLayer(names[index]));
                        break;
                    case 3:
                        stringBuilder.AppendFormat(pattern, newStr, SortingLayer.GetLayerValueFromName(names[index]));
                        break;
                }
            }

            stringBuilder.Append("\t}\r\n");
            stringBuilder.Append("}");
            if (!File.Exists("Assets/Scripts/Const/Enum/" + name + ".cs"))
                Debug.Log("Generate <color=#00FFFF>" + name + "</color> Success! Storage path: <color=#00FF00>Assets/Scripts/Const/Enum/" + name + "</color>");
            else
                Debug.Log("Update <color=#00FFFF>" + name + "</color> Success! Storage path: <color=#00FF00>Assets/Scripts/Const/Enum/" + name + "</color>");
            File.WriteAllText("Assets/Scripts/Const/Enum/" + name + ".cs", stringBuilder.ToString());
            AssetDatabase.Refresh();
        }

        private static string Replace(string str) => Regex.Replace(Regex.Replace(str, "[^a-zA-Z0-9]", ""), "^(?![a-zA-Z])", "_");

        private static string GetPathWithoutExtension(string path) => (path.Replace('\\', '/')[..path.LastIndexOf('/')] + "/" + Path.GetFileNameWithoutExtension(path))[17..];

        public static void ResourcesLoad()
        {
            if (!Directory.Exists("Assets/Resources"))
            {
                Directory.CreateDirectory("Assets/Resources");
                Debug.Log("<color=#00FFFF>Missing resources folder</color> <color=#00FF00>Generate resources folder</color>");
                AssetDatabase.Refresh();
            }
            else
            {
                var array = GetAssetsByGuid("Assets/Resources");
                if (array == null || array.Length == 0)
                {
                    Debug.Log("<color=#00FFFF>Empty</color> <color=#00FF00>Stop</color>");
                    return;
                }

                for (var index = 0; index < array.Length; ++index)
                    array[index] = GetPathWithoutExtension(array[index]);
                if (!Directory.Exists("Assets/Scripts/Const/Path/Resource"))
                    Directory.CreateDirectory("Assets/Scripts/Const/Path/Resource");
                var stringBuilder = new StringBuilder(1024);
                stringBuilder.Append("namespace Erinn\r\n{\r\n");
                stringBuilder.AppendFormat("\tpublic readonly struct {0}\r\n", "ResourcesPath");
                stringBuilder.Append("\t{\r\n");
                var stringList = new HashSet<string>();
                foreach (var path in array)
                {
                    var str = Replace(Path.GetFileNameWithoutExtension(path));
                    if (!stringList.Contains(str))
                    {
                        stringList.Add(str);
                        stringBuilder.AppendFormat("\t\tpublic const string {0} = @\"{1}\";\r\n", str, path);
                    }
                    else
                    {
                        Debug.Log("Duplicate name <color=#FF0000>" + str + "</color> Path: <color=#FFFF33>" + path + "</color>");
                    }
                }

                stringBuilder.Append("\t}\r\n");
                stringBuilder.Append("}");
                const string scriptPath = "Assets/Scripts/Const/Path/Resource/ResourcesPath";
                Debug.Log($"<color=#FF0000>Normal</color> <color=#00FFFF>Resources</color> <color=#00FF00>Generate scripts complete! Path: </color><color=#FFFF33>{scriptPath}</color>");
                File.WriteAllText($"{scriptPath}.cs", stringBuilder.ToString());
                AssetDatabase.Refresh();
            }
        }

        private static string[] GetAssetsByGuid(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return null;
            folder = folder.TrimEnd('/');
            var assets = AssetDatabase.FindAssets("t:Object", new[] { folder });
            var length = assets.Length;
            for (var index = 0; index < length; ++index)
                assets[index] = AssetDatabase.GUIDToAssetPath(assets[index]);
            var array = Array.FindAll(assets, path => !AssetDatabase.IsValidFolder(path));
            return array;
        }

        private static string[] GetSceneNames()
        {
            var sceneNames = new string[EditorBuildSettings.scenes.Length];
            for (var index = 0; index < EditorBuildSettings.scenes.Length; ++index)
            {
                var withoutExtension = Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[index].path);
                sceneNames[index] = withoutExtension;
            }

            return sceneNames;
        }

        private static List<string> InputAxis()
        {
            var axes = new HashSet<string>();
            var inputManagerProp = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
            foreach (SerializedProperty axe in inputManagerProp.FindProperty("m_Axes"))
            {
                var name = axe.FindPropertyRelative("m_Name").stringValue;
                if (!axes.Contains(name))
                    axes.Add(name);
                else
                    Debug.Log("Duplicate name <color=#FF0000>" + name + "</color>");
            }

            return MathV.ToList(axes);
        }

        private static string[] GetSortingLayers()
        {
            var strArr = new string[SortingLayer.layers.Length];
            for (var i = 0; i < SortingLayer.layers.Length; ++i)
                strArr[i] = SortingLayer.layers[i].name;
            return strArr;
        }
    }
}