using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// A DOTween utility class that implements methods that enable animations for Light2D.
    /// </summary>
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
