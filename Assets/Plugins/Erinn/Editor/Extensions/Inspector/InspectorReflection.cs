//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Reflection;

namespace Erinn
{
    internal static class InspectorReflection
    {
        private static Assembly _unityEditor;

        private static Type _type;

        private static Assembly EditorAssembly => _unityEditor ??= Assembly.Load("UnityEditor");

        public static Type Type => _type ??= EditorAssembly.GetType("UnityEditor.InspectorWindow");
    }
}