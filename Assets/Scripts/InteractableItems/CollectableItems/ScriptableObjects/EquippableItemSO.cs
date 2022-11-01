using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EquippableItem", menuName = "ScriptableObjects/EquippableItem", order = 0)]

    public class EquippableItemSO : ItemSO
    {
        [Header("Values")] [SerializeField] private List<ValuesPossibilitiesType> values;

        public override Item GetItem()
        {
            switch (type)
            {
                case ItemType.Boots:
                case ItemType.Breastplate:
                case ItemType.Helmet:
                case ItemType.ShinGuards:
                {
                    float defenceValue = 0;
                    float dodgeValue = 0; 
                    
                    foreach (var value in values)
                    {
                        if (value.Type == ItemValueType.Defence)
                            defenceValue = value.Values.Evaluate(Random.value);
                        if (value.Type == ItemValueType.DodgeChance)
                            dodgeValue = value.Values.Evaluate(Random.value);
                    }
                    
                    return new Armor(defenceValue, dodgeValue, sprite, name, type);
                    break;
                }
                case ItemType.Bow:
                case ItemType.WhiteWeapon:
                {
                    float damageValue = 0;
                    float criticalValue = 0;
                    
                    foreach (var value in values)
                    {
                        if (value.Type == ItemValueType.CriticalDamageChance)
                            damageValue = value.Values.Evaluate(Random.value);
                        if (value.Type == ItemValueType.Damage)
                            criticalValue = value.Values.Evaluate(Random.value);
                    }
                    
                    return new Weapon(damageValue,criticalValue , sprite, name,
                        type);

                    break;
                }
                default:
                    return null;
            }
        }
    }
}