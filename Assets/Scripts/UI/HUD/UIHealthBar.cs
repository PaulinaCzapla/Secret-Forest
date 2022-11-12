using DG.Tweening;
using PlayerInteractions;
using UI.Events;
using UnityEngine;
using ValueRepresentation;

namespace UI.HUD
{
    public class UIHealthBar : UIBar
    {
        [SerializeField] private PlayerStatsSO stats;
        private float _prevValue;
        private bool _fistInit = true;
        
        private void OnEnable()
        {
           
            UIStaticEvents.SubscribeToUpdateHealthUI(Refresh);
        }

        private void OnDisable()
        {
            //UIStaticEvents.UnsubscribeFromUpdateHealthUI(Refresh);
        }

        protected override void Refresh()
        {
            if (Mathf.Abs(_prevValue - stats.currentHealthValue) >= 1 || _fistInit)
            {
                slider.value = stats.currentHealthValue / stats.currentMaxHealthValue;
                valueText.text = ValueRounder.RoundUp(stats.currentHealthValue).ToString("F0");
                _prevValue = stats.currentHealthValue;
                _fistInit = false;
                AnimateText();
            }
        }
    }
}