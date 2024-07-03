using DG.Tweening.Core;
using DG.Tweening.CustomPlugins;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
    public static partial class DOTweenExtensions
    {
        public static TweenerCore<T1, T2, TPlugOptions> SetSwing<T1, T2, TPlugOptions>(this TweenerCore<T1, T2, TPlugOptions> tweenerCore) where TPlugOptions : struct, IPlugOptions
        {
            tweenerCore.SetLoops(2, LoopType.Yoyo);
            return tweenerCore;
        }

        public static TweenerCore<Color, Color, ColorOptions> DOSwingColor(this SpriteRenderer target, Color endValue, float duration)
        {
            var t = DOTween.To(() => target.color, x => target.color = x, endValue, duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Color, Color, ColorOptions> DOSwingFade(this SpriteRenderer target, float endValue, float duration)
        {
            var t = DOTween.ToAlpha(() => target.color, x => target.color = x, endValue, duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingX(this Transform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.position, x => target.position = x, new Vector3(endValue, 0.0f, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.X, snapping).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingY(this Transform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.position, x => target.position = x, new Vector3(0.0f, endValue, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingZ(this Transform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.position, x => target.position = x, new Vector3(0.0f, 0.0f, endValue), duration / 2f);
            t.SetOptions(AxisConstraint.Z, snapping).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalSwingX(this Transform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.localPosition, x => target.localPosition = x, new Vector3(endValue, 0.0f, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.X, snapping).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalSwingY(this Transform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.localPosition, x => target.localPosition = x, new Vector3(0.0f, endValue, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalSwingZ(this Transform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.localPosition, x => target.localPosition = x, new Vector3(0.0f, 0.0f, endValue), duration / 2f);
            t.SetOptions(AxisConstraint.Z, snapping).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOSwingRotate(this Transform target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
        {
            var t = DOTween.To(() => target.rotation, x => target.rotation = x, endValue, duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            t.plugOptions.rotateMode = mode;
            return t;
        }

        public static TweenerCore<Quaternion, Quaternion, NoOptions> DOSwingRotateQuaternion(this Transform target, Quaternion endValue, float duration)
        {
            var t = DOTween.To(PureQuaternionPlugin.Plug(), () => target.rotation, x => target.rotation = x, endValue, duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Quaternion, Vector3, QuaternionOptions> DOLocalSwingRotate(this Transform target, Vector3 endValue, float duration, RotateMode mode = RotateMode.Fast)
        {
            var t = DOTween.To(() => target.localRotation, x => target.localRotation = x, endValue, duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            t.plugOptions.rotateMode = mode;
            return t;
        }

        public static TweenerCore<Quaternion, Quaternion, NoOptions> DOLocalSwingRotateQuaternion(this Transform target, Quaternion endValue, float duration)
        {
            var t = DOTween.To(PureQuaternionPlugin.Plug(), () => target.localRotation, x => target.localRotation = x, endValue, duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingScale(this Transform target, Vector3 endValue, float duration)
        {
            var t = DOTween.To(() => target.localScale, x => target.localScale = x, endValue, duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingScale(this Transform target, float endValue, float duration)
        {
            var t = DOTween.To(() => target.localScale, x => target.localScale = x, new Vector3(endValue, endValue, endValue), duration / 2f);
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingScaleX(this Transform target, float endValue, float duration)
        {
            var t = DOTween.To(() => target.localScale, x => target.localScale = x, new Vector3(endValue, 0.0f, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.X).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingScaleY(this Transform target, float endValue, float duration)
        {
            var t = DOTween.To(() => target.localScale, x => target.localScale = x, new Vector3(0.0f, endValue, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.Y).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOSwingScaleZ(this Transform target, float endValue, float duration)
        {
            var t = DOTween.To(() => target.localScale, x => target.localScale = x, new Vector3(0.0f, 0.0f, endValue), duration / 2f);
            t.SetOptions(AxisConstraint.Z).SetTarget(target).SetLoops(2, LoopType.Yoyo);
            return t;
        }
    }
}