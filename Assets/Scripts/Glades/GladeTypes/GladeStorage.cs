using System;
using System.Collections.Generic;
using InteractableItems;
using InteractableItems.CollectableItems;
using LevelGenerating;
using RandomGenerators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Glades.GladeTypes
{
    public class GladeStorage : BaseGlade
    {
        [Serializable]
        internal struct StorageConfiguration
        {
            public List<Chest> chests;
            public GameObject parentGameObject;
        }
        
        [SerializeField] private LevelsConfigSO config;
        [SerializeField] private List<StorageConfiguration> configurations;

        private int _currentConfiguration;
        public override void Initialize()
        {
            ResetGlade();
            _currentConfiguration = Random.Range(0, configurations.Count);
            configurations[_currentConfiguration].parentGameObject.SetActive(true);
            
            var itemsProbs = config.GetChestItemsProbabilities();
            List<Tuple<Item, float>> itemsWithProbabilities = new List<Tuple<Item, float>>(itemsProbs.Count);

            foreach (var item in itemsProbs)
                itemsWithProbabilities.Add(new Tuple<Item, float>(item.item, item.probability));

            Tuple<int, float>[] itemsCount = new[]
            {
                new Tuple<int, float>(1, 0.4f),
                new Tuple<int, float>(2, 0.4f),
                new Tuple<int, float>(3, 0.15f),
                new Tuple<int, float>(4, 0.05f)
            };

            foreach (var chest in configurations[_currentConfiguration].chests)
            {
                chest.Init(RandomWithProbabilityGenerator.
                    GetRandom(itemsWithProbabilities, 
                        RandomWithProbabilityGenerator.GetRandom(itemsCount)));
            }
        }

        private void ResetGlade()
        {
            foreach (var conf in configurations)
            {
                conf.parentGameObject.SetActive(false);
            }
        }
    }
}