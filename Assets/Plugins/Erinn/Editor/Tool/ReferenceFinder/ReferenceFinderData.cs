//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Erinn
{
    internal sealed class ReferenceFinderData
    {
        public enum AssetState : byte
        {
            Normal,
            Changed,
            Missing,
            Invalid
        }

        private const string CachePath = "Library/ReferenceFinderCache";
        public const int MinThreadCount = 8;
        private const int SingleThreadReadCount = 100;
        private static readonly int ThreadCount = Math.Max(MinThreadCount, Environment.ProcessorCount);
        private static string _basePath;

        private static readonly HashSet<string> FileExtension = new()
        {
            ".prefab",
            ".unity",
            ".mat",
            ".asset",
            ".anim",
            ".controller"
        };

        private static readonly Regex GuidRegex = new("guid: ([a-z0-9]{32})", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly Dictionary<(AssetDescription, AssetDescription), int> _dictCache = new();
        private readonly List<Dictionary<string, AssetDescription>> _threadAssetDict = new();
        private readonly List<Thread> _threadList = new();
        private int _curReadAssetCount;
        private int _totalCount;
        public string[] allAssets;
        public Dictionary<string, AssetDescription> assetDict = new();

        public void CollectDependenciesInfo()
        {
            try
            {
                _basePath = Application.dataPath.Replace("/Assets", "");
                ReadFromCache();
                allAssets = AssetDatabase.GetAllAssetPaths();
                _totalCount = allAssets.Length;
                _threadList.Clear();
                _curReadAssetCount = 0;
                foreach (var i in _threadAssetDict) i.Clear();
                _threadAssetDict.Clear();
                for (var i = 0; i < ThreadCount; i++) _threadAssetDict.Add(new Dictionary<string, AssetDescription>());
                var allThreadFinish = false;
                for (var i = 0; i < ThreadCount; i++)
                {
                    ThreadStart method = ReadAssetInfo;
                    var readThread = new Thread(method);
                    _threadList.Add(readThread);
                    readThread.Start();
                }

                while (!allThreadFinish)
                {
                    if (_curReadAssetCount % 500 == 0 && EditorUtility.DisplayCancelableProgressBar("Updating", $"Handle {_curReadAssetCount}", (float)_curReadAssetCount / _totalCount))
                    {
                        EditorUtility.ClearProgressBar();
                        foreach (var i in _threadList) i.Abort();
                        return;
                    }

                    allThreadFinish = true;
                    foreach (var i in _threadList)
                        if (i.IsAlive)
                        {
                            allThreadFinish = false;
                            break;
                        }
                }

                foreach (var dict in _threadAssetDict)
                foreach (var j in dict)
                    assetDict[j.Key] = j.Value;
                EditorUtility.DisplayCancelableProgressBar("Updating", "Write cache", 1f);
                WriteToChache();
                EditorUtility.DisplayCancelableProgressBar("Updating", "Generate reference data", 1f);
                UpdateResourceReferenceInfo();
                EditorUtility.ClearProgressBar();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                EditorUtility.ClearProgressBar();
            }
        }

        public void ReadAssetInfo()
        {
            var index = Thread.CurrentThread.ManagedThreadId % ThreadCount;
            var intervalLength = _totalCount / ThreadCount;
            var start = intervalLength * index;
            var end = start + intervalLength;
            if (_totalCount - end < intervalLength) end = _totalCount;
            var readAssetCount = 0;
            for (var i = start; i < end; i++)
            {
                if (readAssetCount % SingleThreadReadCount == 0)
                {
                    _curReadAssetCount += readAssetCount;
                    readAssetCount = 0;
                }

                GetAsset(_basePath, allAssets[i]);
                readAssetCount++;
            }
        }

        public void GetAsset(string dataPath, string assetPath)
        {
            var extLowerStr = Path.GetExtension(assetPath).ToLower();
            var needReadFile = FileExtension.Contains(extLowerStr);
            var fileName = $"{dataPath}/{assetPath}";
            var metaFile = $"{dataPath}/{assetPath}.meta";
            if (File.Exists(fileName) && File.Exists(metaFile))
            {
                var metaText = File.ReadAllText(metaFile, Encoding.UTF8);
                var matchRs = GuidRegex.Matches(metaText);
                var selfGuid = matchRs[0].Groups[1].Value.ToLower();
                var lastModifyTime = File.GetLastWriteTime(fileName).ToString(CultureInfo.InvariantCulture);
                MatchCollection guids = null;
                var depend = new List<string>();
                if (needReadFile)
                {
                    var fileStr = File.ReadAllText(fileName, Encoding.UTF8);
                    guids = GuidRegex.Matches(fileStr);
                }

                var curListIndex = Thread.CurrentThread.ManagedThreadId % ThreadCount;
                var curDict = _threadAssetDict[curListIndex];
                if (!curDict.ContainsKey(selfGuid) || curDict[selfGuid].assetDependencyHashString != lastModifyTime)
                {
                    if (guids != null)
                        for (var index = 0; index < guids.Count; ++index)
                        {
                            var i = guids[index];
                            depend.Add(i.Groups[1].Value.ToLower());
                        }

                    var ad = new AssetDescription
                    {
                        name = Path.GetFileNameWithoutExtension(assetPath),
                        path = assetPath,
                        assetDependencyHashString = lastModifyTime,
                        dependencies = depend
                    };
                    if (_threadAssetDict[curListIndex].ContainsKey(selfGuid))
                        _threadAssetDict[curListIndex][selfGuid] = ad;
                    else
                        _threadAssetDict[curListIndex].Add(selfGuid, ad);
                }
            }
        }

        private void UpdateResourceReferenceInfo()
        {
            foreach (var asset in assetDict)
            foreach (var assetGuid in asset.Value.dependencies)
                if (assetDict.ContainsKey(assetGuid))
                    assetDict[assetGuid].references.Add(asset.Key);
        }

        public bool ReadFromCache()
        {
            assetDict.Clear();
            ClearCache();
            if (File.Exists(CachePath))
            {
                List<string> serializedGuid;
                List<string> serializedDependencyHash;
                List<int[]> serializedDenpendencies;
                using (var fs = File.OpenRead(CachePath))
                {
                    var bf = new BinaryFormatter();
                    if (EditorUtility.DisplayCancelableProgressBar("Import Cache", "Reading Cache", 0))
                    {
                        EditorUtility.ClearProgressBar();
                        return false;
                    }

                    serializedGuid = (List<string>)bf.Deserialize(fs);
                    serializedDependencyHash = (List<string>)bf.Deserialize(fs);
                    serializedDenpendencies = (List<int[]>)bf.Deserialize(fs);
                    EditorUtility.ClearProgressBar();
                }

                for (var i = 0; i < serializedGuid.Count; ++i)
                {
                    var path = AssetDatabase.GUIDToAssetPath(serializedGuid[i]);
                    if (!path.IsNullOrEmpty())
                    {
                        var ad = new AssetDescription
                        {
                            name = Path.GetFileNameWithoutExtension(path),
                            path = path,
                            assetDependencyHashString = serializedDependencyHash[i]
                        };
                        assetDict.Add(serializedGuid[i], ad);
                    }
                }

                for (var i = 0; i < serializedGuid.Count; ++i)
                {
                    var guid = serializedGuid[i];
                    if (assetDict.ContainsKey(guid))
                    {
                        var guids = new List<string>();
                        foreach (var index in serializedDenpendencies[i])
                        {
                            var g = serializedGuid[index];
                            if (assetDict.ContainsKey(g))
                                guids.Add(g);
                        }

                        assetDict[guid].dependencies = guids;
                    }
                }

                UpdateResourceReferenceInfo();
                return true;
            }

            return false;
        }

        private void WriteToChache()
        {
            if (File.Exists(CachePath)) File.Delete(CachePath);
            var serializedGuid = new List<string>();
            var serializedDependencyHash = new List<string>();
            var serializedDenpendencies = new List<int[]>();
            var guidIndex = new Dictionary<string, int>();
            using var fs = File.OpenWrite(CachePath);
            foreach (var pair in assetDict)
            {
                guidIndex.Add(pair.Key, guidIndex.Count);
                serializedGuid.Add(pair.Key);
                serializedDependencyHash.Add(pair.Value.assetDependencyHashString);
            }

            foreach (var guid in serializedGuid)
            {
                var res = new List<int>();
                foreach (var i in assetDict[guid].dependencies)
                    if (guidIndex.ContainsKey(i))
                        res.Add(guidIndex[i]);
                var indexes = res.ToArray();
                serializedDenpendencies.Add(indexes);
            }

            var bf = new BinaryFormatter();
            bf.Serialize(fs, serializedGuid);
            bf.Serialize(fs, serializedDependencyHash);
            bf.Serialize(fs, serializedDenpendencies);
        }

        public void UpdateAssetState(string guid)
        {
            if (assetDict.TryGetValue(guid, out var ad) && ad.state != AssetState.Invalid)
            {
                if (File.Exists(ad.path))
                    ad.state = ad.assetDependencyHashString != File.GetLastWriteTime(ad.path).ToString(CultureInfo.InvariantCulture) ? AssetState.Changed : AssetState.Normal;
                else
                    ad.state = AssetState.Missing;
            }
            else if (!assetDict.TryGetValue(guid, out ad))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                ad = new AssetDescription
                {
                    name = Path.GetFileNameWithoutExtension(path),
                    path = path,
                    state = AssetState.Invalid
                };
                assetDict.Add(guid, ad);
            }
        }

        public static string GetInfoByState(AssetState state)
        {
            if (state == AssetState.Changed)
                return "<color=red>Mismatch with cache</color>";
            if (state == AssetState.Missing)
                return "<color=red>Lose</color>";
            if (state == AssetState.Invalid) return "<color=yellow>No cache</color>";
            return "<color=green>Cache is normal</color>";
        }

        private int GetRefCount(string assetGUID, AssetDescription desc, List<string> guidStack)
        {
            if (guidStack.Contains(assetGUID))
            {
                Debug.Log("There is a circular reference,Counting may not be accurate");
                return 0;
            }

            guidStack.Add(assetGUID);
            var total = 0;
            if (assetDict.TryGetValue(assetGUID, out var value))
            {
                if (value.references.Count > 0)
                {
                    var cachedRefCount = new Dictionary<string, int>();
                    foreach (var refs in value.references)
                        if (!cachedRefCount.ContainsKey(refs))
                        {
                            var refCount = GetRefCount(refs, value, guidStack);
                            cachedRefCount[refs] = refCount;
                            total += refCount;
                        }
                }
                else
                {
                    total = 0;
                    if (desc != null)
                    {
                        var guid = AssetDatabase.AssetPathToGUID(desc.path);
                        foreach (var deps in value.dependencies)
                            if (guid == deps)
                                total++;
                    }
                }
            }

            guidStack.RemoveAt(guidStack.Count - 1);
            return total;
        }

        public void ClearCache() => _dictCache.Clear();

        public string GetRefCount(AssetDescription desc, AssetDescription parentDesc)
        {
            if (_dictCache.TryGetValue((desc, parentDesc), out var total)) return total.ToString();
            var rootGUID = AssetDatabase.AssetPathToGUID(desc.path);
            var guidInStack = new List<string> { rootGUID };
            var cachedRefCount = new Dictionary<string, int>();
            foreach (var refs in desc.references)
            {
                if (!cachedRefCount.ContainsKey(refs))
                {
                    var refCount = GetRefCount(refs, desc, guidInStack);
                    cachedRefCount[refs] = refCount;
                    total += refCount;
                }
            }

            if (desc.references.Count == 0 && parentDesc != null)
            {
                var guid = AssetDatabase.AssetPathToGUID(desc.path);
                foreach (var refs in parentDesc.references)
                    if (refs == guid)
                        total++;
            }

            guidInStack.RemoveAt(guidInStack.Count - 1);
            _dictCache.Add((desc, parentDesc), total);
            return total.ToString();
        }

        internal sealed class AssetDescription
        {
            public string assetDependencyHashString;
            public List<string> dependencies = new();
            public string name = "";
            public string path = "";
            public List<string> references = new();
            public AssetState state = AssetState.Normal;
        }
    }
}