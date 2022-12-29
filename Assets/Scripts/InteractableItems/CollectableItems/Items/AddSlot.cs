using System.Collections.Generic;
using GameManager;
using Glades;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items.Types;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    /// <summary>
    /// A class that represents an item, that adds one additional slot to the inventory. It implements IUsable interface, so player can use this item.
    /// </summary>
    public class AddSlot : Item, IUsable

    {
        public AddSlot(Sprite sprite, string name, string id, ItemType type) : base(sprite, name, id, type)
        {
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