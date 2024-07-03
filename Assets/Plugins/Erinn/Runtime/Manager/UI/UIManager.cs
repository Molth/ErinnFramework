//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Erinn
{
    /// <summary>
    ///     UIInterface Manager
    /// </summary>
    internal sealed class UIManager : ModuleSingleton, IUIManager
    {
        /// <summary>
        ///     UI Hierarchical array
        /// </summary>
        private static readonly Transform[] LayerGroup = new Transform[5];

        /// <summary>
        ///     UI Dictionary
        /// </summary>
        public static readonly Dictionary<Type, UIBase> UIDict = new();

        public override int Priority => 5;

        void IUIManager.Show<T>() => Show<T>(null);

        void IUIManager.Show<T>(Action<T> action) => Show(action);

        void IUIManager.ShowAsync<T>() => ShowAsync<T>(null);

        void IUIManager.ShowAsync<T>(Action<T> action) => ShowAsync(action);

        void IUIManager.Show<T>(string path) => Show<T>(path, null);

        void IUIManager.Show<T>(string path, Action<T> action) => Show(path, action);

        void IUIManager.ShowAsync<T>(string path) => ShowAsync<T>(path, null);

        void IUIManager.ShowAsync<T>(string path, Action<T> action) => ShowAsync(path, action);

        void IUIManager.Hide<T>() => Hide<T>();

        T IUIManager.Get<T>() => Get<T>();

        /// <summary>
        ///     UIManager synchronization display UIBase class
        /// </summary>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        public static void Show<T>(Action<T> action) where T : UIBase
        {
            var key = typeof(T);
            if (UIDict.TryGetValue(key, out var value))
            {
                value.Show();
                action?.Invoke((T)value);
                return;
            }

            var obj = ResourceManager.Load<GameObject>("UI/" + key.Name);
            if (action != null)
                OnLoadComplete(key, obj, action);
            else
                OnLoadComplete<T>(key, obj);
        }

        /// <summary>
        ///     UIManager asynchronous display UIBase class
        /// </summary>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        public static void ShowAsync<T>(Action<T> action) where T : UIBase
        {
            var key = typeof(T);
            if (UIDict.TryGetValue(key, out var value))
            {
                value.Show();
                action?.Invoke((T)value);
                return;
            }

            if (action != null)
                ResourceManager.LoadAsync<GameObject>("UI/" + key.Name, obj => OnLoadComplete(key, obj, action));
            else
                ResourceManager.LoadAsync<GameObject>("UI/" + key.Name, obj => OnLoadComplete<T>(key, obj));
        }

        /// <summary>
        ///     UIManager synchronization display UIBase class
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        public static void Show<T>(string path, Action<T> action) where T : UIBase
        {
            var key = typeof(T);
            if (UIDict.TryGetValue(key, out var value))
            {
                value.Show();
                action?.Invoke((T)value);
                return;
            }

            var obj = ResourceManager.Load<GameObject>("UI/" + path);
            if (action != null)
                OnLoadComplete(key, obj, action);
            else
                OnLoadComplete<T>(key, obj);
        }

        /// <summary>
        ///     UIManager asynchronous display UIBase class
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="action">Display callback for base class</param>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        public static void ShowAsync<T>(string path, Action<T> action) where T : UIBase
        {
            var key = typeof(T);
            if (UIDict.TryGetValue(key, out var value))
            {
                value.Show();
                action?.Invoke((T)value);
                return;
            }

            if (action != null)
                ResourceManager.LoadAsync<GameObject>("UI/" + path, obj => OnLoadComplete(key, obj, action));
            else
                ResourceManager.LoadAsync<GameObject>("UI/" + path, obj => OnLoadComplete<T>(key, obj));
        }

        /// <summary>
        ///     Call upon completion of loading
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="obj">Object</param>
        /// <typeparam name="T">Type</typeparam>
        private static void OnLoadComplete<T>(Type key, GameObject obj) where T : UIBase
        {
            if (obj == null)
                return;
            obj.name = key.Name;
            var _ = obj.GetComponent<T>();
            if (_ == null)
                return;
            obj.transform.SetParent(GetLayer(_.Layer), false);
            UIDict.Add(key, _);
            _.Show();
        }

        /// <summary>
        ///     Call upon completion of loading
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="obj">Object</param>
        /// <param name="action">Event</param>
        /// <typeparam name="T">Type</typeparam>
        private static void OnLoadComplete<T>(Type key, GameObject obj, Action<T> action) where T : UIBase
        {
            if (obj == null)
                return;
            obj.name = key.Name;
            var _ = obj.GetComponent<T>();
            if (_ == null)
                return;
            obj.transform.SetParent(GetLayer(_.Layer), false);
            UIDict.Add(key, _);
            _.Show();
            action?.Invoke(_);
        }

        /// <summary>
        ///     UIManager HiddenUIBase class
        /// </summary>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        public static void Hide<T>() where T : UIBase
        {
            var key = typeof(T);
            if (UIDict.TryGetValue(key, out var value))
            {
                value.Hide();
                if (value.HideType == UIHideType.Destroy)
                {
                    var obj = value.gameObject;
                    UIDict.Remove(key);
                    Object.Destroy(obj);
                }
            }
        }

        /// <summary>
        ///     UIManager GetUIBase class
        /// </summary>
        /// <typeparam name="T">Can use all inheritance UIBase Object</typeparam>
        /// <returns>Obtained UI</returns>
        public static T Get<T>() where T : UIBase
        {
            var key = typeof(T);
            return UIDict.TryGetValue(key, out var value) ? (T)value : null;
        }

        /// <summary>
        ///     UIManager obtains hierarchy
        /// </summary>
        /// <param name="layer">Type of hierarchy</param>
        /// <returns>Return to the obtained hierarchy</returns>
        private static Transform GetLayer(UILayer layer) => LayerGroup[(int)layer];

        /// <summary>
        ///     InitializationUIHierarchical Group
        /// </summary>
        /// <param name="canvas">UICanvas</param>
        public static void InitCanvasLayerGroup(Transform canvas)
        {
            LayerGroup[0] = canvas.Find("Layer0");
            LayerGroup[1] = canvas.Find("Layer1");
            LayerGroup[2] = canvas.Find("Layer2");
            LayerGroup[3] = canvas.Find("Layer3");
            LayerGroup[4] = canvas.Find("Layer4");
        }

        /// <summary>
        ///     Call upon destruction
        /// </summary>
        public override void OnDispose()
        {
            Array.Clear(LayerGroup, 0, 5);
            UIDict.Clear();
        }
    }
}