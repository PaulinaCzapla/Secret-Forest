using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.ScriptableObjects;
using UnityEngine;

namespace LevelGenerating
{
    /// <summary>
    /// A scriptable object that is used as a configuration file. It contains data required to generate levels and items.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig")]
    public class LevelsConfigSO : ScriptableObject
    {

        [Header("Level generator settings")] [SerializeField]
        private List<LevelInfo> levelInfo;

        [Header("Chest items spawn probabilities")] [SerializeField]
        private List<ItemsForLevel> itemsInfo;

        /// <summary>
        /// Returns and prepares LevelAttributes object that is dependent on current game level.
        /// </summary>
        /// <returns> LevelAttributes object. </returns>
        public LevelAttributes GetLevelAttributes()
        {
            LevelAttributes attributes = null;
            float prevMaxLevelNum = -1;

            foreach (var info in levelInfo)
            {
                if (GameManager.GameController.GetInstance().CurrentLevelNum > prevMaxLevelNum &&
                    GameManager.GameController.GetInstance().CurrentLevelNum <= info.maxLevelNum)
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

        /// <summary>
        /// Prepares a list with items and it's probabilities that can be drawn in chests. It's dependent on the current game level.
        /// </summary>
        /// <returns> List with items and it's probabilities. </returns>
        public List<ItemsProbability> GetChestItemsProbabilities()
        {
            List<ItemsProbability> items = null;
            float prevMaxLevelNum = -1;

            foreach (var info in itemsInfo)
            {
                if (GameManager.GameController.GetInstance().CurrentLevelNum > prevMaxLevelNum &&
                    GameManager.GameController.GetInstance().CurrentLevelNum <= info.maxLevelNum)
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
    }

    /// <summary>
    /// A struct that contains an item and it's probability.
    /// </summary>
    [Serializable]
    public struct ItemProbability
    {
        [Range(0, 1)] public float probability;
        public ItemSO item;
    }

    /// <summary>
    /// A struct that contains an ItemGroupSO and it's probability.
    /// </summary>
    [Serializable]
    public struct ItemsProbability
    {
        [Range(0, 1)] public float probability;
        public ItemGroupSO items;
    }
    
    /// <summary>
    /// A struct that represents items that can exist to the given maximum level.
    /// </summary>
    [Serializable]
    public class ItemsForLevel
    {
        public int maxLevelNum;
        public List<ItemsProbability> items;
    }
}