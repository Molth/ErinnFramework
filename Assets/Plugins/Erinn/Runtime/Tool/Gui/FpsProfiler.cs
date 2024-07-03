//------------------------------------------------------------
// Erinn Framework
// Copyright © 2024 Molth Nevin. All rights reserved.
//------------------------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Profiling;

namespace Erinn
{
    [HideMonoScript]
    internal sealed class FpsProfiler : MonoBehaviour
    {
        private const float SecondsToMS = 1000.0f;
        private const float StartDelay = 2f;
        private const float TimeBetweenDisplayUpdates = 0.2f;
        private const float TimeToConverge = 4f;
        private const int TargetFrameRate = 120;
        private static FpsProfiler _instance;

        [Header("Frame rate: Frame per second")] [Header("Frame timing: Frame timing (ms)")] [Header("=> Time.unscaledDeltaTime")] [Header("Memory consumption rate: Random Access Memory")] [SuffixLabel("~ Exhale_Close")] [LabelText("Display")] [SerializeField]
        private bool _enableDebug = true;

        [SuffixLabel("LeftShift ~ Switch")] [LabelText("Chinese")] [SerializeField]
        private bool _enableChinese = true;

        [SuffixLabel("LeftControl ~ Display_Hide")] [LabelText("Frame")] [SerializeField]
        private bool _enableBox = true;

        [LabelText("Maximum memory consumption rate")] [SerializeField]
        private float _maxMemory = 2.5f;

        private PerformanceData _budget;
        private float _factor;
        private PerformanceData _fps;
        private PerformanceData _frameTiming;
        private Rect _outerControlRect;
        private float _previousUpdateTime;
        private GUISkin _skin;
        private long _systemTotalMemory;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _systemTotalMemory = SystemInfo.systemMemorySize * 1024L * 1024L;
            _fps = new PerformanceData(0f, 0f, 0f, TargetFrameRate);
            _frameTiming = new PerformanceData(0f, 0f, 0f, 1.0f / _fps.Target);
            _budget = new PerformanceData(0f, 0f, 0f, 0f);
            _factor = _frameTiming.Target / TimeToConverge;
        }

        private void Update()
        {
            OnInputUpdate();
            if (Time.time < StartDelay)
                return;
            OnUpdate();
        }

        private void OnGUI()
        {
            if (!_enableDebug)
                return;
            SetupGUISkin();
            DrawPrimary();
        }

        private void OnInputUpdate()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote) || Input.inputString == "·")
            {
                var shift = Input.GetKey(KeyCode.LeftShift);
                var control = Input.GetKey(KeyCode.LeftControl);
                if (!shift && !control)
                    _enableDebug = !_enableDebug;
                else if (shift)
                    _enableChinese = !_enableChinese;
                else
                    _enableBox = !_enableBox;
            }
        }

        private void OnUpdate()
        {
            if (!_enableDebug)
                return;
            UpdateFrameTiming();
            UpdateFrameRate();
            UpdateBudget();
            UpdateDisplayValues();
        }

        private void UpdateDisplayValues()
        {
            if (Time.unscaledTime < _previousUpdateTime)
                return;
            _frameTiming.Display = _frameTiming.Live;
            _fps.Display = _fps.Live;
            _budget.Display = _budget.Live;
            _previousUpdateTime = Time.unscaledTime + TimeBetweenDisplayUpdates;
        }

        private void UpdateFrameTiming()
        {
            _frameTiming.Live = Time.unscaledDeltaTime;
            _frameTiming.Average = Mathf.Lerp(_frameTiming.Average, _frameTiming.Live, _factor);
        }

        private void UpdateFrameRate()
        {
            _fps.Live = 1.0f / _frameTiming.Live;
            _fps.Average = 1.0f / _frameTiming.Average;
        }

        private void UpdateBudget()
        {
            var totalAllocatedMemory = Profiler.GetTotalAllocatedMemoryLong();
            var memoryUsagePercentage = (float)totalAllocatedMemory / _systemTotalMemory * 100f;
            _budget.Live = memoryUsagePercentage;
            _budget.Average = Mathf.Lerp(_budget.Average, _budget.Live, _factor);
        }

        private void DrawPrimary()
        {
            if (_enableBox)
                GUI.Box(new Rect(Screen.width - 320, 45, 255, 122), "");
            GUILayout.BeginArea(_outerControlRect);
            GUILayout.BeginArea(new Rect(0, 0, 100, _outerControlRect.height));
            GUILayout.Label("");
            string fps;
            string ms;
            string budget;
            string live;
            string avg;
            if (_enableChinese)
            {
                fps = "Frame rate";
                ms = "Frame time";
                budget = "Memory";
                live = "Real time";
                avg = "Average";
            }
            else
            {
                fps = "FPS";
                ms = "FT";
                budget = "RAM";
                live = "Live";
                avg = "Avg";
            }

            DrawTextOutlined($"{fps}", GetParagraphColor());
            DrawTextOutlined($"{ms}", GetParagraphColor());
            DrawTextOutlined($"% {budget}", GetParagraphColor());
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(100, 0, 80, _outerControlRect.height));
            DrawTextOutlined($"{live}", Color.white);
            DrawTextOutlined($"{_fps.Display:0.0}", GetConditionalColor(_fps.Display, _fps.Target));
            DrawTextOutlined($"{_frameTiming.Display * SecondsToMS:0.0}", GetConditionalColor(_frameTiming.Target, _frameTiming.Display));
            DrawTextOutlined($"{_budget.Display:0.00}", GetMemoryColor(_budget.Display));
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(180, 0, 80, _outerControlRect.height));
            DrawTextOutlined($"{avg}", Color.white);
            DrawTextOutlined($"{_fps.Average:0.0}", GetConditionalColor(_fps.Average, _fps.Target));
            DrawTextOutlined($"{_frameTiming.Average * SecondsToMS:0.0}", GetConditionalColor(_frameTiming.Target, _frameTiming.Average));
            DrawTextOutlined($"{_budget.Average:0.00}", GetMemoryColor(_budget.Average));
            GUILayout.EndArea();
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(_outerControlRect.x, _outerControlRect.y + _outerControlRect.height, _outerControlRect.width, _outerControlRect.height));
            GUILayout.EndArea();
        }

        private void DrawTextOutlined(string text, Color color)
        {
            var rect = GUILayoutUtility.GetRect(new GUIContent(text), _skin.label);
            rect.y += 1;
            rect.x += 1;
            _skin.label.normal.textColor = Color.black;
            _skin.label.hover = _skin.label.normal;
            GUI.Label(rect, text);
            rect.y -= 1;
            rect.x -= 1;
            _skin.label.normal.textColor = color;
            _skin.label.hover = _skin.label.normal;
            GUI.Label(rect, text);
        }

        private static Color GetConditionalColor(float a, float b)
        {
            if (a < b * 0.9f)
                return Color.red;
            if (a < b)
                return Color.yellow;
            return Color.green;
        }

        private Color GetMemoryColor(float a)
        {
            if (a > _maxMemory)
                return Color.red;
            if (a > _maxMemory * 0.8f)
                return Color.yellow;
            return Color.green;
        }

        private static Color GetParagraphColor() => new(0.8f, 0.8f, 0.8f, 1f);

        private void SetupGUISkin()
        {
            if (_skin == null)
                _skin = Instantiate(GUI.skin);
            _skin.label.fontStyle = FontStyle.Bold;
            _skin.label.alignment = TextAnchor.MiddleRight;
            _skin.label.fontSize = 16;
            _outerControlRect = new Rect(Screen.width - 350, 50, 300, 120);
            GUI.skin = _skin;
        }

        private struct PerformanceData
        {
            public float Live;
            public float Average;
            public float Display;
            public readonly float Target;

            public PerformanceData(float live, float average, float display, float target)
            {
                Live = live;
                Average = average;
                Display = display;
                Target = target;
            }
        }
    }
}