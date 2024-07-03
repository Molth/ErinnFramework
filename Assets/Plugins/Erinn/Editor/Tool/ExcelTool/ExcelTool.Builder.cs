//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Collections.Generic;
using System.Text;

namespace Erinn
{
    internal sealed partial class ExcelParser
    {
        private static readonly Stack<StringBuilder> StringPool = new();

        public static StringBuilder Pop()
        {
            if (StringPool.Count == 0)
                return new StringBuilder(1024);
            var stringBuilder = StringPool.Pop();
            stringBuilder.Clear();
            return stringBuilder;
        }

        public static string Push(StringBuilder builder)
        {
            if (builder == null || StringPool.Contains(builder))
                return null;
            StringPool.Push(builder);
            return builder.ToString();
        }

        public static void Clear()
        {
            foreach (var stringBuilder in StringPool)
                stringBuilder.Clear();
            StringPool.Clear();
        }
    }
}