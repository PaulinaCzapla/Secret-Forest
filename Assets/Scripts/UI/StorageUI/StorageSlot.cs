using InteractableItems.CollectableItems.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.StorageUI
{
    /// <summary>
    /// A class that represents a single slot in chest/storage.
    /// </summary>
    public class StorageSlot : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemInfo;
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        private Item _item;

        /// <summary>
        /// Initializes slot with item's values.
        /// </summary>
        /// <param name="item"> Item object. </param>
        public void InitSlot(Item item)
        {
            _item = item;
            itemName.text = item.Name;
            itemInfo.text = item.GetString();
            image.sprite = item.Sprite;

            button.onClick.AddListener(OnItemCollected);
        }
        /// <summary>
        /// Is called when player collected an item stored in slot.
        /// </summary>
        private void OnItemCollected()
        {
            if (_item.Collect())
                this.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnItemCollected);
        }
    }
}