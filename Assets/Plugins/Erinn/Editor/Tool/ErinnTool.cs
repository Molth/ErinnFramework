//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEditor;

namespace Erinn
{
    internal sealed class ErinnTool
    {
        [MenuItem("Tools/Develop/Find references", false, 0)]
        public static void FindRef() => ResourceReferenceInfo.FindRef();

        [MenuItem("Tools/Develop/Open a folder", false, 1)]
        private static void OpenFolderSelect() => OpenFolderTool.OpenFolderSelect();

        [MenuItem("Tools/Develop/Open a scene", false, 2)]
        private static void ShowSceneWindow() => SceneSwitcherWindow.ShowWindow();

        [MenuItem("Tools/Develop/Excel tool", false, 3)]
        private static void OpenExcelTool() => ExcelTool.OpenExcelTool();

        [MenuItem("Tools/Develop/Object tool", false, 4)]
        private static void ShowRenamerWindow() => GameObjectTool.OpenGameObjectTool();

        [MenuItem("Tools/Develop/Prefab tool", false, 5)]
        private static void OpenPrefabTool() => PrefabTool.OpenPrefabTool();

        [MenuItem("Tools/Develop/Addressables Tool", false, 6)]
        private static void OpenAddressableTool() => AddressableEditorTool.OpenAddressableTool();

        [MenuItem("Tools/Develop/Code generator", false, 7)]
        private static void OpenGenerateNormalTool() => GeneratorTool.OpenGenerateNormalTool();

        [MenuItem("Tools/Develop/Sprite tool", false, 8)]
        private static void OpenSpriteTool() => SpriteTool.OpenSpriteTool();

        [MenuItem("Tools/Develop/Generate animation", false, 9)]
        private static void CreateAnim() => SpriteAnimTool.OpenSpriteAnimTool();

        [MenuItem("Tools/Develop/Generate so", false, 10)]
        private static void CreateSo() => ScriptableObjectTool.CreateSo();
    }
}