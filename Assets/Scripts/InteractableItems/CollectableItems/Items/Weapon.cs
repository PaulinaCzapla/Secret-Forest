using System.Collections.Generic;
using GameManager;
using InteractableItems.CollectableItems.Interfaces;
using PlayerInteractions.StaticEvents;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class Weapon : WearableItem
    {
        private float _criticalHitChance;
        private float _damage;

        public Weapon(float damage, float criticalHitChance, Sprite sprite, string name, ItemType type) : base(type,
            sprite, name)
        {
            _damage = damage;
            _criticalHitChance = criticalHitChance;
        }


        public override string GetString()
        {
            string text = "";
            float damageValue = GetTypeValue(ItemValueType.Damage);

            if (!(damageValue == 0 && GameManager.GameController.GetInstance().Equipment.GetWeaponCurrentDamage(Type) == 0))
            {
                float value = damageValue -
                              GameManager.GameController.GetInstance().Equipment.GetWeaponCurrentDamage(Type);
                text += (value >= 0
                    ? "<color=#62F167>+" + value + " damage</color>\n"
                    : "<color=#FF3A38>" + value + " damage</color>\n");
            }

            float critialValue = GetTypeValue(ItemValueType.CriticalDamageChance);

            if (!(critialValue == 0 && GameManager.GameController.GetInstance().Equipment
                .GetWeaponCurrentCriticalChance(Type) ==0))
            {
                float value = critialValue - GameManager.GameController.GetInstance().Equipment
                    .GetWeaponCurrentCriticalChance(Type);
                text += (value >= 0
                    ? "<color=#62F167>+" + value + "% critical damage chance</color>\n"
                    : "<color=#FF3A38>" + value + "% critical damage chance</color>\n");
            }

            text.Trim('\n');
            return text;
        }

        public override string GetUninfluencedString()
        {
            string text = "";
            float damageValue = GetTypeValue(ItemValueType.Damage);

            if (damageValue != 0)
                text += (damageValue >= 0
                    ? "+" + damageValue + " damage\n"
                    : damageValue + " damage\n");


            float critialValue = GetTypeValue(ItemValueType.CriticalDamageChance);

            if (critialValue != 0)
                text += (critialValue >= 0
                    ? "+" + critialValue + "% critical damage chance\n"
                    : critialValue + "% critical damage chance\n");
            
            text.Trim('\n');
            return text;
        }

        public override float GetTypeValue(ItemValueType type)
        {
            if (type == ItemValueType.Damage)
                return _damage;
            if (type == ItemValueType.CriticalDamageChance)
                return _criticalHitChance;

            return 0;
        }
    }
}