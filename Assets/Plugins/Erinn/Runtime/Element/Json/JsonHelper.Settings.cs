//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Erinn
{
    internal static class JsonHelperSettings
    {
        private static readonly Dictionary<Type, Action<JsonWriter, object>> TypeMappings = new()
        {
            {
                typeof(Vector2), (w, v) =>
                {
                    var vector2 = (Vector2)v;
                    w.WritePropertyName("x");
                    w.WriteValue(vector2.x);
                    w.WritePropertyName("y");
                    w.WriteValue(vector2.y);
                }
            },
            {
                typeof(Vector3), (w, v) =>
                {
                    var vector3 = (Vector3)v;
                    w.WritePropertyName("x");
                    w.WriteValue(vector3.x);
                    w.WritePropertyName("y");
                    w.WriteValue(vector3.y);
                    w.WritePropertyName("z");
                    w.WriteValue(vector3.z);
                }
            },
            {
                typeof(Vector4), (w, v) =>
                {
                    var vector4 = (Vector4)v;
                    w.WritePropertyName("x");
                    w.WriteValue(vector4.x);
                    w.WritePropertyName("y");
                    w.WriteValue(vector4.y);
                    w.WritePropertyName("z");
                    w.WriteValue(vector4.z);
                    w.WritePropertyName("w");
                    w.WriteValue(vector4.w);
                }
            },
            {
                typeof(Vector2Int), (w, v) =>
                {
                    var vector2Int = (Vector2Int)v;
                    w.WritePropertyName("x");
                    w.WriteValue(vector2Int.x);
                    w.WritePropertyName("y");
                    w.WriteValue(vector2Int.y);
                }
            },
            {
                typeof(Vector3Int), (w, v) =>
                {
                    var vector3Int = (Vector3Int)v;
                    w.WritePropertyName("x");
                    w.WriteValue(vector3Int.x);
                    w.WritePropertyName("y");
                    w.WriteValue(vector3Int.y);
                    w.WritePropertyName("z");
                    w.WriteValue(vector3Int.z);
                }
            },
            {
                typeof(Color), (w, v) =>
                {
                    var color = (Color)v;
                    w.WritePropertyName("r");
                    w.WriteValue(color.r);
                    w.WritePropertyName("g");
                    w.WriteValue(color.g);
                    w.WritePropertyName("b");
                    w.WriteValue(color.b);
                    w.WritePropertyName("a");
                    w.WriteValue(color.a);
                }
            },
            {
                typeof(Quaternion), (w, v) =>
                {
                    var quaternion = (Quaternion)v;
                    w.WritePropertyName("x");
                    w.WriteValue(quaternion.x);
                    w.WritePropertyName("y");
                    w.WriteValue(quaternion.y);
                    w.WritePropertyName("z");
                    w.WriteValue(quaternion.z);
                    w.WritePropertyName("w");
                    w.WriteValue(quaternion.w);
                }
            }
        };

        public static bool ContainsType(Type type) => TypeMappings.ContainsKey(type);

        public static Action<JsonWriter, object> GetWriterParse(Type type) => TypeMappings.ContainsKey(type) ? TypeMappings[type] : null;
    }
}