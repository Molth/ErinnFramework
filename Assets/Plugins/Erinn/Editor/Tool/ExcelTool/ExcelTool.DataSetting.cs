//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Erinn
{
    internal sealed partial class ExcelParser
    {
        private static string FolderPath => !string.IsNullOrEmpty(PathDataKey) ? PathDataKey : Environment.CurrentDirectory;

        public static string PathDataKey
        {
            get => EditorPrefs.GetString("ExcelPathData");
            private set => EditorPrefs.SetString("ExcelPathData", value);
        }

        public static bool LoadDataKey
        {
            get => EditorPrefs.GetBool("ExcelLoadData");
            set => EditorPrefs.SetBool("ExcelLoadData", value);
        }

        public static void ExcelToScripts()
        {
            var str = EditorUtility.OpenFolderPanel("Select a folder", FolderPath, "");
            if (string.IsNullOrEmpty(str))
                return;
            PathDataKey = str;
            GenerateScripts();
        }

        [DidReloadScripts]
        private static void CompileScripts()
        {
            if (!LoadDataKey)
                return;
            LoadDataKey = false;
            PathDataKey = FolderPath;
            GenerateAssets();
        }

        public static bool IsSupport(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || Path.GetFileName(filePath).Contains("~$"))
                return false;
            var lower = Path.GetExtension(filePath).ToLower();
            return (lower is ".xlsx" or ".xls" ? 0 : !(lower == ".xlsm") ? 1 : 0) == 0;
        }

        public static bool IsValidField(string type)
        {
            if (string.IsNullOrEmpty(type))
                return false;
            var type1 = type.Trim();
            return IsNormal(type1) || IsNormals(type1) || IsEnum(type1) || IsStruct(type1) || IsStructs(type);
        }

        public static bool IsValidStruct(string type) => !string.IsNullOrEmpty(type) && (IsStruct(type.Trim()) || IsStructs(type));

        private static bool IsEnum(string type)
        {
            var strArray = type.Split(':');
            return strArray.Length == 2 && strArray[1] == "enum";
        }

        private static bool IsNormal(string type) => ExcelParserSetting.Array.Contains(type);

        private static bool IsNormals(string type) => type.EndsWith("[]") && ExcelParserSetting.Array.Contains(type[..type.IndexOf('[')]);

        private static bool IsStruct(string type) => type.StartsWith("{") && type.EndsWith("}");

        private static bool IsStructs(string type) => type.StartsWith("{") && type.EndsWith("}[]");

        private static string GetDataName(string sheetName) => sheetName + "Data";

        private static string GetTableName(string sheetName) => sheetName + "DataTable";

        public static string GetDataWithNamespace(string sheetName) => "ExcelTable." + GetDataName(sheetName);

        public static string GetTableWithNamespace(string sheetName) => "ExcelTable." + GetTableName(sheetName);

        public static string GetScriptPath(string sheetName) => ExcelParserSetting.ScriptPath + "Table/" + GetTableName(sheetName) + ".cs";

        public static string GetEnumPath(string sheetName) => ExcelParserSetting.ScriptPath + "Enum/" + sheetName + ".cs";

        public static string GetStructPath(string sheetName) => ExcelParserSetting.ScriptPath + "Struct/" + sheetName + ".cs";

        public static string GetAssetsPath(string sheetName) => ExcelParserSetting.AssetPath + GetTableName(sheetName) + ".asset";
    }
}