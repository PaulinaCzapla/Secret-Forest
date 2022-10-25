using UnityEngine;

namespace InteractableItems.CollectableItems
{
    public class HealthPotion : Item, IUsable
    {
        public float Value => value;
        [SerializeField] private float value;
        public override void Collect()
        {
            
        }

        public void Use()
        {
            
        }
    }
}