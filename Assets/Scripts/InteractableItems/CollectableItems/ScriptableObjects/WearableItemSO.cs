using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WearableItem", menuName = "ScriptableObjects/WearableItem", order = 0)]
    public class WearableItemSO : ItemSO
    {
        [SerializeField] private List<EquippableItemSO> items;
        public override Item GetItem()
        {
            return items[Random.Range(0, items.Count)].GetItem();
        }
    }
}