using System.Collections.Generic;
using Glades.GladeTypes;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items
{
    public abstract class Item : ICollectable
    {
        public UnityEvent onCollected { get; set; } = new UnityEvent();
        public Sprite Sprite => _sprite;
        public string Name => _name;

        private string _name;
        private Sprite _sprite;

        protected Item(Sprite sprite, string name)
        {
            _name = name;
            _sprite = sprite;
        }

        public abstract void Collect();

        public override string ToString()
        {
            return _name;
        }
    }
}