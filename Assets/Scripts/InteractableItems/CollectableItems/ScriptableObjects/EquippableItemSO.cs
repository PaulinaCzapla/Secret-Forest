using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;
using ValueRepresentation;

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
                            defenceValue = ValueRounder.RoundUp(value.Values.Evaluate(Random.value) * GameManager
                                .GameManager.GetInstance().LevelsConfig
                                .GetValueMultiplier(value.Type), 0.5f);
                        if (value.Type == ItemValueType.DodgeChance)
                            dodgeValue = ValueRounder.RoundUp(value.Values.Evaluate(Random.value) * GameManager
                                .GameManager.GetInstance().LevelsConfig
                                .GetValueMultiplier(value.Type), 0.5f);
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
                        {
                            Debug.Log(" VALUE " + value.Values.Evaluate(Random.value) + "    " +
                                      "multiplier = " + GameManager
                                          .GameManager.GetInstance().LevelsConfig
                                          .GetValueMultiplier(value.Type)
                                          + "result = " + ValueRounder.RoundUp(value.Values.Evaluate(Random.value) * GameManager
                                          .GameManager.GetInstance().LevelsConfig
                                          .GetValueMultiplier(value.Type), 0.5f));
                            
                            criticalValue = ValueRounder.RoundUp(value.Values.Evaluate(Random.value) * GameManager
                                .GameManager.GetInstance().LevelsConfig
                                .GetValueMultiplier(value.Type), 0.5f);
                        }

                        if (value.Type == ItemValueType.Damage)
                            damageValue = ValueRounder.RoundUp(
                                value.Values.Evaluate(Random.value) * GameManager.GameManager.GetInstance()
                                    .LevelsConfig.GetValueMultiplier(value.Type), 0.5f);
                    }

                    return new Weapon(damageValue, criticalValue, sprite, name,
                        type);

                    break;
                }
                default:
                    return null;
            }
        }
    }
}