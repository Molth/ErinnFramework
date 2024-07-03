//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    public static class ExcelTableParser
    {
        public static bool TryParse(this string s, out Vector2 r)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var floatArray = MathV.ParseFloats(s, 2);
                r = new Vector2(floatArray[0], floatArray[1]);
                return true;
            }

            r = Vector2.zero;
            return false;
        }

        public static bool TryParse(this string s, out Vector3 r)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var floatArray = MathV.ParseFloats(s, 3);
                r = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
                return true;
            }

            r = Vector3.zero;
            return false;
        }

        public static bool TryParse(this string s, out Vector4 r)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var floatArray = MathV.ParseFloats(s, 4);
                r = new Vector4(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
                return true;
            }

            r = Vector4.zero;
            return false;
        }

        public static bool TryParse(this string s, out Vector2Int r)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var intArray = MathV.ParseInts(s, 2);
                r = new Vector2Int(intArray[0], intArray[1]);
                return true;
            }

            r = Vector2Int.zero;
            return false;
        }

        public static bool TryParse(this string s, out Vector3Int r)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var intArray = MathV.ParseInts(s, 3);
                r = new Vector3Int(intArray[0], intArray[1], intArray[2]);
                return true;
            }

            r = Vector3Int.zero;
            return false;
        }

        public static bool TryParse(this string s, out Color r)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var floatArray = MathV.ParseFloats(s, 4);
                r = new Color(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
                return true;
            }

            r = Color.white;
            return false;
        }

        public static bool TryParse(this string s, out Quaternion r)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var floatArray = MathV.ParseFloats(s, 4);
                r = new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);
                return true;
            }

            r = Quaternion.identity;
            return false;
        }

        public static bool TryParse(this string s, out Vector2[] r)
        {
            if (string.IsNullOrEmpty(s))
            {
                r = null;
                return false;
            }

            var strArray = MathV.SplitByDot(s);
            var length = strArray.Length;
            r = new Vector2[length];
            for (var index = 0; index < length; ++index)
                strArray[index].TryParse(out r[index]);
            return true;
        }

        public static bool TryParse(this string s, out Vector3[] r)
        {
            if (string.IsNullOrEmpty(s))
            {
                r = null;
                return false;
            }

            var strArray = MathV.SplitByDot(s);
            var length = strArray.Length;
            r = new Vector3[length];
            for (var index = 0; index < length; ++index)
                strArray[index].TryParse(out r[index]);
            return true;
        }

        public static bool TryParse(this string s, out Vector4[] r)
        {
            if (string.IsNullOrEmpty(s))
            {
                r = null;
                return false;
            }

            var strArray = MathV.SplitByDot(s);
            var length = strArray.Length;
            r = new Vector4[length];
            for (var index = 0; index < length; ++index)
                strArray[index].TryParse(out r[index]);
            return true;
        }

        public static bool TryParse(this string s, out Vector2Int[] r)
        {
            if (string.IsNullOrEmpty(s))
            {
                r = null;
                return false;
            }

            var strArray = MathV.SplitByDot(s);
            var length = strArray.Length;
            r = new Vector2Int[length];
            for (var index = 0; index < length; ++index)
                strArray[index].TryParse(out r[index]);
            return true;
        }

        public static bool TryParse(this string s, out Vector3Int[] r)
        {
            if (string.IsNullOrEmpty(s))
            {
                r = null;
                return false;
            }

            var strArray = MathV.SplitByDot(s);
            var length = strArray.Length;
            r = new Vector3Int[length];
            for (var index = 0; index < length; ++index)
                strArray[index].TryParse(out r[index]);
            return true;
        }

        public static bool TryParse(this string s, out Color[] r)
        {
            if (string.IsNullOrEmpty(s))
            {
                r = null;
                return false;
            }

            var strArray = MathV.SplitByDot(s);
            var length = strArray.Length;
            r = new Color[length];
            for (var index = 0; index < length; ++index)
                strArray[index].TryParse(out r[index]);
            return true;
        }

        public static bool TryParse(this string s, out Quaternion[] r)
        {
            if (string.IsNullOrEmpty(s))
            {
                r = null;
                return false;
            }

            var strArray = MathV.SplitByDot(s);
            var length = strArray.Length;
            r = new Quaternion[length];
            for (var index = 0; index < length; ++index)
                strArray[index].TryParse(out r[index]);
            return true;
        }
    }
}