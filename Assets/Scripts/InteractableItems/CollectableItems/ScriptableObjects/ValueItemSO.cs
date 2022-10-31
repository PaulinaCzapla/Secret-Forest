﻿using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ValueItem", menuName = "ScriptableObjects/ValueItem", order = 0)]
    public class ValueItemSO : ItemSO
    {
        [Header("Values")] 
        [SerializeField] private List<ValueType> values;

        public override Item GetItem()
        {
            switch (type)
            {
                case ItemType.Food:
                {
                    return new Food(values,sprite, name );

                    break;
                }
                case ItemType.Potion:
                {
                    float value = 0;

                    foreach (var v in values)
                    {
                        if (v.Type == ItemValueType.Health)
                            value = v.Value;
                    }
                    return new HealthPotion(value,sprite, name );

                    break;
                }
                default:
                    return null;
            }
        }
    }
}