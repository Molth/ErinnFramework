//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Reflection;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Erinn
{
    /// <summary>
    ///     Dependency injection
    /// </summary>
    public static class EditorInjector
    {
        /// <summary>
        ///     Bind button
        /// </summary>
        public static void BindButtonClick(MonoBehaviour monoBehaviour)
        {
            var type = monoBehaviour.GetType();
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes<BindButtonAttribute>();
                foreach (var attribute in attributes)
                {
                    var targetButtonName = attribute.Target;
                    var fieldInfo = type.GetField(targetButtonName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (fieldInfo != null && fieldInfo.FieldType == typeof(Button))
                    {
                        var targetButton = (Button)fieldInfo.GetValue(monoBehaviour);
                        if (targetButton != null)
                        {
                            var callback = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), monoBehaviour, method);
                            UnityEventTools.AddPersistentListener(targetButton.onClick, callback);
                        }
                    }
                }
            }
        }
    }
}