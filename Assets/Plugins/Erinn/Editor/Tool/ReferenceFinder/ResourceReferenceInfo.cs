//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Erinn
{
    internal sealed class ResourceReferenceInfo : EditorWindow
    {
        private const string IsDependPrefKey = "ReferenceFinderData_IsDepend";
        public static ReferenceFinderData data = new();
        private static bool _initializedData;
        [SerializeField] private TreeViewState _treeViewState;
        public bool needUpdateAssetTree;
        public bool needUpdateState = true;
        public List<string> selectedAssetGuid = new();
        private readonly HashSet<string> _brotherAssetIsAdd = new();
        private readonly HashSet<string> _parentAssetIsAdd = new();
        private readonly HashSet<string> _updatedAssetSet = new();
        private Dictionary<string, ListInfo> _artInfo = new();
        private bool _initializedGUIStyle;
        private bool _isDepend;
        private GUIStyle _toolbarButtonGUIStyle;
        private GUIStyle _toolbarGUIStyle;
        public AssetTreeView mAssetTreeView;

        private void OnEnable() => _isDepend = PlayerPrefs.GetInt(IsDependPrefKey, 0) == 1;

        private void OnGUI()
        {
            UpdateDragAssets();
            InitGUIStyleIfNeeded();
            DrawOptionBar();
            UpdateAssetTree();
            mAssetTreeView?.OnGUI(new Rect(0, _toolbarGUIStyle.fixedHeight, position.width, position.height - _toolbarGUIStyle.fixedHeight));
        }

        public static void FindRef()
        {
            InitDataIfNeeded();
            OpenWindow();
            var window = GetWindow<ResourceReferenceInfo>();
            window.UpdateSelectedAssets();
        }

        private static void OpenWindow()
        {
            var window = GetWindow<ResourceReferenceInfo>();
            window.wantsMouseMove = false;
            window.titleContent = new GUIContent("Find references");
            window.Show();
            window.Focus();
            SortHelper.Init();
        }

        private static void InitDataIfNeeded()
        {
            if (!_initializedData)
            {
                if (!data.ReadFromCache()) data.CollectDependenciesInfo();
                _initializedData = true;
            }
        }

        private void InitGUIStyleIfNeeded()
        {
            if (!_initializedGUIStyle)
            {
                _toolbarButtonGUIStyle = new GUIStyle("ToolbarButton");
                _toolbarGUIStyle = new GUIStyle("Toolbar");
                _initializedGUIStyle = true;
            }
        }

        private void UpdateSelectedAssets()
        {
            _artInfo = new Dictionary<string, ListInfo>();
            selectedAssetGuid.Clear();
            foreach (var obj in Selection.objects)
            {
                var path = AssetDatabase.GetAssetPath(obj);
                if (Directory.Exists(path))
                {
                    string[] folder = { path };
                    var guids = AssetDatabase.FindAssets(null, folder);
                    foreach (var guid in guids)
                        if (!selectedAssetGuid.Contains(guid) && !Directory.Exists(AssetDatabase.GUIDToAssetPath(guid)))
                            selectedAssetGuid.Add(guid);
                }
                else
                {
                    var guid = AssetDatabase.AssetPathToGUID(path);
                    selectedAssetGuid.Add(guid);
                }
            }

            needUpdateAssetTree = true;
        }

        private void UpdateDragAssets()
        {
            if (mouseOverWindow)
            {
                var tempObj = DragAreaGetObject.GetOjbects();
                if (tempObj != null)
                {
                    InitDataIfNeeded();
                    selectedAssetGuid.Clear();
                    foreach (var obj in tempObj)
                    {
                        var path = AssetDatabase.GetAssetPath(obj);
                        if (Directory.Exists(path))
                        {
                            string[] folder = { path };
                            var guids = AssetDatabase.FindAssets(null, folder);
                            foreach (var guid in guids)
                                if (!selectedAssetGuid.Contains(guid) && !Directory.Exists(AssetDatabase.GUIDToAssetPath(guid)))
                                    selectedAssetGuid.Add(guid);
                        }
                        else
                        {
                            var guid = AssetDatabase.AssetPathToGUID(path);
                            selectedAssetGuid.Add(guid);
                        }
                    }

                    needUpdateAssetTree = true;
                }
            }
        }

        private void UpdateAssetTree()
        {
            if (needUpdateAssetTree && selectedAssetGuid.Count != 0)
            {
                var root = SelectedAssetGuidToRootItem(selectedAssetGuid);
                if (mAssetTreeView == null)
                {
                    if (_treeViewState == null)
                        _treeViewState = new TreeViewState();
                    var headerState = AssetTreeView.CreateDefaultMultiColumnHeaderState(position.width, _isDepend);
                    var multiColumnHeader = new ClickColumn(headerState);
                    mAssetTreeView = new AssetTreeView(_treeViewState, multiColumnHeader);
                }
                else
                {
                    var headerState = AssetTreeView.CreateDefaultMultiColumnHeaderState(position.width, _isDepend);
                    var multiColumnHeader = new ClickColumn(headerState);
                    mAssetTreeView.multiColumnHeader = multiColumnHeader;
                }

                mAssetTreeView.assetRoot = root;
                mAssetTreeView.Reload();
                needUpdateAssetTree = false;
                var totalPrefab = 0;
                var totalMat = 0;
                var prefabName = "";
                var matName = "";
                var sb = new StringBuilder();
                if (_artInfo.Count > 0)
                    foreach (var kv in _artInfo)
                    {
                        if (kv.Value.type == "prefab")
                        {
                            totalPrefab += kv.Value.count;
                            prefabName += kv.Value.name + "<--->";
                        }

                        if (kv.Value.type == "mat")
                        {
                            totalMat += kv.Value.count;
                            matName += kv.Value.name + "<--->";
                        }

                        var tempInfo = $"name  <color=green>[{kv.Key}]</color>, type: <color=orange>[{kv.Value.type}]</color>, count: <color=red>[{kv.Value.count}]</color>";
                        sb.AppendLine(tempInfo);
                    }

                sb.Insert(0, $"PrefabTotal number  <color=red>[{totalPrefab}]</color>  PrefabDetails  <color=green>[{prefabName}]</color> \r\n");
                sb.Insert(0, $"MatTotal number  <color=red>[{totalMat}]</color>  MatDetails  <color=green>[{matName}]</color>  \r\n");
                Debug.Log(sb.ToString());
            }
        }

        public void DrawOptionBar()
        {
            EditorGUILayout.BeginHorizontal(_toolbarGUIStyle);
            if (GUILayout.Button("Click to update the local library", _toolbarButtonGUIStyle))
            {
                data.CollectDependenciesInfo();
                needUpdateAssetTree = true;
                GUIUtility.ExitGUI();
            }

            var preIsDepend = _isDepend;
            _isDepend = GUILayout.Toggle(_isDepend, _isDepend ? "Dependency pattern" : "Reference pattern", _toolbarButtonGUIStyle, GUILayout.Width(100));
            if (preIsDepend != _isDepend) OnModelSelect();
            if (GUILayout.Button("Launch", _toolbarButtonGUIStyle)) mAssetTreeView?.ExpandAll();
            if (GUILayout.Button("Fold", _toolbarButtonGUIStyle)) mAssetTreeView?.CollapseAll();
            EditorGUILayout.EndHorizontal();
        }

        private void OnModelSelect()
        {
            needUpdateAssetTree = true;
            PlayerPrefs.SetInt(IsDependPrefKey, _isDepend ? 1 : 0);
            UpdateAssetTree();
        }

        private AssetViewItem SelectedAssetGuidToRootItem(List<string> inputSelectedAssetGuid)
        {
            _updatedAssetSet.Clear();
            _parentAssetIsAdd.Clear();
            _brotherAssetIsAdd.Clear();
            var elementCount = 0;
            var root = new AssetViewItem { id = elementCount, depth = -1, displayName = "Root", data = null };
            const int depth = 0;
            foreach (var childGuid in inputSelectedAssetGuid)
            {
                var rs = CreateTree(childGuid, ref elementCount, depth);
                root.AddChild(rs);
            }

            _updatedAssetSet.Clear();
            return root;
        }

        private AssetViewItem CreateTree(string guid, ref int elementCount, int depth)
        {
            if (_parentAssetIsAdd.Contains(guid)) return null;
            if (needUpdateState && !_updatedAssetSet.Contains(guid))
            {
                data.UpdateAssetState(guid);
                _updatedAssetSet.Add(guid);
            }

            ++elementCount;
            var referenceData = data.assetDict[guid];
            var root = new AssetViewItem { id = elementCount, displayName = referenceData.name, data = referenceData, depth = depth };
            var childGuids = _isDepend ? referenceData.dependencies : referenceData.references;
            _parentAssetIsAdd.Add(guid);
            foreach (var childGuid in childGuids)
            {
                if (_brotherAssetIsAdd.Contains(childGuid)) continue;
                var listInfo = new ListInfo();
                if (AssetDatabase.GUIDToAssetPath(childGuid).EndsWith(".mat") && depth < 2)
                {
                    listInfo.type = "mat";
                    listInfo.count = 1;
                    listInfo.name = Path.GetFileName(AssetDatabase.GUIDToAssetPath(childGuid));
                    if (_artInfo.ContainsKey(root.displayName))
                    {
                        _artInfo[root.displayName].count += 1;
                        _artInfo[root.displayName].name += "<<==>>" + listInfo.name;
                    }
                    else
                    {
                        _artInfo.Add(root.displayName, listInfo);
                    }
                }

                if (AssetDatabase.GUIDToAssetPath(childGuid).EndsWith(".prefab") && !AssetDatabase.GUIDToAssetPath(childGuid).Contains("_gen_render") && depth < 2)
                {
                    listInfo.type = "prefab";
                    listInfo.count = 1;
                    listInfo.name = Path.GetFileName(AssetDatabase.GUIDToAssetPath(childGuid));
                    if (_artInfo.ContainsKey(root.displayName))
                    {
                        _artInfo[root.displayName].count += 1;
                        _artInfo[root.displayName].name += "<<==>>" + listInfo.name;
                    }
                    else
                    {
                        _artInfo.Add(root.displayName, listInfo);
                    }
                }

                _brotherAssetIsAdd.Add(childGuid);
                var rs = CreateTree(childGuid, ref elementCount, depth + 1);
                if (rs != null) root.AddChild(rs);
            }

            foreach (var childGuid in childGuids)
                if (_brotherAssetIsAdd.Contains(childGuid))
                    _brotherAssetIsAdd.Remove(childGuid);
            _parentAssetIsAdd.Remove(guid);
            return root;
        }
    }
}