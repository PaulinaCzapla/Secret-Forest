using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ActionItem", menuName = "ScriptableObjects/ActionItem", order = 0)]
    public class ActionItemSO : ItemSO
    {
        public override Item GetRandom()
        {
            switch (type)
            {
                case ItemType.CatEyeNear:
                {
                    return new AdjacentGladesUncoverer(sprite, name);
                    break;
                }
                case ItemType.EnchantedCatEye:
                {
                    return new RandomGladesUncoverer(sprite, name);
                    break;
                }
                case ItemType.Compass:
                {
                    return new ExitUncoverer(sprite, name);

                    break;
                }
                default:
                    return null;
            }
        }
    }
}