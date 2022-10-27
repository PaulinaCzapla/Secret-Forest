using System;
using DG.Tweening;
using PlayerInteractions;
using TMPro;
using UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class UIHungerBar : UIBar
    {
        [SerializeField] private PlayerStatsSO stats;
        private float _textInitialFontSize;
        private void OnEnable()
        {
            _textInitialFontSize = valueText.fontSize;
            UIStaticEvents.SubscribeToUpdateHungerUI(Refresh);
        }

        private void OnDisable()
        {
            UIStaticEvents.UnsubscribeFromUpdateHungerUI(Refresh);
        }

        protected override void Refresh()
        {
            slider.value = stats.currentHungerValue / stats.currentMaxHungerValue;
            valueText.text = stats.currentHungerValue.ToString();
            // valueText.DO
            // var s = DOTween.Sequence();
            // s.Append(valueText)
            //  slider.DOValue(newvalue, 0.12f).SetEase(Ease.InQuint);
        }
    }
}