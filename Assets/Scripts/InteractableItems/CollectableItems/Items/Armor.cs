using System.Collections.Generic;
using GameManager;
using InteractableItems.CollectableItems.Interfaces;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class Armor : WearableItem
    {
        private float _defence;
        private float _dodgeChance;

        public Armor(float defence, float dodgeChance, Sprite sprite, string name, ItemType type) : base(type, sprite,
            name)
        {
            _defence = defence;
            _dodgeChance = dodgeChance;
        }


        public override string GetString()
        {
            string text = "";

            float defenceValue = GetTypeValue(ItemValueType.Defence);

            if (!(defenceValue == 0 && GameManager.GameManager.GetInstance().Equipment.GetArmorCurrentDefense(Type) == 0))
            {
                float value = defenceValue -
                              GameManager.GameManager.GetInstance().Equipment.GetArmorCurrentDefense(Type);
                text += (value >= 0
                    ? "<color=#62F167>+" + value + " defense</color>\n"
                    : "<color=#FF3A38>" + value + " defense</color>\n");
            }

            float dodgeValue = GetTypeValue(ItemValueType.DodgeChance);

            if (!(dodgeValue == 0 && GameManager.GameManager.GetInstance().Equipment.GetArmorDodgeChance(Type) == 0))
            {
                float value = dodgeValue - GameManager.GameManager.GetInstance().Equipment.GetArmorDodgeChance(Type);
                text += (value >= 0
                    ? "<color=#62F167>+" + value + "% dodge chance</color>\n"
                    : "<color=#FF3A38>" + value + "% dodge chance</color>\n");
            }

            text.Trim('\n');
            return text;
        }

        public override string GetUninfluencedString()
        {
            string text = "";

            float defenceValue = GetTypeValue(ItemValueType.Defence);

            if (defenceValue != 0)
                text += (defenceValue >= 0 ? "+" + defenceValue + " defense\n" : defenceValue + " defense\n");

            float dodgeValue = GetTypeValue(ItemValueType.DodgeChance);

            if (dodgeValue != 0)
                text += (dodgeValue >= 0 ? "+" + dodgeValue + "% dodge chance\n" : dodgeValue + "% dodge chance\n");
            
            text.Trim('\n');
            return text;
        }

        public override float GetTypeValue(ItemValueType type)
        {
            if (type == ItemValueType.Defence)
                return _defence;
            if (type == ItemValueType.DodgeChance)
                return _dodgeChance;

            return 0;
        }
    }
}