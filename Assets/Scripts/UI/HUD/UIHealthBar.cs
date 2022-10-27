using PlayerInteractions;
using UI.Events;
using UnityEngine;

namespace UI.HUD
{
    public class UIHealthBar : UIBar
    {
        [SerializeField] private PlayerStatsSO stats;

        private float _textInitialFontSize;
        private void OnEnable()
        {
            _textInitialFontSize = valueText.fontSize;
            UIStaticEvents.SubscribeToUpdateHealthUI(Refresh);
        }

        private void OnDisable()
        {
            UIStaticEvents.UnsubscribeFromUpdateHealthUI(Refresh);
        }

        protected override void Refresh()
        {
            slider.value = stats.currentHealthValue / stats.currentMaxHealthValue;
            valueText.text = stats.currentHealthValue.ToString();
           // valueText.DO
        }
    }
}