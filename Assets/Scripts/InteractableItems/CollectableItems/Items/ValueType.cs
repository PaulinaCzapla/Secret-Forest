using System;
using InteractableItems.CollectableItems.ScriptableObjects;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    [Serializable]
    public struct ValueType
    {
        public ItemValueType Type => type;
        public float Value => value;
            
        [SerializeField] private ItemValueType type;
        [SerializeField] private float value;
    }
}