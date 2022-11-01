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
    
    [Serializable]
    public struct ValuesPossibilitiesType
    {
        public ItemValueType Type => type;
        public AnimationCurve Values => values;
            
        [SerializeField] private ItemValueType type;
        [SerializeField] private AnimationCurve values;
    }
}