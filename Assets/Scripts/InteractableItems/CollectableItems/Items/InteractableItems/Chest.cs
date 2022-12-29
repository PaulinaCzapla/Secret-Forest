using System.Collections.Generic;
using DebugTools;
using PlayerInteractions.Interfaces;
using UI.Events;
using UI.StorageUI;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items.InteractableItems
{
    /// <summary>
    /// A class that is a chest representation. It implements IInteractable interface.
    /// </summary>
    public class Chest : MonoBehaviour, IInteractable
    {
        public bool IsEmpty => _items.Count == 0;
        public UnityAction OnChestEmptied { get; set; }
        private List<Item> _items;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            for (int i =0; i<_items.Count; i++)
            {
                _items[i].onCollected.RemoveAllListeners();
            }
        }

        /// <summary>
        /// Initializes chest by filling it with items.
        /// </summary>
        /// <param name="items"> List of items in chest. </param>
        public void Init(List<Item> items)
        {
            this._items = items;

            foreach (var item in _items)
            {
                item.onCollected.AddListener(() => CollectedItem(item));
            }
            DebugMessageSender.SendDebugMessage("Initialized chest "+ this.gameObject.name +" with " + items.Count
            + " items: " + string.Join(",\n", _items));
        }

        /// <summary>
        /// Removes the item from the list when item is collected.
        /// </summary>
        /// <param name="index"> Index of an item in list. </param>
        private void CollectedItem(Item index)
        {
            _items.Remove(index);
            
            if(_items.Count == 0)
                OnChestEmptied?.Invoke();
        }

        /// <summary>
        /// Opens the chest by starting animation and setting UI.
        /// </summary>
        public void Interact()
        {
            _animator.SetTrigger("open");
            ChestUIStaticEvents.InvokeOpenChest(_items, this);
        }

        /// <summary>
        /// Closes the chest.
        /// </summary>
        public void Close()
        {
            _animator.SetTrigger("close");
        }
        
    }
}