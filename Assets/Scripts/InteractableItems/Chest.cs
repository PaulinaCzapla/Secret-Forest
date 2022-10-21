using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems;
using PlayerInteractions.Interfaces;
using UnityEngine;

namespace InteractableItems
{
    public class Chest : MonoBehaviour, IInteractable
    {
        private List<Item> _items;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Init(List<Item> items)
        {
            this._items = items;
        }
        
        public void Interact()
        {
            _animator.SetTrigger("open");
         //   Invoke();
        }

        public void Close()
        {
            _animator.SetTrigger("close");
        }
        
    }
}