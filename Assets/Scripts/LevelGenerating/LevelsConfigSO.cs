using System;
using System.Collections.Generic;
using Glades;
using RandomGenerators;
using UnityEngine;

namespace LevelGenerating
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "ScriptableObjects/LevelsConfig")]
    public class LevelsConfigSO : ScriptableObject
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
    public class LevelAttributes
    {
        public List<GladeTypeWithProbability> availableGladeTypes;
        [Range(2, 100)] public int minRoomsNum ;
        [Range(2, 100)] public int maxRoomsNum;
        [Range(0, 10)] public int roomsDifficultyLevel;

        private Tuple<GladeType, float>[] _types ;
        public GladeType GetRandomGladeType()
        {
            if (_types == null || _types.Length == 0)
            {
                _types = new Tuple<GladeType, float>[availableGladeTypes.Count];

                int i = 0;
                foreach (var type in availableGladeTypes)
                {
                    _types[i] = new Tuple<GladeType, float>(type.type, type.probability);
                    i++;
                }
            }

            return RandomWithProbabilityGenerator.GetRandom(_types);
        }
    }

    [Serializable]
    public struct GladeTypeWithProbability
    {
        public GladeType type;
        [Range(0,1)]
        public float probability;
    }
    [Serializable] 
    public struct LevelInfo
    {
        [Tooltip("The maximum level for which these rules will be applied. ")]
        public int maxLevelNum;
        public LevelAttributes levelAttributes;
    }
}