using CameraManagement;
using Glades;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInteractions.StaticEvents
{
    /// <summary>
    /// A class that contains static events that are used for player movement functionality.
    /// </summary>
    public static class PlayerMovementStaticEvents
    {
        /// <summary>
        /// Invoked when the player try to move to given glade.
        /// </summary>
        private static UnityEvent<SpawnedGlade, bool> onTryMovePlayerToGlade = new UnityEvent<SpawnedGlade, bool>();
        /// <summary>
        /// Invoked when the played moved to the given glade.
        /// </summary>
        private static UnityEvent<SpawnedGlade> onPlayerMovedToGlade = new UnityEvent<SpawnedGlade>();
        
        public static void SubscribeToTryMovePlayerToGlade (UnityAction<SpawnedGlade, bool> subscriber) =>
            onTryMovePlayerToGlade.AddListener(subscriber);
        public static void UnsubscribeFromTryMovePlayerToGlade  (UnityAction<SpawnedGlade, bool> subscriber) =>
            onTryMovePlayerToGlade.RemoveListener(subscriber);
        public static void InvokeTryMovePlayerToPosition (SpawnedGlade glade, bool forced = false) => onTryMovePlayerToGlade.Invoke(glade, forced);
        
        public static void SubscribeToPlayerMovedToGlade  (UnityAction<SpawnedGlade> subscriber) =>
            onPlayerMovedToGlade .AddListener(subscriber);
        public static void UnsubscribeFromPlayerMovedToGlade   (UnityAction<SpawnedGlade> subscriber) =>
            onPlayerMovedToGlade .RemoveListener(subscriber);
        public static void InvokePlayerMovedToGlade  (SpawnedGlade glade) =>  onPlayerMovedToGlade .Invoke(glade);

    }
}