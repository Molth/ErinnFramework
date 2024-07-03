//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class AddressableTool
    {
        private static AddressableAssetSettings Settings => AddressableAssetSettingsDefaultObject.Settings;

        private static void AddressableCreate()
        {
            if (Settings == null && !Directory.Exists("Assets/AddressableResources"))
            {
                AddressableAssetSettingsDefaultObject.Settings = AddressableAssetSettings.Create("Assets/AddressableAssetsData", "AddressableAssetSettings", true, true);
                Directory.CreateDirectory("Assets/AddressableResources");
                Debug.Log("<color=#FF0000>Missing addressables settings!</color> <color=#FFFF33>Generate addressables settings!</color>");
                Debug.Log("<color=#FF0000>Missing addressable resources folder!</color> <color=#FFFF33>Generate addressable resources folder!</color>");
                AssetDatabase.Refresh();
            }
            else if (Settings == null)
            {
                AddressableAssetSettingsDefaultObject.Settings = AddressableAssetSettings.Create("Assets/AddressableAssetsData", "AddressableAssetSettings", true, true);
                Debug.Log("<color=#FF0000>Missing addressables settings!</color> <color=#FFFF33>Generate addressables settings!</color>");
                AssetDatabase.Refresh();
            }
            else if (!Directory.Exists("Assets/AddressableResources"))
            {
                Directory.CreateDirectory("Assets/AddressableResources");
                Debug.Log("<color=#FF0000>Missing addressable resources folder!</color> <color=#FFFF33>Generate addressable resources folder!</color>");
                AssetDatabase.Refresh();
            }

            ClearMissingGroups();
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

        private static Dictionary<string, HashSet<string>> GetAddressableDict()
        {
            if (Settings == null)
            {
                Debug.Log("<color=#FF0000>The addressables settings does not exist!</color>");
                return null;
            }

            var dict = new Dictionary<string, HashSet<string>>();
            var ignore = new[]
            {
                "Built In Data",
                "Resources",
                "EditorSceneList",
                "Default Local Group"
            };
            var array = Array.FindAll(Settings.groups.ToArray(), t => !ignore.Contains(t.Name));
            foreach (var addressableAssetGroup in array)
            {
                var stringList = new HashSet<string>();
                foreach (var entry in addressableAssetGroup.entries)
                {
                    if (!stringList.Contains(entry.address))
                        stringList.Add(entry.address);
                    else
                        Debug.Log("Duplicate name <color=#FF0000>" + entry.address + "</color> Path: <color=#FFFF33>" + entry.AssetPath[28..] + "</color>");
                }

                dict.Add(addressableAssetGroup.Name, stringList);
            }

            return dict;
        }

        private static string Replace(string str) => Regex.Replace(Regex.Replace(str, "[^a-zA-Z0-9]", ""), "^(?![a-zA-Z])", "_");

        private static void GetAddressableGroup(string name, string folder)
        {
            var group = CreateGroup<AddressableAssetGroup>(name);
            var assetsByGuid = GetAssetsByGuid(folder);
            var assetsPaths = new List<string>();
            if (Enabled == EditorTriggerState.Disable)
            {
                foreach (var assetPath in assetsByGuid)
                {
                    var assetsAddress = GetAssetsAddress(folder, assetPath);
                    if (assetsPaths.Contains(assetsAddress))
                    {
                        Debug.Log("Duplicate name <color=#FF0000>" + assetsAddress.Split('/')[1] + "</color> Path: <color=#FFFF33>" + GetPathWithoutExtension(assetPath) + "</color>");
                        return;
                    }

                    assetsPaths.Add(assetsAddress);
                    AddAssetEntry(group, assetPath, assetsAddress);
                }
            }
            else
            {
                foreach (var assetPath in assetsByGuid)
                {
                    var assetsAddress = GetPathWithoutExtension(assetPath);
                    if (assetsPaths.Contains(assetsAddress))
                    {
                        Debug.Log("Duplicate name <color=#FF0000>" + assetsAddress.Split('/')[1] + "</color> Path: <color=#FFFF33>" + GetPathWithoutExtension(assetPath) + "</color>");
                        return;
                    }

                    assetsPaths.Add(assetsAddress);
                    AddAssetEntry(group, assetPath, assetsAddress);
                }
            }

            Debug.Log("Addressables groups: <color=#FFAA00>" + name + "</color> Count: <color=#00FF00>" + assetsPaths.Count + "</color>");
        }

        private static string GetAssetsAddress(string folder, string assetPath) => Path.GetFileNameWithoutExtension(folder) + "/" + Path.GetFileNameWithoutExtension(assetPath);

        private static string GetPathWithoutExtension(string path) => (path.Replace('\\', '/')[..path.LastIndexOf('/')] + "/" + Path.GetFileNameWithoutExtension(path))[28..];

        private static AddressableAssetGroup CreateGroup<T>(string groupName)
        {
            var group = Settings.FindGroup(groupName);
            if (!group)
                group = Settings.CreateGroup(groupName, false, false, false, Settings.DefaultGroup.Schemas, typeof(T));
            return group;
        }

        private static void AddAssetEntry(AddressableAssetGroup group, string assetPath, string address)
        {
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            AddressableAssetEntry entry = null;
            var list = MathV.ToList(group.entries);
            var count = list.Count;
            for (var i = count - 1; i >= 0; --i)
            {
                if (list[i].guid == guid)
                {
                    entry = list[i];
                    break;
                }
            }

            entry ??= Settings.CreateOrMoveEntry(guid, group, false, false);
            entry.address = address;
        }
    }
}