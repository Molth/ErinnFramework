//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Erinn
{
    internal sealed class ClickColumn : MultiColumnHeader
    {
        public delegate void SortInColumn();

        public static Dictionary<int, SortInColumn> sortWithIndex = new()
        {
            { 0, SortByName },
            { 1, SortByPath }
        };

        public ClickColumn(MultiColumnHeaderState state) : base(state) => canSort = true;

        protected override void ColumnHeaderClicked(MultiColumnHeaderState.Column column, int columnIndex)
        {
            base.ColumnHeaderClicked(column, columnIndex);
            if (sortWithIndex.ContainsKey(columnIndex))
            {
                sortWithIndex[columnIndex].Invoke();
                var curWindow = EditorWindow.GetWindow<ResourceReferenceInfo>();
                curWindow.mAssetTreeView.SortExpandItem();
            }
        }

        public static void SortByName() => SortHelper.SortByName();

        public static void SortByPath() => SortHelper.SortByPath();
    }
}