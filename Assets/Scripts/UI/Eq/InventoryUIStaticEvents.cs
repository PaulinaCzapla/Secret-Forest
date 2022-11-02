using System.Collections.Generic;
using Glades;
using InteractableItems.CollectableItems;
using UnityEngine.Events;

namespace UI.Eq
{
    public static class InventoryUIStaticEvents
    {
        private static UnityEvent onRefreshInventoryStatsUI = new UnityEvent();

        public static void SubscribeToRefreshInventoryStatsUI(UnityAction subscriber) =>
            onRefreshInventoryStatsUI.AddListener(subscriber);
        public static void UnsubscribeFromRefreshInventoryStatsUI(UnityAction subscriber) =>
            onRefreshInventoryStatsUI.RemoveListener(subscriber);
        public static void InvokeRefreshInventoryStatsUI() => onRefreshInventoryStatsUI?.Invoke();
    }
}