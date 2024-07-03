//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using UnityEngine;

namespace Erinn
{
    public static partial class UnityExtensions
    {
        public static T AddComponent<T>(this Component t) where T : Component => t.gameObject.AddComponent<T>();

        public static T GetOrAddComponent<T>(this GameObject t) where T : Component => t.TryGetComponent<T>(out var component) ? component : t.AddComponent<T>();

        public static T GetOrAddComponent<T>(this Component t) where T : Component => t.TryGetComponent<T>(out var component) ? component : t.AddComponent<T>();

        public static T FindComponent<T>(this GameObject t, string name) where T : Component
        {
            foreach (var component in t.GetComponentsInChildren<T>())
                if (component.name == name)
                    return component;
            return null;
        }

        public static T FindComponent<T>(this Component t, string name) where T : Component
        {
            foreach (var component in t.GetComponentsInChildren<T>())
                if (component.name == name)
                    return component;
            return null;
        }
    }
}