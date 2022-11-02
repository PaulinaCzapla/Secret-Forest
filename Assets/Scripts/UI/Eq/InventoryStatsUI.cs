using System;
using PlayerInteractions;
using TMPro;
using UnityEngine;

namespace UI.Eq
{
    public class InventoryStatsUI : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerStats;
        [SerializeField] private TextMeshProUGUI damage;
        [SerializeField] private TextMeshProUGUI critical;
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
            damage.text = playerStats.currentDamage.ToString();
            critical.text = playerStats.currentCritical.ToString()+ "%";
            defense.text = playerStats.currentDefense.ToString();
            dodge.text = playerStats.currentDodgeChance.ToString()+ "%";
        }
    }
}