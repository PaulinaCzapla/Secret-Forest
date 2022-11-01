using InteractableItems.CollectableItems.Items;

namespace PlayerInteractions
{
    public class PlayerEquipment
    {
        public Armor Helmet { private get; set; }
        public Armor BreastPlate {private  get; set; }
        public Armor ShinGuards {private  get; set; }
        public Armor Boots {private  get; set; }
        
        public Weapon Bow {private  get; set; }
        public Weapon Sword {private  get; set; }
        
        
        public float GetWeaponCurrentDamage(ItemType type)
        {
            if (type == ItemType.Bow)
                return  Bow?.GetTypeValue(ItemValueType.Damage) ?? 0;
            if (type == ItemType.WhiteWeapon)
                return  Sword?.GetTypeValue(ItemValueType.Damage) ?? 0;
            return 0;
        }
        
        public float GetWeaponCurrentCriticalChance(ItemType type)
        {
            if (type == ItemType.Bow)
                return Bow?.GetTypeValue(ItemValueType.CriticalDamageChance) ?? 0;
            if (type == ItemType.WhiteWeapon)
                return Sword?.GetTypeValue(ItemValueType.CriticalDamageChance)?? 0;
            return 0;
        }

        public float GetArmorCurrentDefense(ItemType type)
        {
            switch (type)
            {
                case ItemType.Boots:
                    return Boots?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.Breastplate:
                    return BreastPlate?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.Helmet:
                    return Helmet?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.ShinGuards:
                    return ShinGuards?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                default:
                    return 0;
            }
        }
        
        public float GetArmorDodgeChance(ItemType type)
        {
            switch (type)
            {
                case ItemType.Boots:
                    return Boots?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                case ItemType.Breastplate:
                    return BreastPlate?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                case ItemType.Helmet:
                    return Helmet?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                case ItemType.ShinGuards:
                    return ShinGuards?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                default:
                    return 0;
            }
        }
    }
}