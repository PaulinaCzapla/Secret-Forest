using System.Collections.Generic;
using InteractableItems;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.InteractableItems;
using UnityEngine.Events;

namespace UI.StorageUI
{
    public static class ChestUIStaticEvents
    {
        private static UnityEvent<List<Item>,Chest > onOpenChest = new UnityEvent<List<Item>,Chest  > ();
        private static UnityEvent onCloseChest = new UnityEvent ();

        
        public static void SubscribeToOpenChest (UnityAction<List<Item>,Chest  >  subscriber) =>
            onOpenChest.AddListener(subscriber);
        public static void UnsubscribeFromOpenChest (UnityAction<List<Item >, Chest > subscriber) =>
            onOpenChest.RemoveListener(subscriber);
        public static void InvokeOpenChest (List<Item> items, Chest chest) => onOpenChest.Invoke(items, chest);
        

        public static void SubscribeToCloseChest (UnityAction  subscriber) =>
            onCloseChest.AddListener(subscriber);
        public static void UnsubscribeFromCloseChest (UnityAction subscriber) =>
            onCloseChest.RemoveListener(subscriber);
        public static void InvokeCloseChest ( ) => onCloseChest.Invoke( );
    }
}