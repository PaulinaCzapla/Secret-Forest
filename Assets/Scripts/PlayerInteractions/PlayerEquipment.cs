using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.Types;
using UI.Eq;
using UI.Events;
using UnityEngine;

namespace PlayerInteractions
{
    /// <summary>
    /// A class that represents player's equipment.
    /// </summary>
    public class PlayerEquipment
    {
        private Dictionary<ItemType, WearableItem> _equipment;
        private PlayerStatsSO _playerStats;

        public PlayerEquipment(List<Item> items)
        {
            ItemType[] types =
            {
                ItemType.Boots, ItemType.Breastplate, ItemType.Helmet, ItemType.ShinGuards, ItemType.Bow,
                ItemType.WhiteWeapon
            };

            _equipment = new Dictionary<ItemType, WearableItem>();

            if (items != null)
                foreach (var item in items)
                {
                    if (!_equipment.ContainsKey(item.Type))
                    {
                        _equipment.Add(item.Type, (WearableItem) item);
                    }
                }

            foreach (var type in types)
            {
                if (!_equipment.ContainsKey(type))
                    _equipment.Add(type, null);
            }

            _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");
            RecalculateValues();
        }

        /// <summary>
        /// Returns currently equipped item of a given type. 
        /// </summary>
        /// <param name="type"> Item type </param>
        /// <returns> WearableItem object. If player doesn't have equipped item the method returns null. </returns>
        public WearableItem GetCurrentEquippedItem(ItemType type)
        {
            return _equipment[type];
        }

        /// <summary>
        /// Recalculates current player stats considering current equipment.
        /// </summary>
        private void RecalculateValues()
        {
            _playerStats.CurrentBowDamage = (_equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.Damage) ?? 0);
            _playerStats.CurrentSwordDamage =
                (_equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.Damage) ?? 0);

            _playerStats.CurrentCriticalBow =
                (_equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.CriticalDamageChance) ?? 0);
            _playerStats.CurrentCriticalSword =
                (_equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.CriticalDamageChance) ?? 0);

            _playerStats.CurrentDefense = (_equipment[ItemType.Boots]?.GetTypeValue(ItemValueType.Defence) ??
                                           0) + (_equipment[ItemType.Breastplate]
                                                     ?.GetTypeValue(ItemValueType.Defence) ??
                                                 0) + (_equipment[ItemType.Helmet]
                                                           ?.GetTypeValue(ItemValueType.Defence) ??
                                                       0) + (_equipment[ItemType.ShinGuards]
                ?.GetTypeValue(ItemValueType.Defence) ?? 0);

            _playerStats.CurrentDodgeChance = (_equipment[ItemType.Boots]?.GetTypeValue(ItemValueType.DodgeChance) ??
                                               0) + (_equipment[ItemType.Breastplate]
                                                         ?.GetTypeValue(ItemValueType.DodgeChance) ??
                                                     0) + (_equipment[ItemType.Helmet]
                                                               ?.GetTypeValue(ItemValueType.DodgeChance) ??
                                                           0) + (_equipment[ItemType.ShinGuards]
                ?.GetTypeValue(ItemValueType.DodgeChance) ?? 0);

            _playerStats.currentMaxHealthValue = _playerStats.CurrentDefense;
            UIStaticEvents.InvokeUpdateHealthUI();
            InventoryUIStaticEvents.InvokeRefreshInventoryStatsUI();
        }

        /// <summary>
        /// Equips given Item and returns the previous one.
        /// </summary>
        /// <param name="item"> An item to equip.</param>
        /// <returns> Previously equipped item. If there wasn't any it returns null. </returns>
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

        /// <summary>
        /// Unequips given item.
        /// </summary>
        /// <param name="item"> An item to unequip. </param>
        /// <returns> Previously equipped item. </returns>
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

        /// <summary>
        /// Gets currently equipped weapon's damage.
        /// </summary>
        /// <param name="type"> Item type. </param>
        /// <returns> Damage value. </returns>
        public float GetWeaponCurrentDamage(ItemType type)
        {
            if (type == ItemType.Bow)
                return _equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.Damage) ?? 0;
            if (type == ItemType.WhiteWeapon)
                return _equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.Damage) ?? 0;
            return 0;
        }

        /// <summary>
        /// Gets currently equipped weapon's critical damage chance.
        /// </summary>
        /// <param name="type"> Item type. </param>
        /// <returns> Critical damage chance value. </returns>
        public float GetWeaponCurrentCriticalChance(ItemType type)
        {
            if (type == ItemType.Bow)
                return _equipment[ItemType.Bow]?.GetTypeValue(ItemValueType.CriticalDamageChance) ?? 0;
            if (type == ItemType.WhiteWeapon)
                return _equipment[ItemType.WhiteWeapon]?.GetTypeValue(ItemValueType.CriticalDamageChance) ??
                       0;
            return 0;
        }
        /// <summary>
        /// Gets currently equipped weapon's defense value.
        /// </summary>
        /// <param name="type"> Item type. </param>
        /// <returns> Defense value. </returns>
        public float GetArmorCurrentDefense(ItemType type)
        {
            switch (type)
            {
                case ItemType.Boots:
                    return _equipment[ItemType.Boots]?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.Breastplate:
                    return _equipment[ItemType.Breastplate]?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.Helmet:
                    return _equipment[ItemType.Helmet]?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                case ItemType.ShinGuards:
                    return _equipment[ItemType.ShinGuards]?.GetTypeValue(ItemValueType.Defence) ?? 0;
                    break;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// Gets currently equipped armour's dodge chance.
        /// </summary>
        /// <param name="type"> Item type. </param>
        /// <returns> Dodge chance value. </returns>
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