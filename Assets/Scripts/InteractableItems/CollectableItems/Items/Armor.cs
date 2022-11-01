using System.Collections.Generic;
using GameManager;
using InteractableItems.CollectableItems.Interfaces;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class Armor : Item, IEquippable
    {
        public ItemType Type => _type;
        
        private ItemType _type;
        private float _defence;
        private float _dodgeChance;

        public Armor (float defence, float dodgeChance, Sprite sprite, string name, ItemType type) : base(sprite, name)
        {
            _type = type;
            _defence = defence;
            _dodgeChance = dodgeChance;
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

            float defenceValue = GetTypeValue(ItemValueType.Defence);

            if (defenceValue != 0)
            {
                float value = defenceValue - GameManager.GameManager.GetInstance().Equipment.GetArmorCurrentDefense(_type);
                text += (value > 0 ? "<color=green>+" + value + " defense</color>\n" : "<color=red>" + value + " defense</color>\n");
            }

            float dodgeValue = GetTypeValue(ItemValueType.DodgeChance);

            if (dodgeValue  != 0)
            {
                float value = dodgeValue - GameManager.GameManager.GetInstance().Equipment.GetArmorDodgeChance(_type);
                text += (value > 0 ? "<color=green>+" + value + "% dodge chance</color>\n" : "<color=red>" +value + "% dodge chance</color>\n");
            }

            text.Trim('\n');
            return text;
        }

        public float GetTypeValue(ItemValueType type)
        {
            if (type == ItemValueType.Defence)
                return _defence;
            if (type == ItemValueType.DodgeChance)
                return _dodgeChance;

            return 0;
        }

        public void Equip()
        {
            throw new System.NotImplementedException();
        }
    }
}