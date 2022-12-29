using System;
using DG.Tweening;
using UnityEngine;

namespace Glades
{
    /// <summary>
    /// Represents a mask that covers a glade. 
    /// </summary>
    public class Mask : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer maskSurface;
        [SerializeField] private SpriteRenderer mask;
        [SerializeField] private SpriteRenderer frameRenderer;
        [SerializeField] private GameObject fireflies;

        private Color _initialFrameColor;
        private bool _initialized;

        private void Awake()
        {
            if (!_initialized)
                _initialFrameColor = frameRenderer.color;
            _initialized = true;
        }

        /// <summary>
        /// Animated mask deactivation.
        /// </summary>
        public void DeactivateMask()
        {
            if (mask.color.a == 0)
                return;

            maskSurface.DOFade(0, 0.25f);

            Sequence s = DOTween.Sequence()
                .AppendCallback(() => frameRenderer.color = _initialFrameColor)
                .AppendCallback(() => fireflies.SetActive(false))
                .Append(mask.DOFade(0, 0.4f));
        }

        /// <summary>
        /// Shows gray glade frame.
        /// </summary>
        public void ShowFrame()
        {
            if (maskSurface.color.a == 0)
                return;

            maskSurface.DOFade(0, 0.25f);
            fireflies.SetActive(true);
        }
        /// <summary>
        /// Sets a mask that covers glade and a frame. 
        /// </summary>
        public void SetMask()
        {
            if (!_initialized)
            {
                _initialFrameColor = frameRenderer.color;
                _initialized = true;
            }

            frameRenderer.color = new Color(0.1886792f, 0.1886792f, 0.1886792f);
            maskSurface.color = Color.white;
            mask.color = Color.white;
            fireflies.SetActive(false);
        }
    }
}