//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class ExcelParser
    {
        private static List<(string, List<string[]>)> GetExcelData(string excelPath)
        {
            using var excelPackage = new ExcelPackage(new FileInfo(excelPath));
            var worksheets = excelPackage.Workbook.Worksheets;
            var excelData = new List<(string, List<string[]>)>();
            foreach (var excelWorksheet in worksheets)
            {
                if (excelWorksheet.Dimension != null)
                {
                    var row = excelWorksheet.Dimension.End.Row;
                    var column = excelWorksheet.Dimension.End.Column;
                    var intList = new List<int>();
                    for (var col = 1; col <= column; ++col)
                    {
                        var str = excelWorksheet.Cells[2, col].Value?.ToString();
                        var type = excelWorksheet.Cells[3, col].Value?.ToString();
                        if (!string.IsNullOrEmpty(str) && IsValidField(type))
                            intList.Add(col);
                    }

                    if (intList.Count != 0)
                    {
                        var strArrayList = new List<string[]>();
                        for (var index1 = 0; index1 < row - 3; ++index1)
                        {
                            strArrayList.Add(new string[intList.Count]);
                            for (var index2 = 0; index2 < intList.Count; ++index2)
                                strArrayList[index1][index2] = excelWorksheet.Cells[index1 + 4, intList[index2]].Value?.ToString();
                        }

                        if (strArrayList.Count != 0)
                            excelData.Add((excelWorksheet.Name, strArrayList));
                    }
                }
            }

            return excelData;
        }

        private static string WriteDataTable(string className, List<(string, string, string)> fieldList)
        {
            var builder1 = Pop();
            var builder2 = Pop();
            var stringBuilder = Pop();
            var useUnityEngine = false;
            var ppCount = 0;
            foreach (var (name, type, description) in fieldList)
            {
                builder1.Append(WriteField(name, type, description, true));
                builder2.Append(WriteParse(name, type));
                ppCount++;
                if (!useUnityEngine)
                    if (ExcelParserSetting.UnityEngineArray.Contains(type))
                        useUnityEngine = true;
            }

            if (ppCount > 1)
            {
                var ppIndex = builder2.ToString().LastIndexOf("column++", StringComparison.Ordinal);
                if (ppIndex != -1)
                    builder2.Remove(ppIndex + 6, 2);
            }

            stringBuilder.Append("using System;\r\n");
            if (useUnityEngine)
            {
                stringBuilder.Append("using Erinn;\r\n");
                stringBuilder.Append("using UnityEngine;\r\n\r\n");
            }
            else
            {
                stringBuilder.Append("using Erinn;\r\n\r\n");
            }

            stringBuilder.AppendFormat("namespace {0}\r\n", "ExcelTable");
            stringBuilder.Append("{\r\n");
            stringBuilder.AppendFormat("\tpublic sealed class {0}DataTable : ExcelTableBase<{0}Data>\r\n\t{{\r\n\t}}\r\n\r\n", className);
            stringBuilder.Append("\t[Serializable]\r\n");
            stringBuilder.AppendFormat("\tpublic struct {0}Data : IExcelDataBase\r\n", className);
            stringBuilder.Append("\t{\r\n");
            stringBuilder.Append(Push(builder1));
            stringBuilder.Append("#if UNITY_EDITOR\r\n");
            stringBuilder.AppendFormat("\t\tpublic {0}Data(string[] sheet, int column)\r\n", className);
            stringBuilder.Append("\t\t{\r\n");
            stringBuilder.Append(Push(builder2));
            stringBuilder.Append("\t\t}\r\n");
            stringBuilder.Append("#endif\r\n");
            stringBuilder.Append("\t}\r\n");
            stringBuilder.Append("}");
            return Push(stringBuilder);
        }

        private static void WriteStruct(string name, string type, string description)
        {
            var num = type.IndexOf("}", StringComparison.Ordinal);
            var strArray1 = type.Substring(1, num - 1).Split(',');
            try
            {
                var stringBuilder = Pop();
                var builder = Pop();
                var useUnityEngine = false;
                for (var index = 0; index < strArray1.Length - 1; ++index)
                {
                    var str2 = strArray1[index];
                    var strArray2 = str2.Split(' ');
                    if (strArray2.Length == 2)
                    {
                        builder.Append(WriteField(strArray2[1], strArray2[0], description, true));
                        if (!useUnityEngine)
                            if (ExcelParserSetting.UnityEngineArray.Contains(strArray2[0]))
                                useUnityEngine = true;
                    }
                }

                var last = strArray1[^1];
                var lastArray = last.Split(' ');
                if (lastArray.Length == 2)
                {
                    builder.Append(WriteField(lastArray[1], lastArray[0], description, false));
                    if (!useUnityEngine)
                        if (ExcelParserSetting.UnityEngineArray.Contains(lastArray[0]))
                            useUnityEngine = true;
                }

                if (useUnityEngine)
                {
                    stringBuilder.Append("using System;\r\n");
                    stringBuilder.Append("using UnityEngine;\r\n\r\n");
                }
                else
                {
                    stringBuilder.Append("using System;\r\n\r\n");
                }

                stringBuilder.AppendFormat("namespace {0}\r\n", "ExcelTable");
                stringBuilder.Append("{\r\n");
                stringBuilder.Append("\t[Serializable]\r\n");
                stringBuilder.AppendFormat("\tpublic struct {0}\r\n", name);
                stringBuilder.Append("\t{\r\n");
                stringBuilder.Append(Push(builder));
                stringBuilder.Append("\t}\r\n");
                stringBuilder.Append("}");
                var compareText = Push(stringBuilder);
                CreateAndCompare(GetStructPath(name), compareText);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        private static void WriteEnumOrConst(string name, string type, List<string> writers)
        {
            var stringBuilder = Pop();
            stringBuilder.AppendFormat("namespace {0}\r\n", "ExcelTable");
            stringBuilder.Append("{\r\n");
            stringBuilder.AppendFormat("\tpublic enum {0}\r\n", name);
            stringBuilder.Append("\t{\r\n");
            var builder = Pop();
            var arr = Array.FindAll(writers.ToArray(), writer => writer != null);
            foreach (var strArrays in arr)
            {
                var strArray = strArrays.Split(',');
                if (type != "enum")
                {
                    if (type == "const" && strArray.Length == 3)
                    {
                        builder.AppendFormat(WriteConst(strArray[0], strArray[1], strArray[2]));
                        goto label_10;
                    }
                }
                else
                {
                    switch (strArray.Length)
                    {
                        case 1:
                            builder.AppendFormat(WriteEnum(strArray[0]));
                            goto label_10;
                        case 2:
                            builder.AppendFormat(WriteEnum(strArray[0], strArray[1]));
                            goto label_10;
                    }
                }

                Debug.LogWarning("Automatic generation <color=#FF8888> " + type + " => " + name + " </color> Fail!");
                return;
                label_10: ;
            }

            stringBuilder.Append(Push(builder));
            stringBuilder.Append("\t}\r\n");
            stringBuilder.Append("}");
            var compareText = Push(stringBuilder);
            CreateAndCompare(GetEnumPath(name), compareText);
        }
    }
}