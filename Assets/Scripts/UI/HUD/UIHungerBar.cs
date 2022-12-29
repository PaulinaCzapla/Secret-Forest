using System;
using DG.Tweening;
using PlayerInteractions;
using TMPro;
using UI.Events;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ValueRepresentation;

namespace UI.HUD
{
    /// <summary>
    /// A class that represents player's hunger bar.
    /// </summary>
    public class UIHungerBar : UIBar
    {
        [SerializeField] private PlayerStatsSO stats;

        private float _prevValue;
        private bool _fistInit = true;
        
        private void OnEnable()
        {
            _prevValue = stats.currentHungerValue;
            UIStaticEvents.SubscribeToUpdateHungerUI(Refresh);
        }

        private void OnDisable()
        {
            UIStaticEvents.UnsubscribeFromUpdateHungerUI(Refresh);
        }

        /// <summary>
        /// Refreshes the bar values.
        /// </summary>
        protected override void Refresh()
        {
            if (Mathf.Abs(_prevValue - Mathf.Abs(stats.currentHungerValue)) >= 1 || _fistInit || Mathf.Approximately(stats.currentHungerValue,0))
            {
                slider.value = stats.currentHungerValue / stats.currentMaxHungerValue;
                valueText.text = ValueRounder.RoundUp(stats.currentHungerValue).ToString("F0") + "<size=70%>/" + stats.currentMaxHungerValue;
                _prevValue = stats.currentHungerValue;
                _fistInit = false;
                AnimateText();
            }
        }
    }
}