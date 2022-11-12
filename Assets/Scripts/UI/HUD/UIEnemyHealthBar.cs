using System;
using CombatSystem;
using UI.Events;
using UnityEngine;
using ValueRepresentation;

namespace UI.HUD
{
    public class UIEnemyHealthBar : UIBar
    {
        private float _prevValue;
        private bool _fistInit = true;
        
        private void OnEnable()
        {
            StaticCombatEvents.SubscribeToUpdateEnemyHealthUI(UpdateBar);
        }


        private void OnDisable()
        {
            StaticCombatEvents.UnsubscribeFromUpdateEnemyHealthUI(UpdateBar);
        }

        private void UpdateBar(float current, float max)
        {
            slider.value = current/ max;
                valueText.text = ValueRounder.RoundUp(current).ToString("F0");
                _prevValue = current;
                _fistInit = false;
                AnimateText();
        }

        protected override void Refresh()
        {
           
        }
    }
}