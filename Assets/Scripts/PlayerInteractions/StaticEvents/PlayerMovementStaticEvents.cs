using CameraManagement;
using Glades;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerInteractions.StaticEvents
{
    public static class PlayerMovementStaticEvents
    {
        private static UnityEvent<SpawnedGlade> onTryMovePlayerToGlade = new UnityEvent<SpawnedGlade>();
        private static UnityEvent<Vector2> onMovePlayerToPosition = new UnityEvent<Vector2>();
        
        public static void SubscribeToTryMovePlayerToGlade (UnityAction<SpawnedGlade> subscriber) =>
            onTryMovePlayerToGlade.AddListener(subscriber);

        public static void UnsubscribeFromTryMovePlayerToGlade  (UnityAction<SpawnedGlade> subscriber) =>
            onTryMovePlayerToGlade.RemoveListener(subscriber);

        public static void InvokeTryMovePlayerToPosition (SpawnedGlade glade) => onTryMovePlayerToGlade.Invoke(glade);
        
        public static void SubscribeToMovePlayerToPosition (UnityAction<Vector2> subscriber) =>
            onMovePlayerToPosition.AddListener(subscriber);

        public static void UnsubscribeFromMovePlayerToPosition (UnityAction<Vector2> subscriber) =>
            onMovePlayerToPosition.RemoveListener(subscriber);

        public static void InvokeMovePlayerToPosition (Vector2 position) => onMovePlayerToPosition.Invoke(position);
    }
}