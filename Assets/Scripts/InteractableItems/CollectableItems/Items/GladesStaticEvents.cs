using System.Collections.Generic;
using CombatSystem;
using Glades;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items
{
    public static class GladesStaticEvents
    {
        private static UnityEvent<List<SpawnedGlade>> onUnlockGlades = new UnityEvent<List<SpawnedGlade>>();
        public static void SubscribeToUnlockGlades  (UnityAction<List<SpawnedGlade>> subscriber) =>
            onUnlockGlades.AddListener(subscriber);
        public static void UnsubscribeFromUnlockGlades  (UnityAction<List<SpawnedGlade>> subscriber) =>
            onUnlockGlades.RemoveListener(subscriber);
        public static void InvokeUnlockGlades  (List<SpawnedGlade> glades) => onUnlockGlades?.Invoke(glades);

    }
}