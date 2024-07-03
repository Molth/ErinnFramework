//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class SpriteTool
    {
        private static string _containerName;
        private static int _pixelsPerUnit = 100;
        private static Vector2 _scrollPosition;
        private static Vector2 _pivot = new(0.5f, 0f);
        private static readonly TextureImporterSettings Settings = new();
        private static TextureImporterCompression _compression = TextureImporterCompression.Uncompressed;
        private static FilterMode _filterMode = FilterMode.Point;

        public static string PathDataKey
        {
            get => EditorPrefs.GetString("SpriteCollectorPathData");
            private set => EditorPrefs.SetString("SpriteCollectorPathData", value);
        }

        private static void CreateButton(Action action)
        {
            GUILayout.Space(5);
            action.Invoke();
            GUILayout.Space(20);
        }

        private static string OpenPanel()
        {
            var str = EditorUtility.OpenFolderPanel("Select to includeSpriteThe folder for", PathDataKey, "");
            if (!string.IsNullOrEmpty(str))
                PathDataKey = str;
            return str;
        }

        private static void ChangeSettings(TextureImporter importer, Action<TextureImporterSettings> action)
        {
            importer.ReadTextureSettings(Settings);
            action.Invoke(Settings);
            importer.SetTextureSettings(Settings);
            importer.SaveAndReimport();
        }

        private static void SetTextureImporter(Action<TextureImporter> action)
        {
            var str = OpenPanel();
            if (string.IsNullOrEmpty(str))
                return;
            var importers = GetTextureImportersByFolderPath(str);
            if (importers == null || importers.Length == 0)
                return;
            foreach (var importer in importers)
                action.Invoke(importer);
            AssetDatabase.Refresh();
            Debug.Log("SpriteModification completed! Sprite Num : " + importers.Length);
        }

        private static TextureImporter[] GetTextureImportersByFolderPath(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return null;
            var paths = MathV.ToArray(GetSpritesPathsByFolderPath(folder));
            var length = paths.Length;
            var importers = new TextureImporter[length];
            for (var i = 0; i < length; ++i)
                importers[i] = (TextureImporter)AssetImporter.GetAtPath(paths[i]);
            return importers;
        }

        public static Sprite[] GetSpritesByFolderPath(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return null;
            var paths = MathV.ToArray(GetSpritesPathsByFolderPath(folder));
            var length = paths.Length;
            var sprites = new Sprite[length];
            for (var i = 0; i < length; ++i)
                sprites[i] = AssetDatabase.LoadAssetAtPath<Sprite>(paths[i]);
            return sprites;
        }

        private static string[] GetSpritesPathsByFolderPath(string folder)
        {
            if (string.IsNullOrEmpty(folder))
                return null;
            folder = folder.TrimEnd('/');
            var length = Environment.CurrentDirectory.Length + 1;
            folder = folder[length..];
            var spriteGuids = AssetDatabase.FindAssets("t:Sprite", new[] { folder });
            for (var i = 0; i < spriteGuids.Length; ++i)
                spriteGuids[i] = AssetDatabase.GUIDToAssetPath(spriteGuids[i]);
            return spriteGuids;
        }
    }
}