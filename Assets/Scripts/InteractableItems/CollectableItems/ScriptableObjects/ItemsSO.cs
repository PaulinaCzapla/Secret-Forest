using System.Collections.Generic;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/Items", order = 0)]
    public class ItemsSO : ScriptableObject
    {
        public Dictionary<string, ItemSO> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new();
                    foreach (var item in items)
                    {
                        _items.Add(item.ID, item);
                    }
                }

                return _items;
            }
        }
        [SerializeField] private List<ItemSO> items;
        private Dictionary<string, ItemSO> _items;
    }
}