using System.Drawing;
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
        private float _prevValue, _prevMaxValue;
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
            if (Mathf.Abs(_prevValue - Mathf.Abs(stats.currentHealthValue)) >= 0.5 || _fistInit || Mathf.Approximately(stats.currentHealthValue,0) 
                || !Mathf.Approximately(_prevMaxValue ,stats.currentMaxHealthValue))
            {
                slider.value = stats.currentHealthValue / stats.currentMaxHealthValue;
                valueText.text = ValueRounder.RoundUp(stats.currentHealthValue, 0.5f) + "<size=70%>/" + ValueRounder.RoundUp(stats.currentMaxHealthValue,0.5f)  ;
                _prevValue = stats.currentHealthValue;
                _fistInit = false;
                _prevMaxValue = stats.currentMaxHealthValue;
                AnimateText();
            }
        }
    }
}