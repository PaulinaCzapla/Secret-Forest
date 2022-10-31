using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class HealthPotion : Item, IUsable
    {
        private float _value;

        public HealthPotion(float value, Sprite sprite, string name) : base(sprite, name)
        {
            _value = value;
        }
        

        public override bool Collect()
        {
            if (InventoryUI.Instance.ItemCollected(this))
            {
                onCollected?.Invoke();
                return true;
            }

            return false;
        }
        public override string GetString()
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
        }
    }
}