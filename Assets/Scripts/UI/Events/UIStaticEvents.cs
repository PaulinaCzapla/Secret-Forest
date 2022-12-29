using Glades;
using UnityEngine.Events;

namespace UI.Events
{
    /// <summary>
    /// A static class that implements events that are used to manage UI.
    /// </summary>
    public static class UIStaticEvents
    {
        /// <summary>
        /// Event that is invoked when hunger UI need refresh.
        /// </summary>
        private static UnityEvent onUpdateHungerUI = new UnityEvent();
        /// <summary>
        /// Event that is invoked when health UI needs refresh.
        /// </summary>
        private static UnityEvent onUpdateHealthUI = new UnityEvent();

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
        
    }
}