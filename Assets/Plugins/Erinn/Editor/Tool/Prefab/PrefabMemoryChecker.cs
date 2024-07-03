//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;

namespace Erinn
{
    internal sealed class PrefabMemoryChecker
    {
        public static object GetFieldValue(object obj, string fieldName) => obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)?.GetValue(obj);

        public static object InvokeMethod(Type type, string methodName, object[] parameters = null)
        {
            if (parameters == null)
                return type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)?.Invoke(null, null);
            var types = new Type[parameters.Length];
            for (var i = 0; i < parameters.Length; i++) types[i] = parameters[i].GetType();
            return type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static, null, types, null)?.Invoke(null, parameters);
        }

        public static void BeginMemoryCheck()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null)
            {
                Debug.LogWarning("<color=#FF0000>Please open a prefab!</color>");
                return;
            }

            long totalMemory = 0;
            var dependencies = EditorUtility.CollectDependencies(new Object[] { prefabStage.prefabContentsRoot });
            foreach (var dependency in dependencies)
            {
                var path = AssetDatabase.GetAssetPath(dependency);
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                    continue;
                if (dependency is Sprite sprite)
                {
                    var textureUtilType = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.TextureUtil");
                    var size = (long)InvokeMethod(textureUtilType, "GetStorageMemorySizeLong", new object[] { sprite.texture });
                    Debug.Log("<color=#00FFFF>" + sprite + "</color>:<color=#FFAA00> " + EditorUtility.FormatBytes(size) + "</color>");
                    totalMemory += size;
                }
                else if (dependency is AnimationClip clip)
                {
                    var stats = InvokeMethod(typeof(AnimationUtility), "GetAnimationClipStats", new object[] { clip });
                    var size = (int)GetFieldValue(stats, "size");
                    Debug.Log("<color=#00FFFF>" + clip + "</color>:<color=#FFAA00> " + EditorUtility.FormatBytes(size) + "</color>");
                    totalMemory += size;
                }
            }

            var objSize = Profiler.GetRuntimeMemorySizeLong(prefabStage.prefabContentsRoot);
            Debug.Log("<color=#00FFFF>" + prefabStage.prefabContentsRoot + "</color>:<color=#FFAA00> " + EditorUtility.FormatBytes(objSize) + "</color>");
            totalMemory += objSize;
            Debug.Log("<color=#00FFFF>Memory usage</color>:<color=#FFAA00> " + EditorUtility.FormatBytes(totalMemory) + "</color>");
        }
    }
}