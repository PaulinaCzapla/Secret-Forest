using System.Collections.Generic;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items.Types;
using PlayerInteractions.StaticEvents;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    /// <summary>
    /// A class that represents an potion item, that can have influence on player's health and hunger stats. It implements IUsable interface, so player can use this item.
    /// </summary>
    public class Potion : Item, IUsable
    {
        private List<ValueType> _values;

        public Potion(List<ValueType> values, Sprite sprite, string name, string id, ItemType type) 
            : base(sprite, name, id, type)
        {
            _values = values;
        }

        public void Use()
        {
            PlayerStatsStaticEvents.InvokeHungerValueChanged(GetTypeValue(ItemValueType.Food));
            PlayerStatsStaticEvents.InvokeHealthValueChanged(GetTypeValue(ItemValueType.Health));
        }

        public override string GetString()
        {
            string text = "";
            float value = GetTypeValue(ItemValueType.Food);

            if (value != 0)
                text += (value > 0 ? "+" + value + " food\n" : value + " food\n");

            value = GetTypeValue(ItemValueType.Health);
            if (value != 0)
                text += (value > 0 ? "+" + value + " health\n" : value + " health\n");

            text.Trim('\n');
            return text;
        }

        private float GetTypeValue(ItemValueType type)
        {
            foreach (var value in _values)
            {
                if (value.Type == type)
                    return value.Value;
            }

            return 0;
        }
    }
}