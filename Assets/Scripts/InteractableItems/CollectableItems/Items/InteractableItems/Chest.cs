﻿using System.Collections.Generic;
using DebugTools;
using PlayerInteractions.Interfaces;
using UI.StorageUI;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items.InteractableItems
{
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

        private void CollectedItem(Item index)
        {
            _items.Remove(index);
            
            if(_items.Count == 0)
                OnChestEmptied?.Invoke();
        }

        public void Interact()
        {
            _animator.SetTrigger("open");
            ChestUIStaticEvents.InvokeOpenChest(_items, this);
        }

        public void Close()
        {
            _animator.SetTrigger("close");
        }
        
    }
}