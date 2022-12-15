using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.ScriptableObjects;
using UnityEngine;

namespace LevelGenerating
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig")]
    public class LevelsConfigSO : ScriptableObject
    {

        [Header("Level generator settings")] [SerializeField]
        private List<LevelInfo> levelInfo;

        [Header("Chest items spawn probabilities")] [SerializeField]
        private List<ItemsForLevel> itemsInfo;

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

    [Serializable]
    public struct ItemProbability
    {
        [Range(0, 1)] public float probability;
        public ItemSO item;
    }

    [Serializable]
    public struct ItemsProbability
    {
        [Range(0, 1)] public float probability;
        public ItemGroupSO items;
    }
    
    [Serializable]
    public class ItemsForLevel
    {
        public int maxLevelNum;
        public List<ItemsProbability> items;
    }
}