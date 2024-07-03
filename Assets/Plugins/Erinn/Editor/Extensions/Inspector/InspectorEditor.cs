//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Erinn
{
    internal static class InspectorEditor
    {
        private const string Name = "Mórrígan";
        private static VisualElement _inspector;
        private static int _colorIndex;
        private static readonly List<EditorWindow> FailedWindows = new();
        private static double _timeSceneStartup;

        [InitializeOnLoadMethod]
        public static void Init()
        {
            EditorManager.TryCombineOnSelectChanged(Awake);
            EditorManager.TryCombineOnMaximizedChanged(OnMaximizedChanged);
            EditorApplication.delayCall -= Awake;
            MathV.Combine(ref EditorApplication.delayCall, Awake);
        }

        private static void Awake()
        {
            FailedWindows.Clear();
            foreach (var o in Resources.FindObjectsOfTypeAll(InspectorReflection.Type))
                if (o is EditorWindow editorWindow && !TryStartUpWindow(editorWindow))
                    FailedWindows.Add(editorWindow);
            if (FailedWindows.Count == 0)
                return;
            _timeSceneStartup = EditorApplication.timeSinceStartup;
            EditorManager.TryCombineOnUpdate(Update);
        }

        private static void Update()
        {
            if (EditorApplication.timeSinceStartup - _timeSceneStartup <= 0.1)
                return;
            EditorManager.RemoveOnUpdate(Update);
            if (FailedWindows.Count == 0)
                return;
            UpdateForWindow(FailedWindows);
            FailedWindows.Clear();
        }

        private static void UpdateForWindow(List<EditorWindow> windows)
        {
            var array = Array.FindAll(windows.ToArray(), window => window != null);
            foreach (var editorWindow in array)
                TryStartUpWindow(editorWindow);
        }

        private static bool TryStartUpWindow(EditorWindow editorWindow)
        {
            var visualElement = TryGetVisualElement(editorWindow);
            return visualElement is { childCount: >= 2 } && Start(GetVisualElement(visualElement, "unity-inspector-editors-list"));
        }

        private static VisualElement TryGetVisualElement(EditorWindow window) => window != null ? GetVisualElement(window.rootVisualElement, "unity-inspector-main-container") : null;

        private static VisualElement GetVisualElement(VisualElement visualElement, string className)
        {
            for (var index = 0; index < visualElement.childCount; ++index)
            {
                var visualElement1 = visualElement[index];
                if (visualElement1.ClassListContains(className))
                    return visualElement1;
                var visualElement2 = GetVisualElement(visualElement1, className);
                if (visualElement2 != null)
                    return visualElement2;
            }

            return null;
        }

        private static void OnMaximizedChanged(EditorWindow w)
        {
            foreach (var o in Resources.FindObjectsOfTypeAll(InspectorReflection.Type))
                if (o is EditorWindow editorWindow)
                    TryStartUpWindow(editorWindow);
        }

        private static bool Start(VisualElement visualElement)
        {
            if (visualElement.parent[0].name == Name)
                visualElement.parent.RemoveAt(0);
            if (visualElement.childCount == 0)
            {
                var layout = visualElement.layout;
                if (!float.IsNaN(layout.width))
                {
                    _inspector ??= SpawnInspectorWindow();
                    visualElement.parent.Insert(0, _inspector);
                    return true;
                }
            }

            return false;
        }

        private static VisualElement SpawnInspectorWindow()
        {
            var visualElement = new VisualElement
            {
                name = Name,
                style =
                {
                    borderBottomColor = new StyleColor(ColorType.DarkOliveGreen),
                    borderTopColor = new StyleColor(ColorType.DarkOliveGreen),
                    borderLeftColor = new StyleColor(ColorType.DarkOliveGreen),
                    borderRightColor = new StyleColor(ColorType.DarkOliveGreen),
                    backgroundColor = new StyleColor(ColorType.DarkSlateGray)
                }
            };
            CreateTitle(visualElement);
            InitFramework(visualElement);
            InitSettings(visualElement);
            InitProjectSettings(visualElement);
            InitPackages(visualElement);
            return visualElement;
        }

        private static void InitFramework(VisualElement parent)
        {
            CreateLabel(parent, "Develop");
            var container = CreateContainer(parent);
            CreateButton(container, "Window/Unity Version Control", "Version Control");
            foreach (var submenu in Unsupported.GetSubmenus("Tools/Develop"))
            {
                var strArray = submenu.Split('/');
                CreateButton(container, submenu, strArray[2]);
            }
        }

        private static void InitSettings(VisualElement parent)
        {
            CreateLabel(parent, "Setup");
            var container = CreateContainer(parent);
            parent.Add(container);
            CreateEnumButton(container, "Audio Preview", AudioPreview.Enabled, state =>
            {
                if (state != AudioPreview.Enabled)
                    AudioPreview.Enabled = state;
            });
        }

        private static void InitProjectSettings(VisualElement parent)
        {
            CreateLabel(parent, "Edit");
            var container = CreateContainer(parent);
            CreateButton(container, "Edit/Project Settings...", "Project Settings");
            CreateButton(container, "Edit/Preferences...", "Preferences");
        }

        private static void InitPackages(VisualElement parent)
        {
            CreateLabel(parent, "Package");
            var container = CreateContainer(parent);
            CreateButton(container, "Assets/Import Package/Custom Package...", "Custom Package");
            var flag = true;
            var str = "";
            foreach (var submenu in Unsupported.GetSubmenus("Window"))
            {
                var upper = CultureInfo.InvariantCulture.TextInfo.ToUpper(submenu);
                if (flag)
                {
                    if (upper == "WINDOW/PACKAGE MANAGER")
                        flag = false;
                    else
                        continue;
                }

                var strArray = submenu.Split('/');
                var key1 = strArray[1];
                if (CultureInfo.InvariantCulture.TextInfo.ToUpper(key1) != "ASSET MANAGEMENT")
                    switch (strArray.Length)
                    {
                        case 2:
                            CreateButton(container, submenu, key1);
                            continue;
                        case 3:
                            if (str != key1)
                            {
                                CreateLabel(parent, key1);
                                container = CreateContainer(parent);
                                str = key1;
                            }

                            CreateButton(container, submenu, strArray[2]);
                            continue;
                        case 4:
                            var key2 = strArray[2];
                            if (str != key2)
                            {
                                CreateLabel(parent, key2);
                                container = CreateContainer(parent);
                                str = key2;
                            }

                            CreateButton(container, submenu, strArray[3]);
                            continue;
                        default:
                            continue;
                    }
            }

            CreateContent(container, "Molth Nevin");
        }

        private static void CreateTitle(VisualElement element)
        {
            var title = new Label(Name)
            {
                style =
                {
                    height = new StyleLength(28f),
                    fontSize = new StyleLength(15f),
                    borderBottomWidth = new StyleFloat(1f),
                    borderTopWidth = new StyleFloat(1f),
                    borderLeftWidth = new StyleFloat(1f),
                    borderRightWidth = new StyleFloat(1f),
                    borderLeftColor = new StyleColor(ColorType.LimeGreen),
                    borderRightColor = new StyleColor(ColorType.LimeGreen),
                    borderTopColor = new StyleColor(ColorType.LimeGreen),
                    borderBottomColor = new StyleColor(ColorType.LimeGreen),
                    backgroundColor = new StyleColor(ColorType.DarkSlateGray),
                    unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4),
                    color = new StyleColor(ColorType.DarkPink)
                }
            };
            element.Add(title);
        }

        private static void CreateContent(VisualElement element, string content)
        {
            var title = new Label(content)
            {
                style =
                {
                    borderBottomWidth = new StyleFloat(1f),
                    borderTopWidth = new StyleFloat(1f),
                    borderLeftWidth = new StyleFloat(1f),
                    borderRightWidth = new StyleFloat(1f),
                    borderLeftColor = new StyleColor(ColorType.LimeGreen),
                    borderRightColor = new StyleColor(ColorType.LimeGreen),
                    borderTopColor = new StyleColor(ColorType.LimeGreen),
                    borderBottomColor = new StyleColor(ColorType.LimeGreen),
                    fontSize = new StyleLength(14f),
                    marginTop = new StyleLength(3f),
                    marginLeft = new StyleLength(3f),
                    marginRight = new StyleLength(3f),
                    paddingLeft = new StyleLength(5f),
                    backgroundColor = new StyleColor(ColorType.DarkSlateBlue),
                    unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4),
                    color = new StyleColor(ColorType.DarkSeaGreen)
                }
            };
            element.Add(title);
        }

        private static void CreateLabel(VisualElement parent, string text)
        {
            var label = new Label(text)
            {
                style =
                {
                    fontSize = new StyleLength(12f),
                    marginTop = new StyleLength(3f),
                    marginLeft = new StyleLength(3f),
                    marginRight = new StyleLength(3f),
                    paddingLeft = new StyleLength(5f),
                    color = new StyleColor(ColorType.LightGoldenrodYellow),
                    backgroundColor = new StyleColor(ColorType.DarkGreen)
                }
            };
            parent.Add(label);
        }

        private static void CreateEnumButton<T>(VisualElement parent, string text, T value, Action<T> onValueChanged) where T : Enum
        {
            var container = new VisualElement
            {
                style =
                {
                    marginTop = new StyleLength(2f),
                    backgroundColor = new StyleColor(ColorType.DarkSlateGray),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    alignItems = new StyleEnum<Align>(Align.Center),
                    justifyContent = new StyleEnum<Justify>(Justify.SpaceBetween),
                    paddingLeft = new StyleLength(10f),
                    borderBottomWidth = new StyleFloat(1f),
                    borderTopWidth = new StyleFloat(1f),
                    borderLeftWidth = new StyleFloat(1f),
                    borderRightWidth = new StyleFloat(1f),
                    borderLeftColor = new StyleColor(ColorType.LimeGreen),
                    borderRightColor = new StyleColor(ColorType.LimeGreen),
                    borderTopColor = new StyleColor(ColorType.LimeGreen),
                    borderBottomColor = new StyleColor(ColorType.LimeGreen)
                }
            };
            var label = new Label(text)
            {
                style =
                {
                    unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)3),
                    color = new StyleColor(GetColor())
                }
            };
            var enumField = new EnumField(value)
            {
                style =
                {
                    unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)3),
                    width = new StyleLength(60f)
                }
            };
            enumField.RegisterValueChangedCallback(evt => onValueChanged?.Invoke((T)evt.newValue));
            container.Add(label);
            container.Add(enumField);
            parent.Add(container);
        }

        private static void CreateButton(VisualElement parent, string submenu, string text)
        {
            var button = new Button(() => EditorApplication.ExecuteMenuItem(submenu))
            {
                text = text,
                style =
                {
                    left = new StyleLength(0.0f),
                    unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)4),
                    backgroundColor = new StyleColor(ColorType.DarkSlateGray),
                    color = new StyleColor(GetColor())
                }
            };
            parent.Add(button);
        }

        private static VisualElement CreateContainer(VisualElement parent)
        {
            var container = new VisualElement
            {
                style =
                {
                    borderTopWidth = new StyleFloat(0.2f),
                    borderBottomWidth = new StyleFloat(0.2f),
                    borderLeftWidth = new StyleFloat(2.5f),
                    borderRightWidth = new StyleFloat(2.5f),
                    backgroundColor = new StyleColor(ColorType.DimGray),
                    borderTopColor = new StyleColor(ColorType.DarkSlateGray),
                    borderBottomColor = new StyleColor(ColorType.DarkSlateGray),
                    borderLeftColor = new StyleColor(ColorType.DarkSlateGray),
                    borderRightColor = new StyleColor(ColorType.DarkSlateGray),
                    marginTop = new StyleLength(2f)
                }
            };
            parent.Add(container);
            return container;
        }

        private static Color GetColor()
        {
            if (_colorIndex == 0)
            {
                _colorIndex = 1;
                return ColorType.DeepSkyBlue;
            }

            _colorIndex = 0;
            return ColorType.LawnGreen;
        }
    }
}