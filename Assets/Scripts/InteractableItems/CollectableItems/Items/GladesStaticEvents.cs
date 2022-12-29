using System.Collections.Generic;
using CombatSystem;
using Glades;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items
{
    /// <summary>
    /// A class contains events that are related to glades management.
    /// </summary>
    public static class GladesStaticEvents
    {
        /// <summary>
        /// Invoked when several glades should be unlocked/made visible.
        /// </summary>
        private static UnityEvent<List<SpawnedGlade>> onUnlockGlades = new UnityEvent<List<SpawnedGlade>>();
        public static void SubscribeToUnlockGlades  (UnityAction<List<SpawnedGlade>> subscriber) =>
            onUnlockGlades.AddListener(subscriber);
        public static void UnsubscribeFromUnlockGlades  (UnityAction<List<SpawnedGlade>> subscriber) =>
            onUnlockGlades.RemoveListener(subscriber);
        public static void InvokeUnlockGlades  (List<SpawnedGlade> glades) => onUnlockGlades?.Invoke(glades);

    }
}