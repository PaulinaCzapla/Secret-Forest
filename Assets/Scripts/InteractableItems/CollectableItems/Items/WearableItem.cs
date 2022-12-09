using System.Collections.Generic;
using InteractableItems.CollectableItems.Interfaces;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public abstract class WearableItem : Item, IEquippable
    {
        public List<ValueType> Values { get; protected set; } = new List<ValueType>();

        protected WearableItem(ItemType type, Sprite sprite, string name, string id) : base(sprite, name, id, type)
        {
        }
        
        public abstract override string GetString();

        public abstract string GetUninfluencedString();

        public abstract float GetTypeValue(ItemValueType type);
        public void Equip()
        {
            InventoryUI.Instance.ItemEquipped(this);

        }
    }
}