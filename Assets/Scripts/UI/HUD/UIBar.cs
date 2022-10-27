using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public abstract class UIBar : MonoBehaviour
    {
        [SerializeField] protected Slider slider;
        [SerializeField] protected TextMeshProUGUI valueText;
        private float _textInitialFontSize;

        private void Awake()
        {
            _textInitialFontSize = valueText.fontSize;
        }

        protected abstract void Refresh();

        protected void AnimateText()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(valueText.DOFontSize(_textInitialFontSize - 18, 0.15f))
                .Append(valueText.DOFontSize(_textInitialFontSize, 0.15f));
        }
    }
}