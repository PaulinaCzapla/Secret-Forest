﻿using System;
using System.Collections.Generic;
using GameManager;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items;
using PlayerInteractions.Input;
using TMPro;
using UI.StorageUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Eq
{
    public class Inventory : Singleton<Inventory>
    {
        public UnityEvent<bool> OnTriedAddItem { get; set; } = new UnityEvent<bool>();
       // public static Inventory Instance { get; private set; }
        public List<Item> StoredItems => _storedItems;
        
        [SerializeField] private GameObject storageObject;
        [SerializeField] private Toggle toggleEq;
        [Header("Slots")] [SerializeField] private List<InventorySlot> slots;
        [SerializeField] private InventorySlot swordSlot;
        [SerializeField] private InventorySlot bowSlot;
        [SerializeField] private InventorySlot helmetSlot;
        [SerializeField] private InventorySlot breastplateSlot;
        [SerializeField] private InventorySlot bootsSlot;
        [SerializeField] private InventorySlot shinGuardsSlot;

        [Header("Item menu")] [SerializeField] private Button actionButton;
        [SerializeField] private TextMeshProUGUI actionButtonText;
        [SerializeField] private TextMeshProUGUI itemStats;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Button throwItemButton;

        private List<Item> _storedItems = new List<Item>();
        private InventorySlot _currentSelected;
        private Dictionary<ItemType, InventorySlot> _equipmentElements;

        protected override void Awake()
        {
            base.Awake();
            _equipmentElements = new Dictionary<ItemType, InventorySlot>();
            _equipmentElements.Add(ItemType.Boots, bootsSlot);
            _equipmentElements.Add(ItemType.Breastplate, breastplateSlot);
            _equipmentElements.Add(ItemType.Helmet, helmetSlot);
            _equipmentElements.Add(ItemType.ShinGuards, shinGuardsSlot);
            _equipmentElements.Add(ItemType.Bow, bowSlot);
            _equipmentElements.Add(ItemType.WhiteWeapon, swordSlot);
            ResetItemUI();
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
            if (toggle)
                OpenStorage();
            else
            {
                CloseStorage();
            }
        }

        public void InitializeStorage(int slotsCount, List<Item> items = null)
        {
            ResetSlots();
            
            int i = 0;
            foreach (var slot in slots)
            {
                if (i < slotsCount)
                {
                    slot.gameObject.SetActive(true);
                    slot.OnSlotEmptied.AddListener(OnSlotEmptied);
                }
                else
                {
                    slot.gameObject.SetActive(false);
                }

                i++;
            }

            if (items != null)
            {
                _storedItems = items;
               
            }
            else
            {
                _storedItems = new List<Item>();
            }
            RefreshInventory();
        }

        private void OnSlotEmptied(Item item)
        {
            _storedItems.Remove(item);
        }

        private void RefreshEquipment()
        {
            ItemType[] types =
            {
                ItemType.Boots, ItemType.Breastplate, ItemType.Helmet, ItemType.ShinGuards, ItemType.Bow,
                ItemType.WhiteWeapon
            };

            foreach (var type in types)
            {
                var currentEquipped = GameManager.GameController.GetInstance().Equipment.GetCurrentEquippedItem(type);

                if (currentEquipped != null)
                {
                    _equipmentElements[type].Init(currentEquipped);
                    _equipmentElements[type].OnSlotClicked.RemoveAllListeners();
                    _equipmentElements[type].OnSlotClicked.AddListener(OnEquipmentSlotClicked);
                }
            }
        }

        private void RefreshInventory()
        {
            RefreshEquipment();

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
                    slot.OnSlotClicked.AddListener(OnItemClicked);
                    i++;
                }
            }
        }

        public void OpenStorage()
        {
            ChestUIStaticEvents.InvokeCloseChest();
            InputManager.TapEnable = false;
            toggleEq.isOn = true;
            storageObject.SetActive(true);
        }

        public void CloseStorage()
        {
            toggleEq.isOn = false;
            storageObject.SetActive(false);
            ResetItemUI();
            InputManager.TapEnable = true;
        }

        public bool ItemCollected(Item item)
        {
            var freeSlot = GetFreeSlot();
            OnTriedAddItem?.Invoke(freeSlot);

            if (freeSlot)
            {
                freeSlot.Init(item);
                freeSlot.OnSlotClicked.AddListener(OnItemClicked);
                _storedItems.Add(item);
            }

            return freeSlot;
        }

        public void ItemEquipped(Item itemToBeEquip)
        {
            var oldItem = GameManager.GameController.GetInstance().Equipment.Equip(itemToBeEquip);

            OnResetSelectedItem(_currentSelected);
            if (oldItem != null)
                ItemCollected(oldItem);

            _storedItems.Remove(itemToBeEquip);
            RefreshEquipment();
        }

        private void OnEquipmentSlotClicked(InventorySlot slot)
        {
            ResetItemUI();

            if (GetFreeSlot())
            {
                actionButtonText.text = "Take off";
                actionButton.gameObject.SetActive(true);
                actionButton.onClick.AddListener(() => OnTakeOffItem(slot.CurrentItem));
                actionButton.onClick.AddListener(slot.OnEmptySlot);
            }

            itemStats.text = slot.ItemInfoUninfluenced;
            itemName.text = slot.CurrentItem.Name;
            _currentSelected = slot;

            slot.Select();
        }

        private void OnTakeOffItem(Item item)
        {
            Item currentItem = GameManager.GameController.GetInstance().Equipment.Unequip(item);
            OnResetSelectedItem(_currentSelected);
            if (currentItem != null)
                ItemCollected(currentItem);
        }

        private void OnItemClicked(InventorySlot slot)
        {
            ResetItemUI();

            if (slot.CurrentItem is IEquippable)
                actionButtonText.text = "Equip";
            else if (slot.CurrentItem is IUsable)
                actionButtonText.text = "Use";

            actionButton.gameObject.SetActive(true);
            actionButton.onClick.AddListener(slot.OnUseItem);

            if (slot.CurrentItem is IUsable)
                actionButton.onClick.AddListener(() => OnResetSelectedItem(slot));

            throwItemButton.gameObject.SetActive(true);
            throwItemButton.onClick.AddListener(slot.OnEmptySlot);
            throwItemButton.onClick.AddListener(() => OnResetSelectedItem(slot));

            itemStats.text = slot.ItemInfo;
            itemName.text = slot.CurrentItem.Name;
            _currentSelected = slot;

            slot.Select();
        }

        private void OnResetSelectedItem(InventorySlot slot)
        {
            ResetItemUI();
            _storedItems.Remove(slot.CurrentItem);
        }

        private void ResetItemUI()
        {
            if (_currentSelected)
                _currentSelected.Unselect();
            _currentSelected = null;
            actionButton.gameObject.SetActive(false);
            actionButton.onClick.RemoveAllListeners();

            throwItemButton.gameObject.SetActive(false);
            throwItemButton.onClick.RemoveAllListeners();

            itemStats.text = "Select an item";
            itemName.text = "";
        }

        private void ResetSlots()
        {
            foreach (var slot in slots)
            {
               slot.OnEmptySlot();
            }

            ItemType[] types =
            {
                ItemType.Boots, ItemType.Breastplate, ItemType.Helmet, ItemType.ShinGuards, ItemType.Bow,
                ItemType.WhiteWeapon
            };

            foreach (var type in types)
            {
                _equipmentElements[type].OnEmptySlot();
            }
            
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