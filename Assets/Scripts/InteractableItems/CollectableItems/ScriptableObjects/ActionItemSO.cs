using InteractableItems.CollectableItems.Items;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    public class ActionItemSO : ItemSO
    {
        public override Item GetItem()
        {
            switch (type)
            {
                case ItemType.CatEyeNear:
                {
                    return new CatEyeNear(sprite, name);
                    break;
                }
                case ItemType.EnchantedCatEye:
                {
                    return new EnchantedCatEye(sprite, name);
                    break;
                }
                case ItemType.Compass:
                {
                    return new Compass(sprite, name);

                    break;
                }
                default:
                    return null;
            }
        }
    }
}