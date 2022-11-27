using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using RandomGenerators;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/Items", order = 0)]
    public class ItemsSO : ItemSO
    {
        [SerializeField] private List<ItemProbability> items;
        public override Item GetItem()
        {
            List<Tuple<Item, float>> itemsWithProbabilities = new List<Tuple<Item, float>>(items.Count);

            foreach (var item in items)
                    itemsWithProbabilities.Add(new Tuple<Item, float>(item.item.GetItem(), item.probability));

            return RandomWithProbabilityGenerator.GetRandom(itemsWithProbabilities);
        }
    }
}