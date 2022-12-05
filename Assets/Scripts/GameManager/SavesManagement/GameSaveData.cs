using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    public class OwnedItem
    {
        [JsonProperty] public bool equipped;
        [JsonProperty] public string id;
        [JsonProperty] public ItemType type;
        [JsonProperty] public List<ValueType> values;

        public OwnedItem(bool equipped, string id, ItemType type, List<ValueType> values)
        {
            this.equipped = equipped;
            this.id = id;
            this.type = type;
            this.values = values;
        }
    }
    public class GameSaveData
    {
       [JsonProperty] public int levelNum;
       [JsonProperty] public List<GeneratedGlade> glades;
       [JsonProperty] public List<OwnedItem> items;
       [JsonProperty] public float currentHungerValue;
       [JsonProperty] public float currentHealthValue;
       [JsonProperty] public int currentEqSlotsCount;

    }
}