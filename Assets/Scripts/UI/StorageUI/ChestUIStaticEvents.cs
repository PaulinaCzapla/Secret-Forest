using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine.Events;

namespace UI.StorageUI
{
    public static class ChestUIStaticEvents
    {
        private static UnityEvent<List<Item> > onOpenChest = new UnityEvent<List<Item> > ();
        private static UnityEvent onUpdateHealthUI = new UnityEvent();
        
        public static void SubscribeToOpenChest (UnityAction<List<Item> >  subscriber) =>
            onOpenChest.AddListener(subscriber);
        public static void UnsubscribeFromOpenChest (UnityAction<List<Item> >  subscriber) =>
            onOpenChest.RemoveListener(subscriber);
        public static void InvokeOpenChest (List<Item> items) => onOpenChest.Invoke(items);
        //
        // public static void SubscribeToUpdateHealthUI  (UnityAction subscriber) =>
        //     onUpdateHealthUI .AddListener(subscriber);
        // public static void UnsubscribeFromUpdateHealthUI   (UnityAction subscriber) =>
        //     onUpdateHealthUI .RemoveListener(subscriber);
        // public static void InvokeUpdateHealthUI  () =>  onUpdateHealthUI .Invoke();
    }
}