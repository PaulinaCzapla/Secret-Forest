using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ActionItem", menuName = "ScriptableObjects/ActionItem", order = 0)]
    public class ActionItemSO : ItemSO
    {
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