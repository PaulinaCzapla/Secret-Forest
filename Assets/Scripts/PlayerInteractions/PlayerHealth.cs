using DebugTools;
using Glades;
using PlayerInteractions.StaticEvents;
using UI.Events;
using UnityEngine;

namespace PlayerInteractions
{
    public class PlayerHealth : MonoBehaviour
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
            if (playerStats.currentHungerValue > 0)
                HealthChanged(playerStats.HealthRestoredPerGlade);
            else
                HealthChanged(- playerStats.HealthLostPerGladeWhenHungry);
        }

        private void HealthChanged(float value)
        {
            playerStats.currentHealthValue = Mathf.Clamp(playerStats.currentHealthValue + value, 0,
                playerStats.currentMaxHealthValue);

            UIStaticEvents.InvokeUpdateHealthUI();

            if (playerStats.currentHealthValue <= 0)
                PlayerStatsStaticEvents.InvokePlayerDied();
        }
    }
}