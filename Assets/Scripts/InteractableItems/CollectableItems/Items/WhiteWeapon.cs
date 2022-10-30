using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class WhiteWeapon : Item, IEquipable
    {
        public WhiteWeapon(Sprite sprite, string name) : base(sprite, name)
        {
        }

        public override void Collect()
        {
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