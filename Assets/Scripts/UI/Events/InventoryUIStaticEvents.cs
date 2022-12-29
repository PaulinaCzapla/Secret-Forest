using UnityEngine.Events;

namespace UI.Events
{
    /// <summary>
    /// A static class that implements events that are used to manage inventory.
    /// </summary>
    public static class InventoryUIStaticEvents
    {
        /// <summary>
        /// Event that is invoked when player stats in inventory need refresh.
        /// </summary>
        private static UnityEvent onRefreshInventoryStatsUI = new UnityEvent();

        public static void SubscribeToRefreshInventoryStatsUI(UnityAction subscriber) =>
            onRefreshInventoryStatsUI.AddListener(subscriber);
        public static void UnsubscribeFromRefreshInventoryStatsUI(UnityAction subscriber) =>
            onRefreshInventoryStatsUI.RemoveListener(subscriber);
        public static void InvokeRefreshInventoryStatsUI() => onRefreshInventoryStatsUI?.Invoke();
    }
}