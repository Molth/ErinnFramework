//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed partial class ExcelParser
    {
        public static bool IsChanged = true;
        private static bool _isCompleted;

        public static bool CreateAndCompare(string filePath, string compareText)
        {
            var directoryName = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryName) && directoryName != null)
                Directory.CreateDirectory(directoryName);
            if (!File.Exists(filePath))
                File.Create(filePath).Close();
            if (File.ReadAllText(filePath) == compareText)
                return true;
            File.WriteAllText(filePath, compareText);
            LoadDataKey = true;
            return false;
        }

        public static string WriteField(string name, string type, string description, bool wrap)
        {
            var builder = Pop();
            if (!description.IsNullOrEmpty())
            {
                builder.Append("\t\t/// <summary>\r\n");
                builder.Append("\t\t///\t\t" + description + "\r\n");
                builder.Append("\t\t/// </summary>\r\n");
            }

            var strArray = name.Split(':');
            if (strArray.Length == 2 && strArray[1].ToLower() == "key")
                builder.Append("\t\t[ExcelTableKey]\r\n");
            builder.AppendFormat(wrap ? "\t\tpublic {0} {1};\r\n\r\n" : "\t\tpublic {0} {1};\r\n", type.Split(":")[0], strArray[0]);
            return Push(builder);
        }

        public static string WriteParse(string name, string type)
        {
            var builder = Pop();
            builder.AppendFormat(!IsValidField(type) ? "\t\t\tsheet[column++].TryParseStruct(out {0});\r\n" : "\t\t\tsheet[column++].TryParse(out {0});\r\n", name.Split(':')[0]);
            return Push(builder);
        }

        public static string WriteEnum(string name)
        {
            var builder = Pop();
            builder.AppendFormat("\t\t{0},\r\n", name);
            return Push(builder);
        }

        public static string WriteEnum(string name, string value)
        {
            var builder = Pop();
            builder.AppendFormat("\t\t{0} = {1},\r\n", name, value);
            return Push(builder);
        }

        public static string WriteConst(string name, string type, string value)
        {
            var builder = Pop();
            if (type != "float")
                builder.AppendFormat(type == "string" ? "\t\tpublic const {0} {1} = \"{2}\";\r\n" : "\t\tpublic const {0} {1} = {2};\r\n", name, type, value);
            else
                builder.AppendFormat("\t\tpublic const {0} {1} = {2}f;\r\n", name, type, value);
            return Push(builder);
        }

        public static Type GetTypeByString(string className)
        {
            var type = Type.GetType(className);
            if (type == null)
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = assembly.GetType(className);
                    if (type != null)
                        break;
                }
            }

            return type;
        }

        public static void UpdateProgress(int curProgress, int maxProgress)
        {
            EditorUtility.DisplayProgressBar("Data import progress [" + curProgress + " / " + maxProgress + "]", "", curProgress / (float)maxProgress);
            _isCompleted = true;
        }

        public static void RemoveProgress()
        {
            if (!_isCompleted)
                return;
            try
            {
                EditorUtility.ClearProgressBar();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.ToString());
            }

            _isCompleted = false;
        }
    }
}