using System;
using System.Collections.Generic;
using DebugTools;
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
            DebugMassageSender.SendDebugMessage("Initialized chest "+ this.gameObject.name +" with " + items.Count
            + " \n Items: " + String.Join(",\n", _items));
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