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
        [Header("Level generator settings")]
        [SerializeField] private List<LevelInfo> levelInfo;

        [Header("Chest items spawn probabilities")] 
        [SerializeField] private List<ItemsForLevel> itemsInfo;
        
        [Header("Food items spawn probabilities")] 
        [SerializeField] private List<ItemsForLevel> foodItemsInfo;
        
        [Header("Chest items spawn probabilities")] 
        [SerializeField] private List<ItemsForLevel> weaponItemsInfo;
        
        public LevelAttributes GetLevelAttributes()
        {
            foreach (var info in levelInfo)
            {
                if (info.maxLevelNum > GameStats.GetInstance().CurrentLevelNum)
                    return info.levelAttributes;
            }

            return levelInfo[levelInfo.Count - 1].levelAttributes;
        }
        
        public List<ItemsForLevel.ItemProbability> GetChestItemsProbabilities()
        {
            foreach (var info in itemsInfo)
            {
                if (info.maxLevelNum > GameStats.GetInstance().CurrentLevelNum)
                    return info.items;
            }

            return itemsInfo[levelInfo.Count - 1].items;
        }
        
        public List<ItemsForLevel.ItemProbability> GetFoodItemsProbabilities()
        {
            foreach (var info in foodItemsInfo)
            {
                if (info.maxLevelNum > GameStats.GetInstance().CurrentLevelNum)
                    return info.items;
            }

            return foodItemsInfo[levelInfo.Count - 1].items;
        }
        
        public List<ItemsForLevel.ItemProbability> GetWeaponItemsProbabilities()
        {
            foreach (var info in  weaponItemsInfo)
            {
                if (info.maxLevelNum > GameStats.GetInstance().CurrentLevelNum)
                    return info.items;
            }

            return  weaponItemsInfo[levelInfo.Count - 1].items;
        }
    }

    [Serializable]
    public class ItemsForLevel
    { 
        [Serializable]
        public struct ItemProbability
        {
            [Range(0,1)]
            public float probability;
            public ItemSO item;
        }
        
        public int maxLevelNum;
        public List<ItemProbability> items;
    }
    

}