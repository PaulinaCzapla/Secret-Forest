using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.Types;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    /// <summary>
    /// A scriptable object that represents an item that is described by a single or multiple numeric values.
    /// </summary>
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
                    return new Food(values,sprite, name, ID , type);
                    break;
                }
                case ItemType.Potion:
                {
                    return new Potion(values,sprite, name , ID, type);
                    break;
                }
                default:
                    return null;
            }
        }

        public override Item GetItem( List<ValueType> initValues)
        {
            switch (type)
            {
                case ItemType.Food:
                {
                    return new Food(values,sprite, name , ID, base.type);
                    break;
                }
                case ItemType.Potion:
                {
                    return new Potion(values,sprite, name , ID, base.type);
                    break;
                }
                default:
                    return null;
            }
        }
    }
}