using System;
using DG.Tweening;
using PlayerInteractions;
using TMPro;
using UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class UIHungerBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private PlayerStatsSO stats;

        private void OnEnable()
        {
            UIStaticEvents.SubscribeToUpdateHungerUI(Refresh);
        }

        private void OnDisable()
        {
            UIStaticEvents.UnsubscribeFromUpdateHungerUI(Refresh);
        }

        private void Refresh()
        {
            slider.value = stats.currentHungerValue / stats.currentMaxHungerValue;
            valueText.text = stats.currentHungerValue.ToString();

          //  slider.DOValue(newvalue, 0.12f).SetEase(Ease.InQuint);
        }
    }
}