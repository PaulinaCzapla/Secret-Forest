using System;
using System.Collections.Generic;
using InteractableItems;
using InteractableItems.CollectableItems;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.InteractableItems;
using InteractableItems.CollectableItems.ScriptableObjects;
using LevelGenerating;
using RandomGenerators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Glades.GladeTypes
{
    /// <summary>
    /// Represents a glade with chests.
    /// </summary>
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

        /// <summary>
        /// Initializes glade by drawing a configuration and initializing chests.
        /// </summary>
        public override void Initialize()
        {
            ResetGlade();

            Tuple<int, float>[] itemsCountProbabilities = new[]
            {
                new Tuple<int, float>(0, 0.2f),
                new Tuple<int, float>(1, 0.37f),
                new Tuple<int, float>(2, 0.3f),
                new Tuple<int, float>(3, 0.1f),
                new Tuple<int, float>(4, 0.03f)
            };
            
            _currentConfiguration = Random.Range(0, configurations.Count);
            configurations[_currentConfiguration].parentGameObject.SetActive(true);

            var itemsProbs = config.GetChestItemsProbabilities();
            List<Tuple<Item, float>> itemsWithProbabilities = new List<Tuple<Item, float>>(itemsProbs.Count);

            foreach (var chest in configurations[_currentConfiguration].chests)
            {
                itemsWithProbabilities.Clear();

                foreach (var item in itemsProbs)
                    itemsWithProbabilities.Add(new Tuple<Item, float>(item.items.GetItems(), item.probability));

                chest.Init(RandomElementsGenerator.GetRandom(itemsWithProbabilities,
                    RandomElementsGenerator.GetRandom(itemsCountProbabilities)));
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