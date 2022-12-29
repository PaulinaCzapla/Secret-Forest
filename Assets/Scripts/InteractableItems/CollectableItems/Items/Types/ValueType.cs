using System;
using Newtonsoft.Json;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items.Types
{
    /// <summary>
    /// A struct that contains value and information of it's type.
    /// </summary>
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

    /// <summary>
    /// A struct that contains animation curve and it's item value type.
    /// </summary>
    [Serializable]
    public struct ValuesPossibilitiesType
    {
        public ItemValueType Type => type;
        public AnimationCurve Values => values;

        [SerializeField] private ItemValueType type;
        [SerializeField] private AnimationCurve values;
    }
}