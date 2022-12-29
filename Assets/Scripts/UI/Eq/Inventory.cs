using System;
using System.Collections.Generic;
using GameManager;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.Types;
using PlayerInteractions.Input;
using TMPro;
using UI.Events;
using UI.StorageUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Eq
{
    /// <summary>
    /// A class that represents player's inventory.
    /// </summary>
    public class Inventory : Singleton<Inventory>
    {
        public UnityEvent<bool> OnTriedAddItem { get; set; } = new UnityEvent<bool>();
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

        /// <summary>
        /// Open/closes and inventory UI.
        /// </summary>
        /// <param name="list"> Information if should be opened or closed. </param>
        private void ToggleEq(bool toggle)
        {
            if (toggle)
                OpenStorage();
            else
            {
                CloseStorage();
            }
        }

        /// <summary>
        /// Initializes inventory slots.
        /// </summary>
        /// <param name="slotsCount"> Maximum slots count. </param>
        /// <param name="elementsCount"> Items in inventory. </param>
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

        /// <summary>
        /// Removes item from list.
        /// </summary>
        /// <param name="item"> Item to remove. </param>
        private void OnSlotEmptied(Item item)
        {
            _storedItems.Remove(item);
        }

        /// <summary>
        /// Refreshes equipment slots.
        /// </summary>
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
        /// <summary>
        /// Refreshes inventory slots.
        /// </summary>
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

        /// <summary>
        /// Opens storage UI.
        /// </summary>
        public void OpenStorage()
        {
            ChestUIStaticEvents.InvokeCloseChest();
            InputManager.TapEnable = false;
            toggleEq.isOn = true;
            storageObject.SetActive(true);
        }
        /// <summary>
        /// Closes storage UI.
        /// </summary>
        public void CloseStorage()
        {
            toggleEq.isOn = false;
            storageObject.SetActive(false);
            ResetItemUI();
            InputManager.TapEnable = true;
        }

        /// <summary>
        /// Places collected item in free inventory slot.
        /// </summary>
        /// <param name="item"> Collected item </param>
        /// <returns> Returns true when item was added successfully. When there was no free slots it returns false. </returns>
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

        /// <summary>
        /// Equips new item and adds the old one to the inventory if there was any.
        /// </summary>
        /// <param name="itemToBeEquip"> Item to equip. </param>
        public void ItemEquipped(Item itemToBeEquip)
        {
            var oldItem = GameManager.GameController.GetInstance().Equipment.Equip(itemToBeEquip);

            OnResetSelectedItem(_currentSelected);
            if (oldItem != null)
                ItemCollected(oldItem);

            _storedItems.Remove(itemToBeEquip);
            RefreshEquipment();
        }

        /// <summary>
        /// Changes UI when player clicks on the equipment slot..
        /// </summary>
        /// <param name="slot"> Clicked slot. </param>
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

        /// <summary>
        /// Removes item from equipment and adds it to the inventory.
        /// </summary>
        /// <param name="item"> Item to be taken off. </param>
        private void OnTakeOffItem(Item item)
        {
            Item currentItem = GameManager.GameController.GetInstance().Equipment.Unequip(item);
            OnResetSelectedItem(_currentSelected);
            if (currentItem != null)
                ItemCollected(currentItem);
        }
        /// <summary>
        /// Changes the UI when player clicks on the inventory slot.
        /// </summary>
        /// <param name="slot"> Clicked slot.</param>
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

        /// <summary>
        /// Empties the given slot.
        /// </summary>
        /// <param name="slot"> Slot to empty. </param>
        private void OnResetSelectedItem(InventorySlot slot)
        {
            ResetItemUI();
            _storedItems.Remove(slot.CurrentItem);
        }

        /// <summary>
        /// Sets default UI appearance.
        /// </summary>
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

        /// <summary>
        /// Empties all slots.
        /// </summary>
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
        
        /// <summary>
        /// Finds free slot in the available slots list.
        /// </summary>
        /// <returns> Free slot or null, when there is no free slots.</returns>
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