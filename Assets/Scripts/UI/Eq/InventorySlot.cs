using System;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Eq
{
    /// <summary>
    /// A class that represents single inventory slot.
    /// </summary>
    public class InventorySlot : MonoBehaviour
    {
        public UnityEvent<InventorySlot> OnSlotClicked { get; set; } = new UnityEvent<InventorySlot>();
        public bool IsActiveAndFree => gameObject.activeSelf && _currentItem == null;
        public string ItemInfo => (_currentItem != null ? _currentItem.GetString() : "");

        public string ItemInfoUninfluenced => (_currentItem != null
            ? (_currentItem is WearableItem item ? item.GetUninfluencedString() : _currentItem.GetString()) : "");

        public UnityEvent<Item> OnSlotEmptied { get; set; } = new UnityEvent<Item>();
        public Item CurrentItem => _currentItem;

        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private Image defaultImage;
        [SerializeField] private GameObject selected;
        
        private Item _currentItem;

        public void Init(Item item)
        {
            _currentItem = item;
            button.onClick.AddListener(() => OnSlotClicked.Invoke(this));
            image.sprite = _currentItem.Sprite;
            image.gameObject.SetActive(true);

            if (defaultImage)
                defaultImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Removes item from a slot.
        /// </summary>
        public void OnEmptySlot()
        {
            OnSlotEmptied?.Invoke(_currentItem);
            _currentItem = null;
            image.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
            OnSlotClicked.RemoveAllListeners();

            if (defaultImage)
                defaultImage.gameObject.SetActive(true);
        }

        /// <summary>
        /// Marks this slot as selected.
        /// </summary>
        public void Select()
        {
            selected.SetActive(true);
        }
        /// <summary>
        /// Unselects slot.
        /// </summary>
        public void Unselect()
        {
            selected.SetActive(false);
        }

        /// <summary>
        /// Empties slot and uses item.
        /// </summary>
        public void OnUseItem()
        {
            if (_currentItem is IUsable usable)
            {
                OnEmptySlot();
                usable.Use();
            }
            else if (_currentItem is IEquippable equippable)
            {
                OnEmptySlot();
                equippable.Equip();
            }
        }
    }
}