using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ActionItem", menuName = "ScriptableObjects/ActionItem", order = 0)]
    public class ActionItemSO : ItemSO
    {
        public override Item GetRandom()
        {
            return GetItemOfType(type);
        }

        public override Item GetItem(ItemType initType, List<ValueType> initValues)
        {
            return GetItemOfType(initType);
        }

        private Item GetItemOfType(ItemType initType)
        {
            switch (initType)
            {
                case ItemType.CatEyeNear:
                {
                    return new AdjacentGladesUncoverer(sprite, name, ID, initType);
                    break;
                }
                case ItemType.EnchantedCatEye:
                {
                    return new RandomGladesUncoverer(sprite, name, ID, initType);
                    break;
                }
                case ItemType.Compass:
                {
                    return new ExitUncoverer(sprite, name, ID, initType);

                    break;
                }
                case ItemType.AddSlot:
                {
                    return new AddSlot(sprite, name, ID, initType);
                }
                default:
                    return null;
            }
        }
    }
}