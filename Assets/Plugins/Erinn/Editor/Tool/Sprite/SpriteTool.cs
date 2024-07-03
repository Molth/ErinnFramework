//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class SpriteTool : EditorWindow
    {
        private void OnGUI()
        {
            GUILayout.Space(5);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            CreateButton(() =>
            {
                GUILayout.Label("Filling containers: Enter container name");
                _containerName = EditorGUILayout.TextField(_containerName);
                GUILayout.Space(5);
                if (GUILayout.Button("Select folder"))
                    CollectSprites();
            });
            CreateButton(() =>
            {
                GUILayout.Label("Set a property: Pixels per unit");
                _pixelsPerUnit = EditorGUILayout.IntField(_pixelsPerUnit);
                GUILayout.Space(5);
                if (GUILayout.Button("Select folder"))
                    SetTextureImporter(newSetting => SetPixelPerUnit(newSetting, _pixelsPerUnit));
            });
            CreateButton(() =>
            {
                _pivot = EditorGUILayout.Vector2Field("Set a property: Axis", _pivot);
                GUILayout.Space(5);
                if (GUILayout.Button("Select folder"))
                    SetTextureImporter(newSetting => SetPivot(newSetting, _pivot));
            });
            CreateButton(() =>
            {
                _filterMode = (FilterMode)EditorGUILayout.EnumPopup("Set a property: Filtering mode", _filterMode);
                GUILayout.Space(5);
                if (GUILayout.Button("Select folder"))
                    SetTextureImporter(newSetting => SetFilterMode(newSetting, _filterMode));
            });
            CreateButton(() =>
            {
                _compression = (TextureImporterCompression)EditorGUILayout.EnumPopup("Set a property: Compression mode", _compression);
                GUILayout.Space(5);
                if (GUILayout.Button("Select folder"))
                    SetTextureImporter(newSetting => SetCompression(newSetting, _compression));
            });
            CreateButton(() =>
            {
                GUILayout.Label("Set all properties");
                GUILayout.Space(5);
                if (GUILayout.Button("Select folder"))
                    SetTextureImporter(SetAll);
            });
            GUILayout.Space(5);
            GUILayout.EndScrollView();
        }

        public static void OpenSpriteTool() => GetWindow(typeof(SpriteTool)).titleContent = new GUIContent("SpriteTool");

        private static void CollectSprites()
        {
            var str = OpenPanel();
            if (string.IsNullOrEmpty(str))
                return;
            var sprites = GetSpritesByFolderPath(str);
            if (sprites == null || sprites.Length == 0)
                return;
            var name = !string.IsNullOrEmpty(_containerName) ? _containerName : str;
            var savePath = "Assets/" + Path.GetFileNameWithoutExtension(name) + ".asset";
            if (File.Exists(savePath))
                AssetDatabase.DeleteAsset(savePath);
            var spriteContainer = CreateInstance<SpriteContainer>();
            spriteContainer.Elements = sprites;
            AssetDatabase.CreateAsset(spriteContainer, savePath);
            AssetDatabase.Refresh();
            Debug.Log("SpriteContainer generation completed! Sprite Num : " + sprites.Length);
        }

        private static void SetAll(TextureImporter importer)
        {
            importer.textureCompression = _compression;
            ChangeSettings(importer, newSetting =>
            {
                newSetting.spritePixelsPerUnit = _pixelsPerUnit;
                newSetting.spriteAlignment = (int)SpriteAlignment.Custom;
                newSetting.spritePivot = _pivot;
                newSetting.filterMode = _filterMode;
            });
        }

        private static void SetPixelPerUnit(TextureImporter importer, int pixelsPerUnit) => ChangeSettings(importer, newSetting => newSetting.spritePixelsPerUnit = pixelsPerUnit);

        private static void SetPivot(TextureImporter importer, Vector2 spritePivot) => ChangeSettings(importer, newSetting =>
        {
            newSetting.spriteAlignment = (int)SpriteAlignment.Custom;
            newSetting.spritePivot = spritePivot;
        });

        private static void SetFilterMode(TextureImporter importer, FilterMode mode) => ChangeSettings(importer, newSetting => newSetting.filterMode = mode);

        private static void SetCompression(TextureImporter importer, TextureImporterCompression mode) => importer.textureCompression = mode;
    }
}