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
                HungerChanged(-playerStats.hungerLostPerMinute);
                _cooldown.StartCooldown();
            }
        }

        private void OnPlayerMoved(SpawnedGlade glade)
        {
            HungerChanged(-playerStats.hungerLostPerGlade);
        }

        private void HungerChanged(float value)
        {
            playerStats.currentHungerValue = Mathf.Clamp(playerStats.currentHungerValue + value, 0,
                playerStats.currentMaxHungerValue);
            
            UIStaticEvents.InvokeUpdateHungerUI();

            if (playerStats.currentHungerValue == 0)
                DebugMessageSender.SendDebugMessage("Player died from hunger");
        }
    }
}