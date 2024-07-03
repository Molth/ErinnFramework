//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class ExcelParser
    {
        public static void GenerateAssets()
        {
            var timeSinceStartup = EditorApplication.timeSinceStartup;
            var assets = Directory.GetFiles(PathDataKey);
            var array = Array.FindAll(assets, IsSupport);
            var valueTupleList = new List<(string, List<string[]>)>();
            if (!Directory.Exists(ExcelParserSetting.AssetPath))
                Directory.CreateDirectory(ExcelParserSetting.AssetPath);
            var num = 0;
            var maxProgress = 0;
            foreach (var excelPath in array)
            {
                var excelData = GetExcelData(excelPath);
                valueTupleList.AddRange(excelData);
                maxProgress += excelData.Count;
            }

            try
            {
                foreach (var (sheetName, strArrayList) in valueTupleList)
                {
                    UpdateProgress(++num, maxProgress);
                    var tableWithNamespace = GetTableWithNamespace(sheetName);
                    var instance = ScriptableObject.CreateInstance(tableWithNamespace);
                    if (instance == null)
                    {
                        Debug.LogWarning("Establish <color=#FF8888>" + tableWithNamespace + "</color> Fail!");
                    }
                    else
                    {
                        var constructor = GetTypeByString(GetDataWithNamespace(sheetName)).GetConstructor(new[]
                        {
                            typeof(string[]),
                            typeof(int)
                        });
                        if (!(constructor == null))
                        {
                            foreach (var strArray in strArrayList)
                            {
                                if (!string.IsNullOrEmpty(strArray[0]))
                                {
                                    var data = (IExcelDataBase)constructor.Invoke(new object[]
                                    {
                                        strArray,
                                        0
                                    });
                                    ((IExcelTableBase)instance).AddData(data);
                                }
                            }

                            var assetsPath = GetAssetsPath(sheetName);
                            if (File.Exists(assetsPath))
                                AssetDatabase.DeleteAsset(assetsPath);
                            AssetDatabase.CreateAsset(instance, assetsPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }
            finally
            {
                RemoveProgress();
                AssetDatabase.Refresh();
            }

            Debug.Log("Data generation completed, Time consuming:<color=#00FF00> " + (EditorApplication.timeSinceStartup - timeSinceStartup).ToString("F") + " </color>Second");
            AddressableTool.AddressableUpdateGroup("ExcelTable", "Assets/AddressableResources/ExcelTable");
        }

        public static void GenerateScripts()
        {
            var assets = Directory.GetFiles(PathDataKey);
            var array = Array.FindAll(assets, IsSupport);
            var valueTupleList = new List<(string, string)>();
            foreach (var excelPath in array)
                valueTupleList.AddRange(GenerateTexts(excelPath));
            try
            {
                foreach (var (sheetName, compareText) in valueTupleList)
                    if (!CreateAndCompare(GetScriptPath(sheetName), compareText) && IsChanged)
                        IsChanged = false;
                if (IsChanged)
                {
                    LoadDataKey = false;
                    GenerateAssets();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            AssetDatabase.Refresh();
        }

        private static List<(string, string)> GenerateTexts(string excelPath)
        {
            using var excelPackage = new ExcelPackage(new FileInfo(excelPath));
            var worksheets = excelPackage.Workbook.Worksheets;
            var texts = new List<(string, string)>();
            foreach (var excelWorksheet in worksheets)
            {
                if (excelWorksheet.Dimension != null)
                {
                    var row = excelWorksheet.Dimension.End.Row;
                    var column = excelWorksheet.Dimension.End.Column;
                    var fieldList = new List<(string, string, string)>();
                    for (var col = 1; col <= column; ++col)
                    {
                        var description = excelWorksheet.Cells[1, col].Value?.ToString();
                        var name = excelWorksheet.Cells[2, col].Value?.ToString();
                        var type = excelWorksheet.Cells[3, col].Value?.ToString();
                        if (!string.IsNullOrEmpty(name))
                        {
                            if (IsValidField(type))
                            {
                                var flag = false;
                                if (IsValidStruct(type))
                                {
                                    flag = type != null && type.EndsWith("[]");
                                    var upper1 = name[..1].ToUpper();
                                    var str1 = name;
                                    var str2 = str1.Substring(1, str1.Length - 1);
                                    name = upper1 + str2;
                                    WriteStruct(name, type, description);
                                    var upper2 = name[..1].ToUpper();
                                    var str3 = name;
                                    var str4 = str3.Substring(1, str3.Length - 1);
                                    type = upper2 + str4;
                                }

                                fieldList.Add((name, flag ? type + "[]" : type, description));
                            }
                            else if (type is "enum" or "const")
                            {
                                var writers = new List<string>();
                                for (var index = 0; index < row; ++index)
                                {
                                    var str = excelWorksheet.Cells[4 + index, col].Value?.ToString();
                                    if (!string.IsNullOrEmpty(str))
                                        writers.Add(str);
                                }

                                WriteEnumOrConst(name, type, writers);
                            }
                        }
                    }

                    if (fieldList.Count != 0)
                    {
                        var str = WriteDataTable(excelWorksheet.Name, fieldList);
                        texts.Add((excelWorksheet.Name, str));
                        Debug.Log("Automatically generate scripts <color=#00FF00> " + excelWorksheet.Name + " </color> Success!");
                    }
                }
            }

            return texts;
        }
    }
}