using System;
using System.Collections.Generic;
using InteractableItems;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.ScriptableObjects;
using LevelGenerating;
using RandomGenerators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Glades.GladeTypes
{
    public sealed class GladeStorage : BaseGlade
    {
        [Serializable]
        internal struct StorageConfiguration
        {
            public List<Chest> chests;
            public GameObject parentGameObject;
        }
        
        [SerializeField] private LevelsConfigSO config;
        [SerializeField] private new List<StorageConfiguration> configurations;

        private int _currentConfiguration;
        public override void Initialize()
        {
            ResetGlade();
            _currentConfiguration = Random.Range(0, configurations.Count);
            configurations[_currentConfiguration].parentGameObject.SetActive(true);
            
            var itemsProbs = config.GetChestItemsProbabilities();
            List<Tuple<Item, float>> itemsWithProbabilities = new List<Tuple<Item, float>>(itemsProbs.Count);

            Tuple<int, float>[] itemsCount = new[]
            {
                new Tuple<int, float>(0, 0.2f),
                new Tuple<int, float>(1, 0.37f),
                new Tuple<int, float>(2, 0.3f),
                new Tuple<int, float>(3, 0.1f),
                new Tuple<int, float>(4, 0.03f)
            };
            
            foreach (var chest in configurations[_currentConfiguration].chests)
            {
                itemsWithProbabilities.Clear();

                foreach (var item in itemsProbs)
                    itemsWithProbabilities.Add(new Tuple<Item, float>(item.item.GetRandom(), item.probability));
                
                chest.Init(RandomElementsGenerator.
                    GetRandom(itemsWithProbabilities, 
                        RandomElementsGenerator.GetRandom(itemsCount)));
            }
        }

        protected override void ResetGlade()
        {
            foreach (var conf in configurations)
            {
                conf.parentGameObject.SetActive(false);
            }
        }
    }
}