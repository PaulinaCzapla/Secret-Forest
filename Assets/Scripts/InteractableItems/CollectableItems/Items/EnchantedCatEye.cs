using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class EnchantedCatEye : Item, IUsable
    {
        public EnchantedCatEye(Sprite sprite, string name) : base(sprite, name)
        {
        }

        public override bool Collect()
        {
            throw new System.NotImplementedException();
        }

        public override string GetString()
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}