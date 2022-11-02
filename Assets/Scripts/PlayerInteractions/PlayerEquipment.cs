using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UI.Eq;
using UnityEngine;

namespace PlayerInteractions
{
    public class PlayerEquipment
    {
        // private Armor _helmet;
        // private Armor _breastPlate;
        // private Armor _shinGuards;
        // private Armor _boots;
        //
        // private Weapon _bow;
        // private Weapon _sword;

        private Dictionary<ItemType, WearableItem> _equipment;
        private PlayerStatsSO _playerStats;

        public PlayerEquipment()
        {
            _equipment = new Dictionary<ItemType, WearableItem>();
            _equipment.Add(ItemType.Boots, null);
            _equipment.Add(ItemType.Breastplate, null);
            _equipment.Add(ItemType.Helmet, null);
            _equipment.Add(ItemType.ShinGuards, null);
            _equipment.Add(ItemType.Bow, null);
            _equipment.Add(ItemType.WhiteWeapon, null);

            _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");
            RecalculateValues();
        }

        public Item GetCurrentEquippedItem(ItemType type)
        {
            return _equipment[type];
        }

        private void RecalculateValues()
        {
            _playerStats.currentDamage = (_equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.Damage) ??
                           0) + (_equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.Damage) ?? 0) + _playerStats.currentBaseDamage;

            _playerStats.currentCritical = (_equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.CriticalDamageChance) ?? 0) +
                (_equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.CriticalDamageChance) ?? 0);

            _playerStats.currentDefense = (_equipment[ItemType.Boots]?.GetTypeValue(ItemValueType.Defence) ??
                                          0) + (_equipment[ItemType.Breastplate]?.GetTypeValue(ItemValueType.Defence) ??
                                          0) + (_equipment[ItemType.Helmet]?.GetTypeValue(ItemValueType.Defence) ??
                                          0) + (_equipment[ItemType.ShinGuards]?.GetTypeValue(ItemValueType.Defence) ?? 0);
            
            _playerStats.currentDodgeChance = (_equipment[ItemType.Boots]?.GetTypeValue(ItemValueType.DodgeChance) ??
                                          0) + (_equipment[ItemType.Breastplate]?.GetTypeValue(ItemValueType.DodgeChance) ??
                                          0) + (_equipment[ItemType.Helmet]?.GetTypeValue(ItemValueType.DodgeChance) ??
                                          0) + (_equipment[ItemType.ShinGuards]?.GetTypeValue(ItemValueType.DodgeChance) ?? 0);
            
            InventoryUIStaticEvents.InvokeRefreshInventoryStatsUI();
        }

        public Item Equip(Item item)
        {
            Item prevItem = null;

            if (item is WearableItem wearable)
            {
                if (wearable.Type == ItemType.Bow)
                {
                    prevItem = _equipment[ItemType.Bow];
                    _equipment[ItemType.Bow] = wearable;
                }

                if (wearable.Type == ItemType.WhiteWeapon)
                {
                    prevItem = _equipment[ItemType.WhiteWeapon];
                    _equipment[ItemType.WhiteWeapon] = wearable;
                }

                if (wearable.Type == ItemType.Boots)
                {
                    prevItem = _equipment[ItemType.Boots];
                    _equipment[ItemType.Boots] = wearable;
                }

                if (wearable.Type == ItemType.Helmet)
                {
                    prevItem = _equipment[ItemType.Helmet];
                    _equipment[ItemType.Helmet] = wearable;
                }

                if (wearable.Type == ItemType.Breastplate)
                {
                    prevItem = _equipment[ItemType.Breastplate];
                    _equipment[ItemType.Breastplate] = wearable;
                }

                if (wearable.Type == ItemType.ShinGuards)
                {
                    prevItem = _equipment[ItemType.ShinGuards];
                    _equipment[ItemType.ShinGuards] = wearable;
                }
            }

            RecalculateValues();
            return prevItem;
        }

        public Item Unequip(Item item)
        {
            Item prevItem = null;

            if (item is WearableItem wearable)
            {
                if (wearable.Type == ItemType.Bow)
                {
                    prevItem = _equipment[ItemType.Bow];
                    _equipment[ItemType.Bow] = null;
                }

                if (wearable.Type == ItemType.WhiteWeapon)
                {
                    prevItem = _equipment[ItemType.WhiteWeapon];
                    _equipment[ItemType.WhiteWeapon] = null;
                }

                if (wearable.Type == ItemType.Boots)
                {
                    prevItem = _equipment[ItemType.Boots];
                    _equipment[ItemType.Boots] = null;
                }

                if (wearable.Type == ItemType.Helmet)
                {
                    prevItem = _equipment[ItemType.Helmet];
                    _equipment[ItemType.Helmet] = null;
                }

                if (wearable.Type == ItemType.Breastplate)
                {
                    prevItem = _equipment[ItemType.Breastplate];
                    _equipment[ItemType.Breastplate] = null;
                }

                if (wearable.Type == ItemType.ShinGuards)
                {
                    prevItem = _equipment[ItemType.ShinGuards];
                    _equipment[ItemType.ShinGuards] = null;
                }
            }

            RecalculateValues();
            return prevItem;
        }

        public float GetWeaponCurrentDamage(ItemType type)
        {
            if (type == ItemType.Bow)
                return  _equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.Damage) ?? 0;
            if (type == ItemType.WhiteWeapon)
                return _equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.Damage) ?? 0;
            return 0;
        }

        public float GetWeaponCurrentCriticalChance(ItemType type)
        {
            if (type == ItemType.Bow)
                return  _equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.CriticalDamageChance) ?? 0;
            if (type == ItemType.WhiteWeapon)
                return _equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.CriticalDamageChance) ??
                       0;
            return 0;
        }

        public float GetArmorCurrentDefense(ItemType type)
        {
            switch (type)
            {
                case ItemType.Boots:
                    return  _equipment[ItemType.Boots]?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.Breastplate:
                    return _equipment[ItemType.Breastplate]?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.Helmet:
                    return  _equipment[ItemType.Helmet]?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.ShinGuards:
                    return _equipment[ItemType.ShinGuards]?.GetTypeValue(ItemValueType.Defence) ?? 0;
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
                    return ((Armor) _equipment[ItemType.Boots])?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                case ItemType.Breastplate:
                    return ((Armor) _equipment[ItemType.Breastplate])?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                case ItemType.Helmet:
                    return ((Armor) _equipment[ItemType.Helmet])?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                case ItemType.ShinGuards:
                    return ((Armor) _equipment[ItemType.ShinGuards])?.GetTypeValue(ItemValueType.DodgeChance) ?? 0;
                    break;
                default:
                    return 0;
            }
        }
    }
}