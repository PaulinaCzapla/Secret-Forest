using CameraManagement;
using Glades;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInteractions.StaticEvents
{
    public static class PlayerMovementStaticEvents
    {
        private static UnityEvent<SpawnedGlade, bool> onTryMovePlayerToGlade = new UnityEvent<SpawnedGlade, bool>();

        public static void SubscribeToTryMovePlayerToGlade (UnityAction<SpawnedGlade, bool> subscriber) =>
            onTryMovePlayerToGlade.AddListener(subscriber);

        public static void UnsubscribeFromTryMovePlayerToGlade  (UnityAction<SpawnedGlade, bool> subscriber) =>
            onTryMovePlayerToGlade.RemoveListener(subscriber);

        public static void InvokeTryMovePlayerToPosition (SpawnedGlade glade, bool forced = false) => onTryMovePlayerToGlade.Invoke(glade, forced);
        
    }
}