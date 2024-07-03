//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Reflection;

namespace Erinn
{
    internal sealed partial class DataManager
    {
        /// <summary>
        ///     Get the data table object classes in the current assembly
        /// </summary>
        /// <returns>Data Table Object Class</returns>
        private static (Assembly, Type[]) GetAssemblyAndTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                types = Array.FindAll(types, type => typeof(IExcelTableBase).IsAssignableFrom(type) && !type.IsAbstract);
                if (types.Length > 0)
                    return (assembly, types);
            }

            return (null, null);
        }

        /// <summary>
        ///     Obtain the primary key for data
        /// </summary>
        /// <param name="type">Incoming data type</param>
        /// <returns>Return field information</returns>
        private static FieldInfo GetKeyField(Type type)
        {
            var keyType = typeof(ExcelTableKeyAttribute);
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(keyType, false);
                if (attributes.Length > 0)
                    return field;
            }

            return null;
        }

        /// <summary>
        ///     Obtain the type of data table object
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="type"></param>
        /// <returns>Types obtained</returns>
        private static Type GetDataType(Assembly assembly, Type type) => assembly.GetType("ExcelTable." + type.Name[..^5]);
    }
}