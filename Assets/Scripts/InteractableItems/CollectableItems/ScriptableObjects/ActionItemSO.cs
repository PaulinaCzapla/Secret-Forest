using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.Types;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    /// <summary>
    /// A scriptable object that represents action item that doesn't have any value.
    /// </summary>
    [CreateAssetMenu(fileName = "ActionItem", menuName = "ScriptableObjects/ActionItem", order = 0)]
    public class ActionItemSO : ItemSO
    {
        /// <summary>
        /// Returns proper Item object.
        /// </summary>
        public override Item GetItem()
        {
            switch (type)
            {
                case ItemType.CatEyeNear:
                {
                    return new AdjacentGladesUncoverer(sprite, name, ID, type);
                    break;
                }
                case ItemType.EnchantedCatEye:
                {
                    return new RandomGladesUncoverer(sprite, name, ID, type);
                    break;
                }
                case ItemType.Compass:
                {
                    return new ExitUncoverer(sprite, name, ID, type);
                    break;
                }
                case ItemType.AddSlot:
                {
                    return new AddSlot(sprite, name, ID, type);
                }
                default:
                    return null;
            }
        }

        public override Item GetItem(List<ValueType> initValues)
        {
            return GetItem();
        }
        
    }
}