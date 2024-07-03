//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Erinn
{
    internal sealed class SpriteAnimTool : EditorWindow
    {
        private static AnimatorController _animatorController;

        public static string PathDataKey
        {
            get => EditorPrefs.GetString("SpriteAnimToolPath");
            private set => EditorPrefs.SetString("SpriteAnimToolPath", value);
        }

        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("DischargeAnimatorController");
            GUILayout.Space(5);
            _animatorController = EditorGUILayout.ObjectField(_animatorController, typeof(AnimatorController), false) as AnimatorController;
            GUILayout.Space(5);
            if (GUILayout.Button("Select folder level three"))
                CollectAnimation();
            GUILayout.Space(5);
            GUILayout.Label("A total of three levels of folders\r\nFor exampleEnemies/Various monsters/Various actions\r\nChoiceEnemiesFolder");
            GUILayout.Space(5);
            if (GUILayout.Button("Select folder level 2"))
                CollectAnimationTwo();
            GUILayout.Space(5);
            GUILayout.Label("Total secondary folders\r\nFor example, a certain monster/Various actions\r\nSelect a monster folder");
            GUILayout.Space(5);
            CreateFirst();
            GUILayout.Space(5);
        }

        public static void OpenSpriteAnimTool() => GetWindow(typeof(SpriteAnimTool)).titleContent = new GUIContent("SpriteAnimation tools");

        private static string OpenPanel()
        {
            var str = EditorUtility.OpenFolderPanel("Select to includeSpriteThe folder for", PathDataKey, "");
            if (!string.IsNullOrEmpty(str))
                PathDataKey = str;
            return str;
        }

        public static void CollectAnimation()
        {
            var str = OpenPanel();
            if (string.IsNullOrEmpty(str))
                return;
            var directories = Directory.GetDirectories(str);
            var length = directories.Length;
            if (length == 0)
                return;
            if (!Directory.Exists("Assets/Anim"))
                Directory.CreateDirectory("Assets/Anim");
            foreach (var folder in directories)
            {
                var newName = Path.GetFileNameWithoutExtension(folder);
                var childFolder = "Assets/Anim/" + newName + "/";
                if (!Directory.Exists(childFolder))
                    Directory.CreateDirectory(childFolder);
                var animPath = Path.Combine(childFolder, newName) + ".asset";
                var animatorOverriderController = new AnimatorOverrideController
                {
                    name = newName
                };
                if (_animatorController != null)
                    animatorOverriderController.runtimeAnimatorController = _animatorController;
                if (File.Exists(animPath))
                    AssetDatabase.DeleteAsset(animPath);
                AssetDatabase.CreateAsset(animatorOverriderController, animPath);
                var childDirectories = Directory.GetDirectories(folder);
                foreach (var child in childDirectories)
                    CreateAnimation(childFolder, child);
            }
        }

        public static void CollectAnimationTwo()
        {
            var folder = OpenPanel();
            if (string.IsNullOrEmpty(folder))
                return;
            var directories = Directory.GetDirectories(folder);
            var length = directories.Length;
            if (length == 0)
                return;
            if (!Directory.Exists("Assets/Anim"))
                Directory.CreateDirectory("Assets/Anim");
            var newName = Path.GetFileNameWithoutExtension(folder);
            var childFolder = "Assets/Anim/" + newName + "/";
            if (!Directory.Exists(childFolder))
                Directory.CreateDirectory(childFolder);
            var animPath = Path.Combine(childFolder, newName) + ".asset";
            var animatorOverriderController = new AnimatorOverrideController
            {
                name = newName
            };
            if (_animatorController != null)
                animatorOverriderController.runtimeAnimatorController = _animatorController;
            if (File.Exists(animPath))
                AssetDatabase.DeleteAsset(animPath);
            AssetDatabase.CreateAsset(animatorOverriderController, animPath);
            var childDirectories = Directory.GetDirectories(folder);
            foreach (var child in childDirectories)
                CreateAnimation(childFolder, child);
        }

        public static void CreateFirst()
        {
            if (GUILayout.Button("Select folder level one"))
            {
                var folder = OpenPanel();
                if (string.IsNullOrEmpty(folder))
                    return;
                CreateAnimation("Assets/", folder);
            }

            GUILayout.Space(5);
            GUILayout.Label("A total of one level folders\r\nFor example, a certain action\r\nSelect an action folder");
        }

        public static void CreateAnimation(string newName, string str)
        {
            var savePath = newName + Path.GetFileNameWithoutExtension(str) + ".asset";
            var sprites = SpriteTool.GetSpritesByFolderPath(str);
            var animationClip = new AnimationClip
            {
                frameRate = 12
            };
            var spriteBinding = new EditorCurveBinding
            {
                type = typeof(SpriteRenderer),
                path = "",
                propertyName = "m_Sprite"
            };
            var keyframes = new ObjectReferenceKeyframe[sprites.Length];
            for (var i = 0; i < sprites.Length; ++i)
                keyframes[i] = new ObjectReferenceKeyframe
                {
                    time = i / animationClip.frameRate,
                    value = sprites[i]
                };
            AnimationUtility.SetObjectReferenceCurve(animationClip, spriteBinding, keyframes);
            if (File.Exists(savePath))
                AssetDatabase.DeleteAsset(savePath);
            AssetDatabase.CreateAsset(animationClip, savePath);
            AssetDatabase.Refresh();
        }
    }
}