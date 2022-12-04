using System.Collections.Generic;
using GameManager;
using InteractableItems.CollectableItems.Interfaces;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class Armor : WearableItem
    {

        public Armor(float defence, float dodgeChance, Sprite sprite, string name, string id, ItemType type) : base(type, sprite,
            name, id)
        {
            Values.Add(new ValueType(ItemValueType.Defence, defence));
            Values.Add(new ValueType(ItemValueType.DodgeChance, dodgeChance));
        }


        public override string GetString()
        {
            string text = "";

            float defenceValue = GetTypeValue(ItemValueType.Defence);

            if (!(defenceValue == 0 && GameManager.GameController.GetInstance().Equipment.GetArmorCurrentDefense(Type) == 0))
            {
                float value = defenceValue -
                              GameManager.GameController.GetInstance().Equipment.GetArmorCurrentDefense(Type);
                text += (value >= 0
                    ? "<color=#62F167>+" + value + " defense</color>\n"
                    : "<color=#FF3A38>" + value + " defense</color>\n");
            }

            float dodgeValue = GetTypeValue(ItemValueType.DodgeChance);

            if (!(dodgeValue == 0 && GameManager.GameController.GetInstance().Equipment.GetArmorDodgeChance(Type) == 0))
            {
                float value = dodgeValue - GameManager.GameController.GetInstance().Equipment.GetArmorDodgeChance(Type);
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
            foreach (var value in Values)
            {
                if (value.Type == type)
                    return value.Value;
            }

            return 0;
        }
    }
}