using System;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Eq
{
    public class InventorySlot : MonoBehaviour
    {
        public UnityEvent<InventorySlot > OnSlotClicked { get; set; } = new UnityEvent<InventorySlot >();
        public bool IsActiveAndFree => gameObject.activeSelf && _currentItem == null;
        public string ItemInfo => (_currentItem != null ? _currentItem.GetString() : "");
        public Item CurrentItem => _currentItem;
        
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private GameObject selected;
        
        private Item _currentItem;

        public void Init(Item item)
        {
            _currentItem = item;
            button.onClick.AddListener(() => OnSlotClicked.Invoke(this));
            image.sprite = _currentItem.Sprite;
            image.gameObject.SetActive(true);
        }

        public void OnEmptySlot()
        {
            _currentItem = null;
            image.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
            OnSlotClicked.RemoveAllListeners();
        }

        public void Select()
        {
            selected.SetActive(true);
        }

        public void Unselect()
        {
            selected.SetActive(false);
        }
        public void OnUseItem()
        {
            if (_currentItem is IUsable usable)
            {
                usable.Use();
            }else if (_currentItem is IEquippable equippable)
            {
                equippable.Equip();
            }
            
            OnEmptySlot();
        }
        
        private void OnDisable()
        {
            //button.onClick.RemoveAllListeners();
        }
    }
}