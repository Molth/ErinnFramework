using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening
{
    public static partial class DOTweenExtensions
    {
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOJump(this Transform target, float endValue, float jumpPower, float duration, bool snapping = false)
        {
            var startPosY = target.position.y;
            var offsetY = endValue - startPosY;
            var t = DOTween.To(() => target.position, x => target.position = x, new Vector3(0.0f, jumpPower, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative();
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            t.OnUpdate(() =>
            {
                var position = target.position;
                position.y += DOVirtual.EasedValue(0.0f, offsetY, t.ElapsedPercentage(), Ease.OutQuad);
                target.position = position;
            });
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOJump(this Transform target, float endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
        {
            if (numJumps < 1)
                numJumps = 1;
            var startPosY = target.position.y;
            var offsetY = endValue - startPosY;
            var t = DOTween.To(() => target.position, x => target.position = x, new Vector3(0.0f, jumpPower, 0.0f), duration / (numJumps * 2f));
            t.SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative();
            t.SetTarget(target).SetLoops(numJumps * 2, LoopType.Yoyo);
            t.OnUpdate(() =>
            {
                var position = target.position;
                position.y += DOVirtual.EasedValue(0.0f, offsetY, t.ElapsedPercentage(), Ease.OutQuad);
                target.position = position;
            });
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalJump(this Transform target, float endValue, float jumpPower, float duration, bool snapping = false)
        {
            var startPosY = target.localPosition.y;
            var offsetY = endValue - startPosY;
            var t = DOTween.To(() => target.localPosition, x => target.localPosition = x, new Vector3(0.0f, jumpPower, 0.0f), duration / 2f);
            t.SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative();
            t.SetTarget(target).SetLoops(2, LoopType.Yoyo);
            t.OnUpdate(() =>
            {
                var localPosition = target.localPosition;
                localPosition.y += DOVirtual.EasedValue(0.0f, offsetY, t.ElapsedPercentage(), Ease.OutQuad);
                target.localPosition = localPosition;
            });
            return t;
        }

        public static TweenerCore<Vector3, Vector3, VectorOptions> DOLocalJump(this Transform target, float endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
        {
            if (numJumps < 1)
                numJumps = 1;
            var startPosY = target.localPosition.y;
            var offsetY = endValue - startPosY;
            var t = DOTween.To(() => target.localPosition, x => target.localPosition = x, new Vector3(0.0f, jumpPower, 0.0f), duration / (numJumps * 2f));
            t.SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad).SetRelative();
            t.SetTarget(target).SetLoops(numJumps * 2, LoopType.Yoyo);
            t.OnUpdate(() =>
            {
                var localPosition = target.localPosition;
                localPosition.y += DOVirtual.EasedValue(0.0f, offsetY, t.ElapsedPercentage(), Ease.OutQuad);
                target.localPosition = localPosition;
            });
            return t;
        }
    }
}