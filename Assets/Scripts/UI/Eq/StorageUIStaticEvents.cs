using System.Collections.Generic;
using Glades;
using InteractableItems.CollectableItems;
using UnityEngine.Events;

namespace UI.Eq
{
    public static class StorageUIStaticEvents
    {
        private static UnityEvent onRefreshUI = new UnityEvent();

        public static void SubscribeToRefreshUI(UnityAction subscriber) =>
            onRefreshUI.AddListener(subscriber);
        public static void UnsubscribeFromRefreshUI(UnityAction subscriber) =>
            onRefreshUI.RemoveListener(subscriber);
        public static void InvokeRefreshUI() => onRefreshUI?.Invoke();
    }
}