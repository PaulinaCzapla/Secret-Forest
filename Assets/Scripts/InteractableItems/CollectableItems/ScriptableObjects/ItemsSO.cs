using System.Collections.Generic;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    /// <summary>
    /// A scriptable object that contains all items in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/Items", order = 0)]
    public class ItemsSO : ScriptableObject
    {
        /// <summary>
        /// A Dictionary that contains items and treats their IDs as a keys.
        /// </summary>
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