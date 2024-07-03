//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class AddressableTool
    {
        private static void ClearMissingGroups()
        {
            for (var index = Settings.groups.Count - 1; index >= 0; --index)
                if (Settings.groups[index] == null)
                    Settings.groups.RemoveAt(index);
        }

        public static void AddressableOpen()
        {
            AddressableCreate();
            EditorApplication.ExecuteMenuItem("Window/Asset Management/Addressables/Groups");
        }

        public static void AddressableUpdate()
        {
            AddressableCreate();
            var directories = Directory.GetDirectories("Assets/AddressableResources");
            var length = directories.Length;
            if (length == 0)
            {
                Debug.Log("<color=#FF0000>Addressables lack grouping!</color> <color=#FFFF33>Unable to update settings!</color>");
            }
            else
            {
                for (var index = 0; index < length; ++index)
                    directories[index] = directories[index].Replace('\\', '/');
                var array = new string[length];
                for (var index = 0; index < length; ++index)
                    array[index] = directories[index].Split('/')[^1];
                for (var index = 0; index < array.Length; ++index)
                    GetAddressableGroup(array[index], directories[index]);
                var dict = GetAddressableDict();
                var jsonData = JsonConvert.SerializeObject(dict);
                if (!Directory.Exists("Assets/Resources"))
                    Directory.CreateDirectory("Assets/Resources");
                var filePath = Path.Combine("Assets/Resources", "AddressablePathData.txt");
                File.WriteAllText(filePath, jsonData);
                AssetDatabase.Refresh();
                Debug.Log("<color=#00FFFF>Addressables settings</color> <color=#00FF00>Update completed!</color>");
            }
        }

        public static void AddressableDelete()
        {
            if (!Directory.Exists("Assets/AddressableAssetsData/"))
                return;
            Directory.Delete("Assets/AddressableAssetsData/", true);
            if (File.Exists("Assets/AddressableAssetsData.meta"))
                File.Delete("Assets/AddressableAssetsData.meta");
            Debug.Log("<color=#FF0000>Addressables settings deleted!</color>");
            AssetDatabase.Refresh();
        }

        public static void UpdateAddressableNames()
        {
            var dict = GetAddressableDict();
            if (dict == null)
                return;
            if (!Directory.Exists("Assets/Scripts/Const/Path/Addressable"))
                Directory.CreateDirectory("Assets/Scripts/Const/Path/Addressable");
            foreach (var pair in dict)
                GenerateStruct(pair.Key, pair.Value);
        }

        private static void GenerateStruct(string name, HashSet<string> names)
        {
            var stringBuilder = new StringBuilder(1024);
            stringBuilder.Append("namespace Erinn\r\n{\r\n");
            stringBuilder.AppendFormat("\tpublic readonly struct {0}\r\n", name + "Path");
            stringBuilder.Append("\t{\r\n");
            foreach (var t in names)
            {
                var str = Replace(t.Split('/')[1]);
                stringBuilder.AppendFormat("\t\tpublic const string {0} = @\"{1}\";\r\n", str, t);
            }

            stringBuilder.Append("\t}\r\n");
            stringBuilder.Append("}");
            if (!File.Exists("Assets/Scripts/Const/Path/Addressable/" + name + "Path.cs"))
                Debug.Log("<color=#FF0000>Addressables Group </color><color=#00FFFF>" + name + "</color><color=#00FF00> Path script generation completed! Storage path: </color><color=#FFFF33>Assets/Scripts/Const/Path/Addressable/" + name + "Path</color>");
            else
                Debug.Log("<color=#FF0000>Addressables Group </color><color=#00FFFF>" + name + "</color><color=#00FF00> Path script update completed! Storage path: </color><color=#FFFF33>Assets/Scripts/Const/Path/Addressable/" + name + "Path</color>");
            File.WriteAllText("Assets/Scripts/Const/Path/Addressable/" + name + "Path.cs", stringBuilder.ToString());
            AssetDatabase.Refresh();
        }

        public static void AddressableUpdateGroup(string group, string path)
        {
            AddressableCreate();
            GetAddressableGroup(group, path);
            var dict = GetAddressableDict();
            var jsonData = JsonConvert.SerializeObject(dict);
            if (!Directory.Exists("Assets/Resources"))
                Directory.CreateDirectory("Assets/Resources");
            var filePath = Path.Combine("Assets/Resources", "AddressablePathData.txt");
            File.WriteAllText(filePath, jsonData);
            AssetDatabase.Refresh();
            Debug.Log("<color=#00FFFF>Addressables settings</color> <color=#00FF00>Update completed!</color>");
        }
    }
}