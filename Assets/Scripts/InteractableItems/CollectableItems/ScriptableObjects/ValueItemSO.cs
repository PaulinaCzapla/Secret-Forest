using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ValueItem", menuName = "ScriptableObjects/ValueItem", order = 0)]
    public class ValueItemSO : ItemSO
    {
        [Header("Values")] 
        [SerializeField] private List<ValueType> values;

        public override Item GetRandom()
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
                    return new Potion(values,sprite, name );
                    break;
                }
                default:
                    return null;
            }
        }
    }
}