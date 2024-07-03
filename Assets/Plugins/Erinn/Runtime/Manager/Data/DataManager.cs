//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    internal sealed partial class DataManager : ModuleSingleton, IDataManager
    {
        /// <summary>
        ///     InitializationExcelData Table Data
        /// </summary>
        public static void ExcelTableInit()
        {
            var (assembly, types) = GetAssemblyAndTypes();
            if (types == null || types.Length == 0)
                return;
            foreach (var type in types)
            {
                var keyName = type.Name;
                try
                {
                    var dataType = GetDataType(assembly, type);
                    var keyInfo = GetKeyField(dataType);
                    if (keyInfo == null)
                    {
                        Log.Warning($"The data table is missing a primary key => {keyName}");
                        return;
                    }

                    var tablePath = "ExcelTable/" + keyName;
                    var obj = ResourceManager.Load<ScriptableObject>(tablePath);
                    if (obj == null)
                    {
                        Log.Error($"<color=#FFAA00>Load Data Table =></color> <color=#00FFFF>{keyName}</color> <color=#FF0000>Fail</color>");
                        return;
                    }

                    var table = (IExcelTableBase)obj;
                    var keyType = keyInfo.FieldType;
                    if (keyType == typeof(int))
                        IntDataDict.Add(dataType, keyInfo, table);
                    else if (keyType == typeof(string))
                        StrDataDict.Add(dataType, keyInfo, table);
                    else if (keyType.IsEnum)
                        EnumDataDict.Add(dataType, keyInfo, table);
                    ResourceManager.Dispose<ScriptableObject>(tablePath);
                }
                catch
                {
                    Log.Error($"<color=#FFAA00>Load Data Table =></color> <color=#00FFFF>{keyName}</color> <color=#FF0000>Fail</color>");
                }
            }
        }

        public override void OnDispose()
        {
            IntDataDict.Clear();
            StrDataDict.Clear();
            EnumDataDict.Clear();
        }
    }
}