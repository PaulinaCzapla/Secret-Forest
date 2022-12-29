using System;
using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using RandomGenerators;
using UnityEngine;
using Random = UnityEngine.Random;
using ValueType = InteractableItems.CollectableItems.Items.Types.ValueType;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    /// <summary>
    /// A scriptable object that represents a group of items with their probabilities.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemGroupSO ", menuName = "ScriptableObjects/ItemGroupSO ", order = 0)]
    public class ItemGroupSO : ScriptableObject
    {
        [SerializeField] private List<ItemProbability> items;
        
        /// <summary>
        /// Draws and item from the list.
        /// </summary>
        /// <returns> Drawn item. </returns>
        public Item GetItems()
        {
            List<Tuple<Item, float>> itemsWithProbabilities = new List<Tuple<Item, float>>(items.Count);

            foreach (var item in items)
                    itemsWithProbabilities.Add(new Tuple<Item, float>(item.item.GetItem(), item.probability));

            return RandomElementsGenerator.GetRandom(itemsWithProbabilities);
        }
    }
}