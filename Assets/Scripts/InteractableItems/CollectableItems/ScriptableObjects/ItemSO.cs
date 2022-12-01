using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    public abstract class ItemSO : ScriptableObject, IRandomizable<Item> 
    {
        [SerializeField] protected ItemType type;
        [SerializeField] protected string name;
        [SerializeField] protected Sprite sprite;
        
        public abstract Item GetRandom();
    }
}