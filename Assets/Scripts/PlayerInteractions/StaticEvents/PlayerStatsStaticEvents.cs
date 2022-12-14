using Glades;
using UnityEngine.Events;

namespace PlayerInteractions.StaticEvents
{
    /// <summary>
    /// A static class that is used for health and hunger functionalities.
    /// </summary>
    public static class PlayerStatsStaticEvents
    {
        /// <summary>
        /// Invoked when player's hunger values was changed.
        /// </summary>
        private static UnityEvent<float> onHungerValueChanged = new UnityEvent<float>();
        /// <summary>
        /// Invoked when player's health value was changed.
        /// </summary>
        private static UnityEvent<float> onHealthValueChanged = new UnityEvent<float>();
        /// <summary>
        /// Invoked when player dies.
        /// </summary>
        private static UnityEvent onPlayerDied = new UnityEvent();
        
        public static void SubscribeToHungerValueChanged (UnityAction<float> subscriber) =>
            onHungerValueChanged.AddListener(subscriber);
        public static void UnsubscribeFromHungerValueChanged (UnityAction<float> subscriber) =>
            onHungerValueChanged.RemoveListener(subscriber);
        public static void InvokeHungerValueChanged (float additionalValue) => onHungerValueChanged.Invoke(additionalValue);
        
        public static void SubscribeToHealthValueChanged (UnityAction<float> subscriber) =>
            onHealthValueChanged .AddListener(subscriber);
        public static void UnsubscribeFromHealthValueChanged(UnityAction<float> subscriber) =>
            onHealthValueChanged .RemoveListener(subscriber);
        public static void InvokeHealthValueChanged (float additionalValue) =>onHealthValueChanged .Invoke(additionalValue);

        public static void SubscribeToPlayerDied (UnityAction subscriber) =>
            onPlayerDied.AddListener(subscriber);
        public static void UnsubscribeFromPlayerDied(UnityAction subscriber) =>
            onPlayerDied .RemoveListener(subscriber);
        public static void InvokePlayerDied () =>onPlayerDied .Invoke();
    }
}