using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public static class DoTweenUtility
    {
        public static Tweener DOIntensity(this UnityEngine.Rendering.Universal.Light2D target, float endValue, float duration)
        {
            return DOTween.To(() => target.intensity, x => target.intensity = x, endValue, duration)
                .SetTarget(target);
        }

        public static Tweener DOInnerRadius(this UnityEngine.Rendering.Universal.Light2D target, float endValue, float duration)
        {
            endValue = Mathf.Max(0, endValue);
            return DOTween.To(() => target.pointLightInnerRadius, x => target.pointLightInnerRadius = x, endValue, duration)
                .SetTarget(target);
        }

        public static Tweener DOOuterRadius(this UnityEngine.Rendering.Universal.Light2D target, float endValue, float duration)
        {        
            endValue = Mathf.Max(0, endValue);
            return DOTween.To(() => target.pointLightOuterRadius, x => target.pointLightOuterRadius = x, endValue, duration)
                .SetTarget(target);
        }
    }
}
