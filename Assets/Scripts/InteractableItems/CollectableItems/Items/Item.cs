using System.Collections.Generic;
using Glades.GladeTypes;
using UI.Eq;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items
{
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

        public virtual bool Collect()
        {
            if (Inventory.Instance.ItemCollected(this))
            {
                onCollected?.Invoke();
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return _name;
        }

        public abstract string GetString();
    }
}