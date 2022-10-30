using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using TMPro;
using UI.Eq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.StorageUI
{
    public class StorageUI : MonoBehaviour
    {
        [SerializeField] private GameObject chestUIObject;
        [SerializeField] private List<StorageSlot> slots;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Button closeButton;

        private Color _infoTextInitialColor;
        private void Awake()
        {
            _infoTextInitialColor = infoText.color;
        }

        private void OnEnable()
        {
            closeButton.onClick.AddListener(Close);
            ChestUIStaticEvents.SubscribeToOpenChest(Open);
            InventoryUI.Instance.OnTriedAddItem.AddListener(OnItemAdded);
        }

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
        }

        public void Open(List<Item> items)
        {
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

            chestUIObject.SetActive(true);
        }

        private void Close()
        {
            chestUIObject.SetActive(false);
            ResetUI();
            
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