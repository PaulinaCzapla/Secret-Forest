using System;
using System.Collections.Generic;
using GameManager;
using Glades;
using RandomGenerators;
using UnityEngine;

namespace LevelGenerating
{
    /// <summary>
    /// A class that contains informations required to generate a level.
    /// </summary>
    [Serializable]
    public class LevelAttributes
    {
        public List<GladeTypeWithProbability> availableGladeTypes;
        [Range(2, 100)] public int minRoomsNum ;
        [Range(2, 100)] public int maxRoomsNum;
        [Range(0, 100)] public int roomsConnectionChance;

        private Tuple<GladeType, float>[] _types ;
        
        /// <summary>
        /// Returns random glade type considering it's probabilities. The output is also dependent on player's current health.
        /// </summary>
        /// <returns> Glade type. </returns>
        public GladeType GetRandomGladeType()
        {
            if (_types == null || _types.Length == 0)
            {
                _types = new Tuple<GladeType, float>[availableGladeTypes.Count];

                int i = 0;
                foreach (var typeGlade in availableGladeTypes)
                {
                    _types[i] = new Tuple<GladeType, float>(typeGlade.type, typeGlade.probability);
                    i++;
                }
            }

            var type = RandomElementsGenerator.GetRandom(_types);
            if (type == GladeType.FightHard && GameController.GetInstance().PlayerStats.currentHealthValue <=
                0.2 * GameController.GetInstance().PlayerStats.currentMaxHealthValue)
            {
                var res = RandomElementsGenerator.GetRandom(.5f, .5f);
            
                if (res)
                {
                    do
                    {
                        type = RandomElementsGenerator.GetRandom(_types);
                    } while (type == GladeType.FightHard);
                }
            }

            return type;
        }
    }

    /// <summary>
    /// A struct that represents glade type with it's probability chance.
    /// </summary>
    [Serializable]
    public struct GladeTypeWithProbability
    {
        public GladeType type;
        [Range(0,1)]
        public float probability;
    }
    
    /// <summary>
    /// A struct that represents information about level.
    /// </summary>
    [Serializable] 
    public struct LevelInfo
    {
        [Tooltip("The maximum level for which these rules will be applied. ")]
        public int maxLevelNum;
        public LevelAttributes levelAttributes;
    }
}