using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems;
using UnityEngine;

namespace UI.Eq
{
    public class StorageUI : MonoBehaviour
    {
        public static StorageUI Instance { get; private set; }

        [SerializeField] private GameObject storageObject;
        [SerializeField] private List<Transform> storageSlots;
        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            }
        }

        private List<Item> _storedItems;
        public void OpenStorage(List<Item> items)
        {
            storageObject.SetActive(true);
            int i = 0;

            foreach (var item in items)
            {
                item.gameObject.transform.SetParent(storageSlots[i]);
                storageSlots[i].gameObject.SetActive(true);
                item.gameObject.SetActive(true);
                i++;
            }

            _storedItems = items;
        }

        private void CloseStorage()
        {
            storageObject.SetActive(false);
            int i = 0;
            
            foreach (var item in _storedItems)
            {
                item.gameObject.transform.SetParent(null);
                storageSlots[i].gameObject.SetActive(false);
                item.gameObject.SetActive(false);
                i++;
            }

            _storedItems = null;
        }

        public void ItemCollected(Item item)
        {
            int i = _storedItems.IndexOf(item);
            item.gameObject.transform.SetParent(null);
            storageSlots[i].gameObject.SetActive(false);
            item.gameObject.SetActive(false);
            _storedItems.Remove(item);
        }
    }
}