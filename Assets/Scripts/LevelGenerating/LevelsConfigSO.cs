using System;
using System.Collections.Generic;
using GameManager;
using Glades;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.ScriptableObjects;
using RandomGenerators;
using UnityEngine;

namespace LevelGenerating
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig")]
    public class LevelsConfigSO : ScriptableObject
    {
        [Serializable]
        private class ValueMultipliersForLevel
        {
            public int maxLevelNum;
            public float armorDefenseMultiplier = 1f;
            public float armorDodgeMultiplier = 1f;
            public float weaponDamageMultiplier = 1f;
            public float weaponCriticalMultiplier = 1f;
        }

        [Header("Level generator settings")] [SerializeField]
        private List<LevelInfo> levelInfo;

        [Header("Chest items spawn probabilities")] [SerializeField]
        private List<ItemsForLevel> itemsInfo;

        [Header("Food items spawn probabilities")] [SerializeField]
        private List<ItemsForLevel> foodItemsInfo;

        [Header("Chest items spawn probabilities")] [SerializeField]
        private List<ItemsForLevel> weaponItemsInfo;

        [Header("Weapons and armor multipliers")] [SerializeField]
        private List<ValueMultipliersForLevel> valueMultipliers;

        public LevelAttributes GetLevelAttributes()
        {
            LevelAttributes attributes = null;
            float prevMaxLevelNum = -1;

            foreach (var info in levelInfo)
            {
                if (GameManager.GameManager.GetInstance().CurrentLevelNum > prevMaxLevelNum &&
                    GameManager.GameManager.GetInstance().CurrentLevelNum <= info.maxLevelNum)
                {
                    attributes = info.levelAttributes;
                    break;
                }

                prevMaxLevelNum = info.maxLevelNum;
            }

            if (attributes == null)
                attributes = levelInfo[levelInfo.Count - 1].levelAttributes;

            return attributes;
        }

        public List<ItemsForLevel.ItemProbability> GetChestItemsProbabilities()
        {
            List<ItemsForLevel.ItemProbability> items = null;
            float prevMaxLevelNum = -1;
            
            foreach (var info in itemsInfo)
            {
                if (GameManager.GameManager.GetInstance().CurrentLevelNum > prevMaxLevelNum &&
                    GameManager.GameManager.GetInstance().CurrentLevelNum <= info.maxLevelNum)
                {
                    items = info.items;
                    break;
                }

                prevMaxLevelNum = info.maxLevelNum;
            }

            if (items == null)
                items = itemsInfo[itemsInfo.Count - 1].items;

            return items;
        }

        public List<ItemsForLevel.ItemProbability> GetFoodItemsProbabilities()
        {
            List<ItemsForLevel.ItemProbability> items = null;
            float prevMaxLevelNum = -1;
            foreach (var info in foodItemsInfo)
            {
                if (GameManager.GameManager.GetInstance().CurrentLevelNum > prevMaxLevelNum &&
                    GameManager.GameManager.GetInstance().CurrentLevelNum <= info.maxLevelNum)
                {
                    items = info.items;
                    break;
                }

                prevMaxLevelNum = info.maxLevelNum;
            }

            if (items == null)
                items = foodItemsInfo[foodItemsInfo.Count - 1].items;

            return items;
        }

        public List<ItemsForLevel.ItemProbability> GetWeaponItemsProbabilities()
        {
            List<ItemsForLevel.ItemProbability> items = null;
            float prevMaxLevelNum = -1;
            
            foreach (var info in weaponItemsInfo)
            {
                if (GameManager.GameManager.GetInstance().CurrentLevelNum > prevMaxLevelNum &&
                    GameManager.GameManager.GetInstance().CurrentLevelNum <= info.maxLevelNum)
                {
                    items = info.items;
                    break;
                }

                prevMaxLevelNum = info.maxLevelNum;
            }

            if (items == null)
                items = weaponItemsInfo[weaponItemsInfo.Count - 1].items;
            return items;
        }

        public float GetValueMultiplier(ItemValueType type)
        {
            ValueMultipliersForLevel multiplierForLevel = null;
            float prevMaxLevelNum = -1;
            
            foreach (var multiplier in valueMultipliers)
            {
                if (GameManager.GameManager.GetInstance().CurrentLevelNum > prevMaxLevelNum &&
                    GameManager.GameManager.GetInstance().CurrentLevelNum <= multiplier.maxLevelNum)
                {
                    multiplierForLevel = multiplier;
                    break;
                }

                prevMaxLevelNum = multiplier.maxLevelNum;
            }

            if (multiplierForLevel == null)
            {
                // TODO: value that depends on player stats
                multiplierForLevel = valueMultipliers[valueMultipliers.Count - 1];
            }

            float value = 1;

            if (multiplierForLevel != null)
                switch (type)
                {
                    case ItemValueType.Damage:
                        value = multiplierForLevel.weaponDamageMultiplier;
                        break;
                    case ItemValueType.Defence:
                        value = multiplierForLevel.armorDefenseMultiplier;
                        break;
                    case ItemValueType.DodgeChance:
                        value = multiplierForLevel.armorDodgeMultiplier;
                        break;
                    case ItemValueType.CriticalDamageChance:
                        value = multiplierForLevel.weaponCriticalMultiplier;
                        break;
                }

            return value;
        }
    }

    [Serializable]
    public class ItemsForLevel
    {
        [Serializable]
        public struct ItemProbability
        {
            [Range(0, 1)] public float probability;
            public ItemSO item;
        }

        public int maxLevelNum;
        public List<ItemProbability> items;
    }
}