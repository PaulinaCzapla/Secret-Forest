using System;
using System.Threading;
using DebugTools;
using Glades;
using Glades.GladeTypes;
using PlayerInteractions.StaticEvents;
using Timers;
using UI.Events;
using UnityEngine;

namespace PlayerInteractions
{
    /// <summary>
    /// A class that represents player hunger.
    /// </summary>
    public class PlayerHunger : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerStats;

        private Cooldown _cooldown;

        private void Awake()
        {
            _cooldown = new Cooldown(60f);
        }

        private void OnEnable()
        {
            PlayerStatsStaticEvents.SubscribeToHungerValueChanged(HungerChanged);
            PlayerMovementStaticEvents.SubscribeToPlayerMovedToGlade(OnPlayerMoved);
        }

        private void OnDisable()
        {
            PlayerStatsStaticEvents.UnsubscribeFromHungerValueChanged(HungerChanged);
            PlayerMovementStaticEvents.UnsubscribeFromPlayerMovedToGlade(OnPlayerMoved);
        }

        private void Update()
        {
            if (_cooldown.CooldownEnded)
            {
                HungerChanged(-playerStats.HungerLostPerMinute);
                _cooldown.StartCooldown();
            }
        }

        /// <summary>
        /// Every time player moves, hunger value is decreased by a certain value.
        /// </summary>
        private void OnPlayerMoved(SpawnedGlade glade)
        {
            HungerChanged(-playerStats.HungerLostPerGlade);
        }

        /// <summary>
        /// Changes hunger value.
        /// </summary>
        /// <param name="value"> Additional value. </param>
        private void HungerChanged(float value)
        {
            playerStats.currentHungerValue = Mathf.Clamp(playerStats.currentHungerValue + value, 0,
                playerStats.currentMaxHungerValue);
            
            UIStaticEvents.InvokeUpdateHungerUI();
        }
    }
}