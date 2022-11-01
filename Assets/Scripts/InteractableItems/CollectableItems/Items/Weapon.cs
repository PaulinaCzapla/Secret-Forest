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
        
        private float _criticalHitChance;
        private float _damage;
        public Weapon (float damage, float criticalHitChance, Sprite sprite, string name, ItemType type) : base(sprite, name)
        {
            _type = type;
            _damage = damage;
            _criticalHitChance = criticalHitChance;
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
                float value = damageValue - GameManager.GameManager.GetInstance().Equipment.GetWeaponCurrentDamage(_type);
                text += (value > 0 ? "<color=green>+" + value + " damage</color>\n" : "<color=red>" + value + " damage</color>\n");
            }

            float critialValue = GetTypeValue(ItemValueType.CriticalDamageChance);

            if (critialValue != 0)
            {
                float value = critialValue - GameManager.GameManager.GetInstance().Equipment.GetWeaponCurrentCriticalChance(_type);
                text += (value > 0 ? "<color=green>+" + value + "% critical damage chance</color>\n" : "<color=red>" +value + "% critical damage chance</color>\n");
            }

            text.Trim('\n');
            return text;
        }

        public float GetTypeValue(ItemValueType type)
        {
            if (type == ItemValueType.Damage)
                return _damage;
            if (type == ItemValueType.CriticalDamageChance)
                return _criticalHitChance;

            return 0;
        }

        public void Equip()
        {
            throw new System.NotImplementedException();
        }
    }
}