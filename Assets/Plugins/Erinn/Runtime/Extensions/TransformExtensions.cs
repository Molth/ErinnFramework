//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static void Flip(this Transform t) => t.localScale = t.localScale.Flip();

        public static void FlipXY(this Transform t) => t.localScale = t.localScale.FlipXY();

        public static bool Flip(this Transform t, bool isRight)
        {
            var modelX = t.localScale.x;
            if ((!isRight || !(modelX < 0f)) && (isRight || !(modelX > 0f)))
                return false;
            t.localScale = t.localScale.Flip();
            return true;
        }

        public static bool Flip(this Transform t, float direction)
        {
            var modelX = t.localScale.x;
            if ((!(direction > 0f) || !(modelX < 0f)) && (!(direction < 0f) || !(modelX > 0f)))
                return false;
            t.localScale = t.localScale.Flip();
            return true;
        }

        public static bool FlipXY(this Transform t, bool isRight)
        {
            var modelX = t.localScale.x;
            if ((!isRight || !(modelX < 0f)) && (isRight || !(modelX > 0f)))
                return false;
            t.localScale = t.localScale.FlipXY();
            return true;
        }

        public static bool FlipXY(this Transform t, float direction)
        {
            var modelX = t.localScale.x;
            if ((!(direction > 0f) || !(modelX < 0f)) && (!(direction < 0f) || !(modelX > 0f)))
                return false;
            t.localScale = t.localScale.FlipXY();
            return true;
        }

        public static void ClampPositionX(this Transform t, float min, float max)
        {
            var position = t.position;
            position.x = MathV.Clamp(position.x, min, max);
            t.position = position;
        }

        public static void ClampPositionY(this Transform t, float min, float max)
        {
            var position = t.position;
            position.y = MathV.Clamp(position.y, min, max);
            t.position = position;
        }

        public static void ClampPositionZ(this Transform t, float min, float max)
        {
            var position = t.position;
            position.z = MathV.Clamp(position.z, min, max);
            t.position = position;
        }

        public static void ClampLocalPositionX(this Transform t, float min, float max)
        {
            var position = t.localPosition;
            position.x = MathV.Clamp(position.x, min, max);
            t.localPosition = position;
        }

        public static void ClampLocalPositionY(this Transform t, float min, float max)
        {
            var position = t.localPosition;
            position.y = MathV.Clamp(position.y, min, max);
            t.localPosition = position;
        }

        public static void ClampLocalPositionZ(this Transform t, float min, float max)
        {
            var position = t.localPosition;
            position.z = MathV.Clamp(position.z, min, max);
            t.localPosition = position;
        }

        public static Transform FindRoot(this Transform t)
        {
            var root = t;
            while (root.parent != null)
                root = root.parent;
            return root;
        }

        public static void SortChildren(this Transform t, float start, float delta)
        {
            var length = t.childCount;
            for (var i = 0; i < length; ++i)
            {
                var child = t.GetChild(i);
                var position = child.position;
                position.x = start;
                child.position = position;
                start += delta;
            }
        }

        public static void SortChildren(this Transform t, Vector3 start, Vector3 delta)
        {
            var length = t.childCount;
            for (var i = 0; i < length; ++i)
            {
                t.GetChild(i).position = start;
                start += delta;
            }
        }

        public static string GetChildPath(this Transform t, string name)
        {
            foreach (var trans in t.GetComponentsInChildren<Transform>(true))
            {
                if (trans.name == name)
                {
                    var pathList = new List<string> { trans.name };
                    var child = trans;
                    while (child.parent != null && child.parent != t)
                    {
                        child = child.parent;
                        pathList.Insert(0, child.name);
                    }

                    var path = pathList[0];
                    for (var i = 1; i < pathList.Count; ++i)
                        path += "/" + pathList[i];
                    return path;
                }
            }

            return null;
        }

        public static List<string> GetChildrenPath(this Transform t, string name)
        {
            var pathLists = new List<string>();
            foreach (var trans in t.GetComponentsInChildren<Transform>(true))
            {
                if (trans.name == name)
                {
                    var pathList = new List<string> { trans.name };
                    var child = trans;
                    while (child.parent != null && child.parent != t)
                    {
                        child = child.parent;
                        pathList.Insert(0, child.name);
                    }

                    var path = pathList[0];
                    for (var i = 1; i < pathList.Count; ++i)
                        path += "/" + pathList[i];
                    pathLists.Add(path);
                }
            }

            return pathLists;
        }

        public static string GetChildPath<T>(this Transform t) where T : Component
        {
            foreach (var trans in t.GetComponentsInChildren<T>(true))
            {
                var pathList = new List<string> { trans.name };
                var child = trans.transform;
                while (child.parent != null && child.parent != t)
                {
                    child = child.parent;
                    pathList.Insert(0, child.name);
                }

                var path = pathList[0];
                for (var i = 1; i < pathList.Count; ++i)
                    path += "/" + pathList[i];
                return path;
            }

            return null;
        }

        public static List<string> GetChildrenPath<T>(this Transform t) where T : Component
        {
            var pathLists = new List<string>();
            foreach (var trans in t.GetComponentsInChildren<T>(true))
            {
                var pathList = new List<string> { trans.name };
                var child = trans.transform;
                while (child.parent != null && child.parent != t)
                {
                    child = child.parent;
                    pathList.Insert(0, child.name);
                }

                var path = pathList[0];
                for (var i = 1; i < pathList.Count; ++i)
                    path += "/" + pathList[i];
                pathLists.Add(path);
            }

            return pathLists;
        }

        public static string GetChildPath<T>(this Transform t, string name) where T : Component
        {
            foreach (var trans in t.GetComponentsInChildren<T>(true))
            {
                if (trans.name == name)
                {
                    var pathList = new List<string> { trans.name };
                    var child = trans.transform;
                    while (child.parent != null && child.parent != t)
                    {
                        child = child.parent;
                        pathList.Insert(0, child.name);
                    }

                    var path = pathList[0];
                    for (var i = 1; i < pathList.Count; ++i)
                        path += "/" + pathList[i];
                    return path;
                }
            }

            return null;
        }

        public static List<string> GetChildrenPath<T>(this Transform t, string name) where T : Component
        {
            var pathLists = new List<string>();
            foreach (var trans in t.GetComponentsInChildren<T>(true))
            {
                if (trans.name == name)
                {
                    var pathList = new List<string> { trans.name };
                    var child = trans.transform;
                    while (child.parent != null && child.parent != t)
                    {
                        child = child.parent;
                        pathList.Insert(0, child.name);
                    }

                    var path = pathList[0];
                    for (var i = 1; i < pathList.Count; ++i)
                        path += "/" + pathList[i];
                    pathLists.Add(path);
                }
            }

            return pathLists;
        }

        public static Transform[] GetChildren(this Transform t)
        {
            var length = t.childCount;
            var children = new Transform[length];
            for (var i = 0; i < length; ++i)
                children[i] = t.GetChild(i);
            return children;
        }

        public static void AddChild(this Transform t, Transform child) => child.SetParent(t, false);

        public static void MoveSibling(this Transform child, int index)
        {
            if (child == null || child.parent == null)
                return;
            var currentIndex = child.GetSiblingIndex();
            var newIndex = MathV.Clamp(currentIndex + index, 0, child.parent.childCount - 1);
            if (currentIndex == newIndex)
                return;
            child.SetSiblingIndex(newIndex);
        }

        public static void MoveUpSibling(this Transform child)
        {
            if (child == null || child.parent == null)
                return;
            var currentIndex = child.GetSiblingIndex();
            var newIndex = MathV.ClampMin(currentIndex - 1, 0);
            if (currentIndex == newIndex)
                return;
            child.SetSiblingIndex(newIndex);
        }

        public static void MoveDownSibling(this Transform child)
        {
            if (child == null || child.parent == null)
                return;
            var currentIndex = child.GetSiblingIndex();
            var newIndex = MathV.ClampMax(currentIndex + 1, child.parent.childCount - 1);
            if (currentIndex == newIndex)
                return;
            child.SetSiblingIndex(newIndex);
        }
    }
}