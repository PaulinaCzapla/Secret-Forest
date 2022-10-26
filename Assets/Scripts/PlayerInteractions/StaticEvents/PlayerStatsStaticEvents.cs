using Glades;
using UnityEngine.Events;

namespace PlayerInteractions.StaticEvents
{
    public class PlayerStatsStaticEvents
    {
        private static UnityEvent<float> onHungerValueChanged = new UnityEvent<float>();

        public static void SubscribeToHungerValueChanged (UnityAction<float> subscriber) =>
            onHungerValueChanged.AddListener(subscriber);

        public static void UnsubscribeFromHungerValueChanged (UnityAction<float> subscriber) =>
            onHungerValueChanged.RemoveListener(subscriber);

        public static void InvokeTryHungerValueChanged (float value) => onHungerValueChanged.Invoke(value);

    }
}