//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    public sealed class HierarchySortingTool : EditorWindow
    {
        private readonly Color _primaryButtonColor = new(1.000f, 0.820f, 0.550f);
        private readonly Color _secondaryButtonColor = new(1.000f, 0.946f, 0.865f);
        private Vector2 _scrollPosition;
        private SortMode _sortMode;
        private SortOrder _sortOrder;
        private SortTarget _sortTarget;

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            EditorGUILayout.Space(5f);
            _sortTarget = (SortTarget)EditorGUILayout.EnumPopup("Sort Target", _sortTarget);
            EditorGUILayout.HelpBox(new GUIContent("Selected-Sort Selected Objects\r\nChildren-Sort the sub objects of the selected object"));
            EditorGUILayout.Space(10f);
            _sortMode = (SortMode)EditorGUILayout.EnumPopup("Sorting type", _sortMode);
            EditorGUILayout.HelpBox(new GUIContent("By name-Sort alphabetically\r\nPosition-Base on X, Y Or Z Coordinate sorting\r\nReversal-Reverse current order"));
            EditorGUILayout.Space(20f);
            if (_sortMode is SortMode.ByPositionX or SortMode.ByPositionY or SortMode.ByPositionZ)
            {
                _sortOrder = (SortOrder)EditorGUILayout.EnumPopup("Sorting order", _sortOrder);
                var axisBeingSorted = _sortMode switch
                {
                    SortMode.ByPositionX => "x",
                    SortMode.ByPositionY => "y",
                    _ => "z"
                };
                EditorGUILayout.LabelField(_sortOrder == SortOrder.Descending ? $"Objects with larger positions {axisBeingSorted} Will be located at the front" : $"Objects with smaller positions {axisBeingSorted} Will be located at the front", EditorStyles.miniLabel);
            }
            else if (_sortMode == SortMode.ByName)
            {
                _sortOrder = (SortOrder)EditorGUILayout.EnumPopup("Character order", _sortOrder);
                EditorGUILayout.LabelField(_sortOrder == SortOrder.Ascending ? "ABCD..." : "ZYXW...", EditorStyles.miniLabel);
            }

            EditorGUILayout.Space(20f);
            GUI.backgroundColor = _primaryButtonColor;
            if (GUILayout.Button("Sort", GUILayout.Height(40f)) && HasSelection())
            {
                if (_sortTarget == SortTarget.Children)
                    SortSelectedChildObjects();
                else
                    SortSelected();
            }

            EditorGUILayout.Space(10f);
            GUI.backgroundColor = _secondaryButtonColor;
            EditorGUILayout.EndScrollView();
        }

        public static void ToggleWindow() => GetWindow<HierarchySortingTool>("Object Sorter");

        private void SortSelected()
        {
            var selectedTransforms = new Transform[Selection.gameObjects.Length];
            Transform parent = null;
            for (var i = 0; i < Selection.gameObjects.Length; ++i)
            {
                selectedTransforms[i] = Selection.gameObjects[i].transform;
                if (i == 0)
                {
                    parent = selectedTransforms[i].parent;
                }
                else if (selectedTransforms[i].parent != parent)
                {
                    Debug.LogWarning("Unable to sort objects that do not have the same parent object");
                    return;
                }
            }

            SortObjects(selectedTransforms);
        }

        private void SortSelectedChildObjects()
        {
            for (var i = 0; i < Selection.gameObjects.Length; ++i)
            {
                var childObjects = Selection.gameObjects[i].GetComponentsInChildren<Transform>().RemoveAt(0);
                SortObjects(childObjects);
            }
        }

        private SortedDictionary<int, Transform> GetTransformIndexDictionary(Transform[] objects)
        {
            var siblingIndexes = new SortedDictionary<int, Transform>();
            for (var i = 0; i < objects.Length; ++i)
                siblingIndexes.Add(objects[i].GetSiblingIndex(), objects[i]);
            return siblingIndexes;
        }

        private void SortObjects(Transform[] objects)
        {
            if (objects.Length <= 0) return;
            var transformIndexPairs = GetTransformIndexDictionary(objects);
            var index = 0;
            foreach (var entry in transformIndexPairs)
            {
                objects[index] = entry.Value;
                index++;
            }

            objects = _sortMode switch
            {
                SortMode.ByName => SortObjectsByName(objects),
                SortMode.ByPositionX => SortObjectsByPositionX(objects),
                SortMode.ByPositionY => SortObjectsByPositionY(objects),
                SortMode.ByPositionZ => SortObjectsByPositionZ(objects),
                SortMode.Reverse => SortObjectsReverse(objects),
                _ => objects
            };
            SetUpTransformOrder(objects, transformIndexPairs);
        }

        private Transform[] SortObjectsByPositionX(Transform[] objects)
        {
            if (_sortOrder == SortOrder.Ascending)
                QuickSort(objects, 0, objects.Length - 1, ComparePositionXAscending);
            else
                QuickSort(objects, 0, objects.Length - 1, ComparePositionXDescending);
            return objects;
        }

        private Transform[] SortObjectsByPositionY(Transform[] objects)
        {
            if (_sortOrder == SortOrder.Ascending)
                QuickSort(objects, 0, objects.Length - 1, ComparePositionYAscending);
            else
                QuickSort(objects, 0, objects.Length - 1, ComparePositionYDescending);
            return objects;
        }

        private Transform[] SortObjectsByPositionZ(Transform[] objects)
        {
            if (_sortOrder == SortOrder.Ascending)
                QuickSort(objects, 0, objects.Length - 1, ComparePositionZAscending);
            else
                QuickSort(objects, 0, objects.Length - 1, ComparePositionZDescending);
            return objects;
        }

        private Transform[] SortObjectsByName(Transform[] objects)
        {
            if (_sortOrder == SortOrder.Ascending)
                QuickSort(objects, 0, objects.Length - 1, CompareNameAscending);
            else
                QuickSort(objects, 0, objects.Length - 1, CompareNameDescending);
            return objects;
        }

        private int ComparePositionXAscending(Transform a, Transform b) => a.position.x.CompareTo(b.position.x);

        private int ComparePositionXDescending(Transform a, Transform b) => b.position.x.CompareTo(a.position.x);

        private int ComparePositionYAscending(Transform a, Transform b) => a.position.y.CompareTo(b.position.y);

        private int ComparePositionYDescending(Transform a, Transform b) => b.position.y.CompareTo(a.position.y);

        private int ComparePositionZAscending(Transform a, Transform b) => a.position.z.CompareTo(b.position.z);

        private int ComparePositionZDescending(Transform a, Transform b) => b.position.z.CompareTo(a.position.z);

        private int CompareNameAscending(Transform a, Transform b) => string.Compare(a.name, b.name, StringComparison.Ordinal);

        private int CompareNameDescending(Transform a, Transform b) => string.Compare(b.name, a.name, StringComparison.Ordinal);

        private void QuickSort(Transform[] objects, int left, int right, Func<Transform, Transform, int> compareFunc)
        {
            while (true)
            {
                if (left < right)
                {
                    var pivotIndex = Partition(objects, left, right, compareFunc);
                    QuickSort(objects, left, pivotIndex - 1, compareFunc);
                    left = pivotIndex + 1;
                    continue;
                }

                break;
            }
        }

        private int Partition(Transform[] objects, int left, int right, Func<Transform, Transform, int> compareFunc)
        {
            var pivotValue = objects[right];
            var i = left - 1;
            for (var j = left; j < right; ++j)
            {
                if (compareFunc(objects[j], pivotValue) <= 0)
                {
                    i++;
                    Swap(objects, i, j);
                }
            }

            Swap(objects, i + 1, right);
            return i + 1;
        }

        private void Swap(Transform[] objects, int i, int j) => (objects[i], objects[j]) = (objects[j], objects[i]);

        private Transform[] SortObjectsReverse(Transform[] objects) => MathV.Reverse(objects);

        private void SetUpTransformOrder(Transform[] arrayAfter, SortedDictionary<int, Transform> originalList)
        {
            var newList = new SortedDictionary<int, Transform>();
            var parent = arrayAfter[0].parent;
            if (parent == null)
            {
                Debug.LogWarning("Unable to sort objects at the top level of the hierarchy, Please ensure that only objects with parents are selected!");
                return;
            }

            var index = 0;
            foreach (var entry in originalList)
            {
                newList.Add(entry.Key, arrayAfter[index]);
                index++;
            }

            foreach (Transform child in parent)
                if (!newList.ContainsValue(child))
                    newList.Add(child.GetSiblingIndex(), child);
            foreach (var entry in newList)
            {
                Undo.RegisterCompleteObjectUndo(entry.Value.root, "ChangeSibling");
                entry.Value.SetSiblingIndex(entry.Key);
            }

            Undo.CollapseUndoOperations(Undo.GetCurrentGroup());
        }

        private bool HasSelection() => Selection.activeGameObject != null;

        private enum SortMode
        {
            ByName,
            ByPositionX,
            ByPositionY,
            ByPositionZ,
            Reverse
        }

        private enum SortOrder
        {
            Ascending,
            Descending
        }

        private enum SortTarget
        {
            Selected,
            Children
        }
    }
}