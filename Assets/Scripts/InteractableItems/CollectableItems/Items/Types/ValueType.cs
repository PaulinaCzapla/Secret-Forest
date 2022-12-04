using System;
using InteractableItems.CollectableItems.ScriptableObjects;
using Newtonsoft.Json;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    [Serializable]
    public struct ValueType
    {
        [JsonIgnore] public ItemValueType Type => type;
        [JsonIgnore] public float Value => value;

        [JsonProperty] [SerializeField] private ItemValueType type;
        [JsonProperty] [SerializeField] private float value;

        public ValueType(ItemValueType type, float value)
        {
            this.type = type;
            this.value = value;
        }
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