using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
    public readonly struct DOTweenMath
    {
        public static TweenerCore<float, float, FloatOptions> DOTimeScale(float endValue, float duration)
        {
            var t = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, endValue, duration);
            return t;
        }

        public static TweenerCore<float, float, FloatOptions> DOLerp(float startValue, float endValue, float duration, Action<float> onUpdate)
        {
            var t = DOTween.To(() => startValue, x => startValue = x, endValue, duration).OnUpdate(() => onUpdate(startValue));
            return t;
        }
    }
}