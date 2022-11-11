using Glades;
using UnityEngine.Events;

namespace UI.Events
{
    public static class UIStaticEvents
    {
        private static UnityEvent onUpdateHungerUI = new UnityEvent();
        private static UnityEvent onUpdateHealthUI = new UnityEvent();
        private static UnityEvent<bool> onToggleCombatUI = new UnityEvent<bool>();

        public static void SubscribeToUpdateHungerUI(UnityAction subscriber) =>
            onUpdateHungerUI.AddListener(subscriber);

        public static void UnsubscribeFromUpdateHungerUI(UnityAction subscriber) =>
            onUpdateHungerUI.RemoveListener(subscriber);

        public static void InvokeUpdateHungerUI() => onUpdateHungerUI.Invoke();

        public static void SubscribeToUpdateHealthUI(UnityAction subscriber) =>
            onUpdateHealthUI.AddListener(subscriber);

        public static void UnsubscribeFromUpdateHealthUI(UnityAction subscriber) =>
            onUpdateHealthUI.RemoveListener(subscriber);

        public static void InvokeUpdateHealthUI() => onUpdateHealthUI.Invoke();

        public static void SubscribeToToggleCombatUI(UnityAction<bool> subscriber) =>
            onToggleCombatUI.AddListener(subscriber);

        public static void UnsubscribeFromToggleCombatUI(UnityAction<bool> subscriber) =>
            onToggleCombatUI.RemoveListener(subscriber);

        public static void InvokeToggleCombatUI(bool isOn) => onToggleCombatUI.Invoke(isOn);
    }
}