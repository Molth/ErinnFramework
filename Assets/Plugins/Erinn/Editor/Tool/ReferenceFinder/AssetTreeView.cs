//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Erinn
{
    internal sealed class AssetTreeView : TreeView
    {
        private const float KIconWidth = 18f;
        private const float KRowHeights = 20f;
        private readonly GUIStyle _stateGuiStyle = new() { richText = true, alignment = TextAnchor.MiddleCenter };
        public AssetViewItem assetRoot;

        public AssetTreeView(TreeViewState state, ClickColumn multicolumnHeader) : base(state, multicolumnHeader)
        {
            rowHeight = KRowHeights;
            columnIndexForTreeFoldouts = 0;
            showAlternatingRowBackgrounds = true;
            showBorder = false;
            customFoldoutYOffset = (KRowHeights - EditorGUIUtility.singleLineHeight) * 0.5f;
            extraSpaceBeforeIconAndLabel = KIconWidth;
        }

        protected override void DoubleClickedItem(int id)
        {
            var item = (AssetViewItem)FindItem(id, rootItem);
            if (item != null)
            {
                var assetObject = AssetDatabase.LoadAssetAtPath(item.data.path, typeof(Object));
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = assetObject;
                EditorGUIUtility.PingObject(assetObject);
            }
        }

        protected override void ExpandedStateChanged() => SortExpandItem();

        public void SortExpandItem()
        {
            if (SortHelper.curSortType == SortType.None) return;
            var expandItemList = GetExpanded();
            foreach (var i in expandItemList)
            {
                var item = (AssetViewItem)FindItem(i, rootItem);
                SortHelper.SortChild(item.data);
            }

            var curWindow = EditorWindow.GetWindow<ResourceReferenceInfo>();
            curWindow.needUpdateAssetTree = true;
        }

        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth, bool isDepend)
        {
            var columns = new List<MultiColumnHeaderState.Column>
            {
                new()
                {
                    headerContent = new GUIContent("Name"),
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = false,
                    width = 200,
                    minWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = false,
                    canSort = true,
                    sortingArrowAlignment = TextAlignment.Center
                },
                new()
                {
                    headerContent = new GUIContent("Path"),
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = false,
                    width = 360,
                    minWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = false,
                    canSort = true,
                    sortingArrowAlignment = TextAlignment.Center
                },
                new()
                {
                    headerContent = new GUIContent("State"),
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = false,
                    width = 60,
                    minWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = true,
                    canSort = false
                }
            };
            if (!isDepend)
            {
                columns.Add(new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Number of references"),
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = false,
                    width = 60,
                    minWidth = 60,
                    autoResize = true,
                    allowToggleVisibility = true,
                    canSort = false
                });
            }

            var state = new MultiColumnHeaderState(columns.ToArray());
            return state;
        }

        protected override TreeViewItem BuildRoot() => assetRoot;

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = (AssetViewItem)args.item;
            for (var i = 0; i < args.GetNumVisibleColumns(); ++i)
                CellGUI(args.GetCellRect(i), item, (MyColumns)args.GetColumn(i), ref args);
        }

        private void CellGUI(Rect cellRect, AssetViewItem item, MyColumns column, ref RowGUIArgs args)
        {
            CenterRectUsingSingleLineHeight(ref cellRect);
            switch (column)
            {
                case MyColumns.Name:
                    var iconRect = cellRect;
                    iconRect.x += GetContentIndent(item);
                    iconRect.width = KIconWidth;
                    if (iconRect.x < cellRect.xMax)
                    {
                        var icon = GetIcon(item.data.path);
                        if (icon != null)
                            GUI.DrawTexture(iconRect, icon, ScaleMode.ScaleToFit);
                    }

                    args.rowRect = cellRect;
                    base.RowGUI(args);
                    break;
                case MyColumns.Path:
                    GUI.Label(cellRect, item.data.path);
                    break;
                case MyColumns.State:
                    GUI.Label(cellRect, ReferenceFinderData.GetInfoByState(item.data.state), _stateGuiStyle);
                    break;
                case MyColumns.RefCount:
                    GUI.Label(cellRect, ResourceReferenceInfo.data.GetRefCount(item.data, (item.parent as AssetViewItem)?.data), _stateGuiStyle);
                    break;
            }
        }

        private Texture2D GetIcon(string path)
        {
            var obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            if (obj != null)
            {
                var icon = AssetPreview.GetMiniThumbnail(obj);
                if (icon == null)
                    icon = AssetPreview.GetMiniTypeThumbnail(obj.GetType());
                return icon;
            }

            return null;
        }

        private enum MyColumns
        {
            Name,
            Path,
            State,
            RefCount
        }
    }
}