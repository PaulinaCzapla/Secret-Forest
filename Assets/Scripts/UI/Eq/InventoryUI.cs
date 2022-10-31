﻿using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Eq
{
    public class InventoryUI : MonoBehaviour
    {
        public UnityEvent<bool> OnTriedAddItem { get; set; } = new UnityEvent<bool>();
        public static InventoryUI Instance { get; private set; }

        [SerializeField] private GameObject storageObject;
        [SerializeField] private List<InventorySlot> slots;
        [SerializeField] private Toggle toggleEq;
      
        [Header("Item menu")] 
        [SerializeField] private Button actionButton;
        [SerializeField] private TextMeshProUGUI actionButtonText;
        [SerializeField] private TextMeshProUGUI itemStats;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Button throwItemButton;
        
        private List<Item> _storedItems = new List<Item>();
        
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

        private void OnEnable()
        {
            toggleEq.onValueChanged.AddListener(ToggleEq);
        }

        private void OnDisable()
        {
            toggleEq.onValueChanged.RemoveListener(ToggleEq);
        }

        private void ToggleEq(bool toggle)
        {
            if(toggle)
                OpenStorage();
            else
            {
                CloseStorage();
            }
        }

        public void InitializeStorage(int slotsCount, List<Item> items = null)
        {
            int i = 0;
            foreach (var slot in slots)
            {
                if(i<slotsCount)
                    slot.gameObject.SetActive(true);
                else
                {
                    slot.gameObject.SetActive(false);
                }

                i++;
            }

            if (items != null)
            {
                _storedItems = items;
                RefreshInventory();
            }
        }

        private void RefreshInventory()
        {
            if (_storedItems == null)
                return;
            int i = 0;
                foreach (var slot in slots)
                {
                    if (i >= _storedItems.Count)
                        return;
                    if (slot.IsActiveAndFree)
                    {
                        slot.Init(_storedItems[i]);
                        i++;
                    }
                }
        }
        
        public void OpenStorage()
        {
            storageObject.SetActive(true);
            
        }

        private void CloseStorage()
        {
            storageObject.SetActive(false);
            ResetItemUI();
        }

        public void ItemCollected(Item item)
        {
            var freeSlot = GetFreeSlot();
            OnTriedAddItem?.Invoke(freeSlot);

            if (freeSlot)
            {
                freeSlot.Init(item);
                freeSlot.OnSlotClicked.AddListener(OnItemClicked);
                 _storedItems.Add(item);
            }
        }

        private void OnItemClicked(InventorySlot  slot)
        {
            ResetItemUI();
            
            if (slot.CurrentItem is IEquippable)
                actionButtonText.text = "Equip";
            else if(slot.CurrentItem is IUsable)
                actionButtonText.text = "Use";
            
            actionButton.gameObject.SetActive(true);
            actionButton.onClick.AddListener(slot.OnUseItem);
            
            throwItemButton.gameObject.SetActive(true);
            throwItemButton.onClick.AddListener(slot.OnEmptySlot);
            throwItemButton.onClick.AddListener(() => _storedItems.Remove(slot.CurrentItem));
            
            itemStats.text = slot.ItemInfo;
            itemName.text = slot.CurrentItem.Name;
        }

        private void ResetItemUI()
        {
            actionButton.gameObject.SetActive(false);
            actionButton.onClick.RemoveAllListeners();
            
            throwItemButton.gameObject.SetActive(false);
            throwItemButton.onClick.RemoveAllListeners();

            itemStats.text = "";
            itemName.text = "";
        }
        
        private InventorySlot GetFreeSlot()
        {
            foreach (var slot in slots)
            {
                if (slot.IsActiveAndFree)
                    return slot;
            }

            return null;
        }
    }
}