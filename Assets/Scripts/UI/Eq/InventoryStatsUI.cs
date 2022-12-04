using System;
using PlayerInteractions;
using TMPro;
using UnityEngine;

namespace UI.Eq
{
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