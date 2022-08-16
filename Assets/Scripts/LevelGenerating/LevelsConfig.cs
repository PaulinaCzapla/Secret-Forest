using System;
using System.Collections.Generic;
using Glades;
using UnityEngine;

namespace LevelGenerating
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig")]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private List<LevelInfo> levelInfo;

        public LevelAttributes GetLevelAttributes(int currentLevel)
        {
            foreach (var info in levelInfo)
            {
                if (info.maxLevelNum > currentLevel)
                    return info.levelAttributes;
            }

            return levelInfo[levelInfo.Count - 1].levelAttributes;
        }
    }

    [Serializable]
    public struct LevelAttributes
    {
        public List<GladeType> availableRoomTypes;
        [Range(2, 100)] public int minRoomsNum ;
        [Range(2, 100)] public int maxRoomsNum;
        [Range(0, 10)] public int roomsDifficultyLevel;
    }
    
    [Serializable] 
    public struct LevelInfo
    {
        [Tooltip("The maximum level for which these rules will be applied. ")]
        public int maxLevelNum;
        public LevelAttributes levelAttributes;
    }
}