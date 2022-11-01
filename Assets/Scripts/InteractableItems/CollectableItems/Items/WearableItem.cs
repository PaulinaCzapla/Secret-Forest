using InteractableItems.CollectableItems.Interfaces;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public abstract class WearableItem : Item, IEquippable
    {
        public ItemType Type { get; private set; }

        
        protected WearableItem(ItemType type, Sprite sprite, string name) : base(sprite, name)
        {
            Type = type;
        }

        public override bool Collect()
        {
            if (InventoryUI.Instance.ItemCollected(this))
            {
                onCollected?.Invoke();
                return true;
            }
            return false;
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