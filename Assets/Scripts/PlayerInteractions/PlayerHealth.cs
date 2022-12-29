using DebugTools;
using Glades;
using PlayerInteractions.StaticEvents;
using UI.Events;
using UnityEngine;

namespace PlayerInteractions
{
    /// <summary>
    /// Represents player's health.
    /// </summary>
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

        /// <summary>
        /// Restores a small amount of heath every time player moves.
        /// </summary>
        private void OnPlayerMoved(SpawnedGlade glade)
        {
            if (playerStats.currentHungerValue > 0)
                HealthChanged(playerStats.HealthRestoredPerGlade);
            else
                HealthChanged(- playerStats.HealthLostPerGladeWhenHungry);
        }

        /// <summary>
        /// Changes a current health value
        /// </summary>
        /// <param name="value"> Additional value. </param>
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