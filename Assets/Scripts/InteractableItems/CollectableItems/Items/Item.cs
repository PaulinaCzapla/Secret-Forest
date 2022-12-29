using System.Collections.Generic;
using Glades.GladeTypes;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items.Types;
using UI.Eq;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items
{
    /// <summary>
    /// A base abstract class that represents an item. It implements ICollectable interface, co player can collect any item.
    /// </summary>
    public abstract class Item : ICollectable
    {
        public UnityEvent onCollected { get; set; } = new UnityEvent();
        public ItemType Type { get; private set; }
        public Sprite Sprite => _sprite;
        public string ID { get; private set; }
        public string Name => _name;

        private string _name;
        private Sprite _sprite;

        protected Item(Sprite sprite, string name, string id, ItemType type)
        {
            _name = name;
            _sprite = sprite;
            ID = id;
            Type = type;
        }

        /// <summary>
        /// Implementation of the method from ICollectable. Adds item to the inventory if it's possible.
        /// </summary>
        /// <returns> Information if was successfully collected. </returns>
        public virtual bool Collect()
        {
            if (Inventory.Instance.ItemCollected(this))
            {
                onCollected?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns item name.
        /// </summary>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Returns item description.
        /// </summary>
        /// <returns> Item textual description. </returns>
        public abstract string GetString();
    }
}