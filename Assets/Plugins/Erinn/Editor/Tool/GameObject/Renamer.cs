//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Erinn
{
    internal sealed class Renamer : EditorWindow
    {
        private const string Green = "#c9e743";
        private readonly string[] _caseOptions = { "Default", "Lowercase", "Uppercase" };
        private readonly char[] _digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly char[] _punctuation = { '.', ',', ';', ':' };
        private readonly string[] _stripOptions = { "Start", "Finish", "Both" };
        private readonly string[] _toolbarOptions = { "Replace", "Reset", "Stripping", "Other" };
        private int _caseSelection;
        private bool _caseSensitiveReplace;
        private string _customChars = "";
        private Vector2 _infoBoxScroll;
        private string _infoBoxText = "";
        private string _prefix = "";
        private string _removeAfter = "";
        private string _removeBefore = "";
        private string _replacePattern = "";
        private string _replaceWith = "";
        private string _setName = "";
        private bool _stripDigits;
        private bool _stripPunctuation;
        private int _stripSelection;
        private bool _stripSpaces;
        private string _suffix = "";
        private int _toolbarSelection;

        private void OnEnable() => Selection.selectionChanged += SetInfoBoxText;

        private void OnDisable() => Selection.selectionChanged -= SetInfoBoxText;

        private void OnGUI()
        {
            GUI.changed = false;
            _toolbarSelection = GUILayout.Toolbar(_toolbarSelection, _toolbarOptions);
            switch (_toolbarSelection)
            {
                case 0:
                    GUILayout.BeginHorizontal();
                    EditorGUIUtility.fieldWidth = position.width - 350;
                    _replacePattern = EditorGUILayout.TextField("Replace", _replacePattern);
                    _caseSensitiveReplace = EditorGUILayout.ToggleLeft("Match case", _caseSensitiveReplace);
                    GUILayout.EndHorizontal();
                    _replaceWith = EditorGUILayout.TextField("Use", _replaceWith);
                    break;
                case 1:
                    _prefix = EditorGUILayout.TextField("Prefix", _prefix);
                    _setName = EditorGUILayout.TextField("Name", _setName);
                    _suffix = EditorGUILayout.TextField("Suffix", _suffix);
                    break;
                case 2:
                    _customChars = EditorGUILayout.TextField("Custom Characters", _customChars);
                    GUILayout.BeginHorizontal("Box");
                    GUILayout.FlexibleSpace();
                    _stripSpaces = EditorGUILayout.ToggleLeft("Space", _stripSpaces);
                    _stripDigits = EditorGUILayout.ToggleLeft("Number", _stripDigits);
                    _stripPunctuation = EditorGUILayout.ToggleLeft("Punctuation marks", _stripPunctuation);
                    GUILayout.EndHorizontal();
                    _stripSelection = GUILayout.Toolbar(_stripSelection, _stripOptions);
                    break;
                case 3:
                    _removeAfter = EditorGUILayout.TextField("Stay...Remove Later", _removeAfter);
                    _removeBefore = EditorGUILayout.TextField("Stay...Remove Before", _removeBefore);
                    GUILayout.BeginHorizontal();
                    _caseSelection = GUILayout.Toolbar(_caseSelection, _caseOptions);
                    GUILayout.EndHorizontal();
                    break;
            }

            if (GUI.changed) SetInfoBoxText();
            var resultInfoBoxStyle = GUI.skin.GetStyle("HelpBox");
            resultInfoBoxStyle.richText = true;
            resultInfoBoxStyle.stretchHeight = true;
            resultInfoBoxStyle.fontSize = 12;
            _infoBoxScroll = EditorGUILayout.BeginScrollView(_infoBoxScroll);
            EditorGUILayout.LabelField(_infoBoxText, resultInfoBoxStyle);
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Rename"))
            {
                Rename();
                SetInfoBoxText();
            }
        }

        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<Renamer>();
            window.titleContent = new GUIContent("Object renaming");
        }

        private void Rename()
        {
            foreach (var obj in Selection.gameObjects)
            {
                Undo.RecordObject(obj, "Rename");
                obj.name = GetRenamed(obj.name);
            }
        }

        private void SetInfoBoxText()
        {
            _infoBoxText = "";
            foreach (var obj in Selection.gameObjects)
                _infoBoxText += $"<b>{obj.name}</b> -> <b><color={Green}>{GetRenamed(obj.name)}</color></b>\r\n";
            Repaint();
        }

        private string GetRenamed(string inputName)
        {
            switch (_toolbarSelection)
            {
                case 0:
                    try
                    {
                        var regexOptions = _caseSensitiveReplace ? RegexOptions.None : RegexOptions.IgnoreCase;
                        inputName = Regex.Replace(inputName, _replacePattern, _replaceWith, regexOptions);
                    }
                    catch
                    {
                        Debug.Log("Nothing Is Valid!");
                    }

                    break;
                case 1:
                    if (_setName.Length != 0)
                        inputName = _setName;
                    inputName = _prefix + inputName;
                    inputName += _suffix;
                    break;
                case 2:
                    switch (_stripSelection)
                    {
                        case 0:
                            inputName = inputName.TrimStart(_customChars.ToCharArray());
                            inputName = _stripSpaces ? inputName.TrimStart() : inputName;
                            inputName = _stripDigits ? inputName.TrimStart(_digits) : inputName;
                            inputName = _stripPunctuation ? inputName.TrimStart(_punctuation) : inputName;
                            break;
                        case 1:
                            inputName = inputName.TrimEnd(_customChars.ToCharArray());
                            inputName = _stripSpaces ? inputName.TrimEnd() : inputName;
                            inputName = _stripDigits ? inputName.TrimEnd(_digits) : inputName;
                            inputName = _stripPunctuation ? inputName.TrimEnd(_punctuation) : inputName;
                            break;
                        case 2:
                            inputName = inputName.Trim(_customChars.ToCharArray());
                            inputName = _stripSpaces ? inputName.Trim() : inputName;
                            inputName = _stripDigits ? inputName.Trim(_digits) : inputName;
                            inputName = _stripPunctuation ? inputName.Trim(_punctuation) : inputName;
                            break;
                    }

                    break;
                case 3:
                    try
                    {
                        if (_removeAfter.Length != 0)
                            inputName = inputName.Split(_removeAfter)[0];
                        if (_removeBefore.Length != 0)
                            inputName = inputName.Split(_removeBefore)[1];
                    }
                    catch
                    {
                        Debug.Log("Invalid! ");
                    }

                    inputName = _caseSelection switch
                    {
                        1 => inputName.ToLower(),
                        2 => inputName.ToUpper(),
                        _ => inputName
                    };
                    break;
            }

            return inputName;
        }
    }
}