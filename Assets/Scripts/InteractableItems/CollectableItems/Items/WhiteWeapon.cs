using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems
{
    public class WhiteWeapon : Item, IEquipable
    {
        public WhiteWeapon(Sprite sprite, string name) : base(sprite, name)
        {
        }

        public override void Collect()
        {
        }

        public void Equip()
        {
            throw new System.NotImplementedException();
        }
    }
}