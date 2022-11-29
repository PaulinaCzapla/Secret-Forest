using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    public abstract class ItemSO : ScriptableObject
    {
        // public string Name => name;
        // public Sprite Sprite => sprite;
        // public List<ValueType> Values => values;
        
        [SerializeField] protected ItemType type;
        [SerializeField] protected string name;
        [SerializeField] protected Sprite sprite;

        public abstract Item GetItem();
    }
}