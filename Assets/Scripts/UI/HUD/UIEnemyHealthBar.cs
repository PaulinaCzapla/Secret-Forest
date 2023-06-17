using System;
using CombatSystem;
using UI.Events;
using UnityEngine;
using Utilities.ValueRepresentation;

namespace UI.HUD
{
    /// <summary>
    /// A class that represents enemy's health bar.
    /// </summary>
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

        /// <summary>
        /// Updates bar values.
        /// </summary>
        private void UpdateBar(float current, float max)
        {
            slider.value = current/ max;
                valueText.text = ValueRounder.RoundUp(current, 0.5f).ToString();
                _prevValue = current;
                _fistInit = false;
                AnimateText();
        }

        protected override void Refresh()
        {
           
        }
    }
}