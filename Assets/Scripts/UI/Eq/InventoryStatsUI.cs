using System;
using PlayerInteractions;
using TMPro;
using UI.Events;
using UnityEngine;

namespace UI.Eq
{
    /// <summary>
    /// A class that represents stats section in the inventory. It displays current player's stats.
    /// </summary>
    public class InventoryStatsUI : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerStats;
        [SerializeField] private TextMeshProUGUI damageBow;
        [SerializeField] private TextMeshProUGUI damageSword;
        [SerializeField] private TextMeshProUGUI criticalBow;
        [SerializeField] private TextMeshProUGUI criticalSword;
        [SerializeField] private TextMeshProUGUI defense;
        [SerializeField] private TextMeshProUGUI dodge;

        private void OnEnable()
        {
            InventoryUIStaticEvents.SubscribeToRefreshInventoryStatsUI(RefreshEqStats);
        }

        private void OnDisable()
        {
            InventoryUIStaticEvents.UnsubscribeFromRefreshInventoryStatsUI(RefreshEqStats);
        }

        /// <summary>
        /// Sets current player's stats in UI.
        /// </summary>
        private void RefreshEqStats()
        {
            damageBow.text = playerStats.CurrentBowDamage.ToString();
            damageSword.text = playerStats.CurrentSwordDamage.ToString();

            criticalBow.text = playerStats.CurrentCriticalBow.ToString() + "%";
            criticalSword.text = playerStats.CurrentCriticalSword.ToString() + "%";

            defense.text = playerStats.CurrentDefense.ToString();
            dodge.text = playerStats.CurrentDodgeChance.ToString() + "%";
        }
    }
}