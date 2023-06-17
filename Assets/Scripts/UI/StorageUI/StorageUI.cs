using System;
using System.Collections.Generic;
using InteractableItems;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.InteractableItems;
using PlayerInteractions.Input;
using TMPro;
using UI.Eq;
using UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI.StorageUI
{
    /// <summary>
    /// A class that represents storage/chest UI.
    /// </summary>
    public class StorageUI : MonoBehaviour
    {
        [SerializeField] private GameObject chestUIObject;
        [SerializeField] private List<StorageSlot> slots;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Button closeButton;

        private Color _infoTextInitialColor;
        private Chest _currentChest;
        private void Awake()
        {
            _infoTextInitialColor = infoText.color;
        }

        private void Start()
        {
            closeButton.onClick.AddListener(Close);
            ChestUIStaticEvents.SubscribeToOpenChest(Open);
            ChestUIStaticEvents.SubscribeToCloseChest(Close);
            Inventory.Instance.OnTriedAddItem.AddListener(OnItemAdded);
        }
        /// <summary>
        /// Called when an item is added to the inventory. It displays relevant information. 
        /// </summary>
        /// <param name="succeded"> Information if item was successfully added to the invemtory. </param>
        private void OnItemAdded(bool succeded)
        {
            if (succeded)
            {
                infoText.color = _infoTextInitialColor;
                infoText.text = "Item added";
            }
            else
            {
                infoText.color = Color.red;
                infoText.text = "Inventory is full";
            }
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(Close);
            ChestUIStaticEvents.UnsubscribeFromOpenChest(Open);
            ChestUIStaticEvents.UnsubscribeFromCloseChest(Close);
        }

        /// <summary>
        /// Fills chest's slots with items and opens chest's UI.
        /// </summary>
        /// <param name="items"> Items that are in the chest. </param>
        /// <param name="chest"> Reference to the Chest object that was opened. </param>
        public void Open(List<Item> items, Chest chest)
        {
            InputManager.TapEnable = false;
            ResetUI();
            int i = 0;

            foreach (var item in items)
            {
                slots[i].InitSlot(item);
                slots[i].gameObject.SetActive(true);
                i++;
                if (i > slots.Count - 1)
                    break;
            }

            _currentChest = chest;
            _currentChest.OnChestEmptied += Close;
            chestUIObject.SetActive(true);
            
            if (_currentChest.IsEmpty)
            {
                infoText.color = _infoTextInitialColor;
                infoText.text = "Empty";
            }
        }

        /// <summary>
        /// Closes the storage UI.
        /// </summary>
        private void Close()
        {
            if (_currentChest)
            {
                InputManager.TapEnable = true;
                _currentChest.OnChestEmptied -= Close;
                chestUIObject.SetActive(false);
                ResetUI();
                _currentChest.Close();
            }
        }

        private void ResetUI()
        {
            infoText.text = "";
            foreach (var slot in slots)
            {
                slot.gameObject.SetActive(false);
            }
        }
    }
}