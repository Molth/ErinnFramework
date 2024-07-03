//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

namespace Erinn
{
    internal readonly struct ExcelParserSetting
    {
        public const string AssetPath = "Assets/AddressableResources/ExcelTable/";

        public const string ScriptPath = "Assets/Scripts/Data/ExcelTable/";

        public static readonly string[] UnityEngineArray =
        {
            "Vector2",
            "Vector3",
            "Vector4",
            "Vector2Int",
            "Vector3Int",
            "Color",
            "Quaternion"
        };

        public static readonly string[] Array =
        {
            "string",
            "bool",
            "byte",
            "int",
            "long",
            "float",
            "double",
            "Vector2",
            "Vector3",
            "Vector4",
            "Vector2Int",
            "Vector3Int",
            "Color",
            "Quaternion"
        };
    }
}