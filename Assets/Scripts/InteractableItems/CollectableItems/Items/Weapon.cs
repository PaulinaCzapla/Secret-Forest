using System.Collections.Generic;
using GameManager;
using InteractableItems.CollectableItems.Interfaces;
using PlayerInteractions.StaticEvents;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class Weapon : Item, IEquippable
    {
        public ItemType Type => _type;
        
        private ItemType _type;
        private List<ValueType> _values;

        public Weapon (List<ValueType> values, Sprite sprite, string name, ItemType type) : base(sprite, name)
        {
            _type = type;
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

        public override string GetString()
        {
            string text = "";
           // float value = GameStats.GetInstance().PlayerStats.GetWeaponCurrentDamage(_type);
            
            float damageValue = GetTypeValue(ItemValueType.Damage);

            if (damageValue != 0)
            {
                float value = damageValue - GameStats.GetInstance().PlayerStats.GetWeaponCurrentDamage(_type);
                text += (value > 0 ? "<color=green>+" + value + " damage</color>\n" : "<color=red>" + value + " damage</color>\n");
            }

            float critialValue = GetTypeValue(ItemValueType.CriticalDamageChance);

            if (damageValue != 0)
            {
                float value = critialValue - GameStats.GetInstance().PlayerStats.GetWeaponCurrentCriticalChance(_type);
                text += (value > 0 ? "<color=green>+" + value + "% critical damage chance</color>\n" : "<color=red>" +value + "% critical damage chance</color>\n");
            }

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

        public void Equip()
        {
            throw new System.NotImplementedException();
        }
    }
}