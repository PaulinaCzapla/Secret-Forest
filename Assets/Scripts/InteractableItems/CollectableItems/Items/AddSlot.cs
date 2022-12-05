using System.Collections.Generic;
using GameManager;
using Glades;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class AddSlot : Item, IUsable

    {
        public AddSlot(Sprite sprite, string name, string id, ItemType type) : base(sprite, name, id, type)
        {
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

        public override string GetString()
        {
            return "Unlocks an additional slot in the inventory";
        }

        public void Use()
        {
            GameController.GetInstance().UnlockSlot();
        }
    }
}