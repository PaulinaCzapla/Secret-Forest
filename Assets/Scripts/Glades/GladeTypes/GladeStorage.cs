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
    public class GladeStorage : Glade
    {
        [SerializeField] private LevelsConfigSO config;
        [SerializeField] private List<Chest> chests;

        public override void Initialize()
        {
            base.Initialize();
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

            foreach (var chest in chests)
            {
                chest.Init(RandomWithProbabilityGenerator.
                    GetRandom(itemsWithProbabilities, 
                        RandomWithProbabilityGenerator.GetRandom(itemsCount)));
            }
        }
    }
}