using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems
{
    public class Food : Item, IUsable
    {
        [SerializeField] private float value = 5;
        
        public override void Collect()
        {
            //add to inventory
            // if collected
            StorageUI.Instance.ItemCollected(this);
        }

        public void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}