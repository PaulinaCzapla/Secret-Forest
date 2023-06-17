using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// A DOTween utility class that implements methods that enable animations for Light2D component.
    /// </summary>
    public static class DoTweenUtility
    {
        /// <summary>
        /// Animates intensity of the Light2D component to given value in a given time.
        /// </summary>
        public static Tweener DOIntensity(this UnityEngine.Rendering.Universal.Light2D target, float endValue, float duration)
        {
            return DOTween.To(() => target.intensity, x => target.intensity = x, endValue, duration)
                .SetTarget(target);
        }
        /// <summary>
        /// Animates inner radius of the Light2D component to given value in a given time.
        /// </summary>
        public static Tweener DOInnerRadius(this UnityEngine.Rendering.Universal.Light2D target, float endValue, float duration)
        {
            endValue = Mathf.Max(0, endValue);
            return DOTween.To(() => target.pointLightInnerRadius, x => target.pointLightInnerRadius = x, endValue, duration)
                .SetTarget(target);
        }
        /// <summary>
        /// Animates outer radius of the Light2D component to given value in a given time.
        /// </summary>
        public static Tweener DOOuterRadius(this UnityEngine.Rendering.Universal.Light2D target, float endValue, float duration)
        {        
            endValue = Mathf.Max(0, endValue);
            return DOTween.To(() => target.pointLightOuterRadius, x => target.pointLightOuterRadius = x, endValue, duration)
                .SetTarget(target);
        }
    }
}
