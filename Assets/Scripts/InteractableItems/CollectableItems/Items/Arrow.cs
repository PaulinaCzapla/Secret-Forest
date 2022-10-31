using InteractableItems.CollectableItems.Interfaces;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class Arrow : Item, IEquippable
    {      public Arrow(Sprite sprite, string name) : base(sprite, name)
             {
             }
        public override void Collect()
        {
            throw new System.NotImplementedException();
        }

        public override string GetString()
        {
            throw new System.NotImplementedException();
        }

        public void Equip()
        {
            throw new System.NotImplementedException();
        }

  
    }
}