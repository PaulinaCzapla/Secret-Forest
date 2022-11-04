using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/Items", order = 0)]
    public class ItemsSO : ItemSO
    {
        [SerializeField] private List<ItemSO> items;
        public override Item GetItem()
        {
            return items[Random.Range(0, items.Count)].GetItem();
        }
    }
}