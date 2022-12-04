using System.Collections.Generic;
using PlayerInteractions.StaticEvents;
using UI.Eq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace InteractableItems.CollectableItems.Items
{
    public class Food : Item, IUsable
    {
        private List<ValueType> _values;

        public Food(List<ValueType> values, Sprite sprite, string name, string id, ItemType type) 
            : base(sprite, name, id, type)
        {
            _values = values;
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