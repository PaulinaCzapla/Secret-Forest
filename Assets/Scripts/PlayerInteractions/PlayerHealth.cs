using DebugTools;
using Glades;
using PlayerInteractions.StaticEvents;
using UI.Events;
using UnityEngine;

namespace PlayerInteractions
{
    public class PlayerHealth: MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerStats;
        
        private void OnEnable()
        {
            PlayerStatsStaticEvents.SubscribeToHealthValueChanged(HealthChanged);
            PlayerMovementStaticEvents.SubscribeToPlayerMovedToGlade(OnPlayerMoved);
        }

        private void OnDisable()
        {
            PlayerStatsStaticEvents.UnsubscribeFromHealthValueChanged(HealthChanged);
            PlayerMovementStaticEvents.UnsubscribeFromPlayerMovedToGlade(OnPlayerMoved);
        }

        private void OnPlayerMoved(SpawnedGlade glade)
        {
            HealthChanged(playerStats.healthRestoredPerGlade);
        }

        private void HealthChanged(float value)
        {
            playerStats.currentHealthValue = Mathf.Clamp(playerStats.currentHealthValue + value, 0,
                playerStats.currentMaxHealthValue);
            
            UIStaticEvents.InvokeUpdateHealthUI();

            if (playerStats.currentHealthValue == 0)
                DebugMessageSender.SendDebugMessage("Player died");
        }
    }
}