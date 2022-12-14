using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.Types;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using Utilities.ValueRepresentation;
using ValueType = InteractableItems.CollectableItems.Items.Types.ValueType;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    /// <summary>
    /// A scriptable object that represents an item whose value is determined by a curve.
    /// </summary>
    [CreateAssetMenu(fileName = "EquippableItem", menuName = "ScriptableObjects/EquippableItem", order = 0)]
    public class CurveItemSO : ItemSO
    {
        [Header("Values")] 
        [SerializeField] private List<ValuesPossibilitiesType> values;

        /// <summary>
        /// Returns proper Item object.
        /// </summary>
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
                        (float multiplier, float min) = GameManager.GameController.GetInstance()
                            .GetValueMultiplierAndMin(type, value.Type);

                        float randomValue = value.Values.Evaluate(Random.value) * multiplier;
                        float newValue = ValueRounder.RoundUp(
                            Mathf.Clamp(randomValue, (randomValue == 0 ? 0 : min), multiplier), 0.5f);

                        if (value.Type == ItemValueType.Defence)
                            defenceValue = newValue;
                        
                        if (value.Type == ItemValueType.DodgeChance)
                            dodgeValue = newValue;
                    }

                    return new Armor(defenceValue, dodgeValue, sprite, name, ID, type);
                    break;
                }
                case ItemType.Bow:
                case ItemType.WhiteWeapon:
                {
                    float damageValue = 0;
                    float criticalValue = 0;

                    foreach (var value in values)
                    {
                        (float multiplier, float min) = GameManager.GameController.GetInstance()
                            .GetValueMultiplierAndMin(type, value.Type);
                        float randomValue = value.Values.Evaluate(Random.value) * multiplier;
                        float newValue = ValueRounder.RoundUp(
                            Mathf.Clamp(randomValue, (randomValue == 0 ? 0 : min), multiplier), 0.5f);
                        
                        if (value.Type == ItemValueType.CriticalDamageChance)
                 
                            criticalValue = newValue;

                        if (value.Type == ItemValueType.Damage)
                            damageValue = newValue;
                    }

                    return new Weapon(damageValue, criticalValue, sprite, name, ID,
                        type);

                    break;
                }
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns proper Item object with initialization values.
        /// </summary>
        /// <param name="initValues"> Initialization values. </param>
        public override Item GetItem(List<ValueType> initValues)
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

                    foreach (var value in initValues)
                    {
                        if (value.Type == ItemValueType.Defence)
                            defenceValue = value.Value;
                        
                        if (value.Type == ItemValueType.DodgeChance)
                            dodgeValue = value.Value;
                    }

                    return new Armor(defenceValue, dodgeValue, sprite, name, ID, base.type);
                    break;
                }
                case ItemType.Bow:
                case ItemType.WhiteWeapon:
                {
                    float damageValue = 0;
                    float criticalValue = 0;

                    foreach (var value in initValues)
                    {
                        if (value.Type == ItemValueType.CriticalDamageChance)
                            criticalValue = value.Value;

                        if (value.Type == ItemValueType.Damage)
                            damageValue = value.Value;
                    }

                    return new Weapon(damageValue, criticalValue, sprite, name, ID,
                        base.type);

                    break;
                }
                default:
                    return null;
            }
        }
    }
}