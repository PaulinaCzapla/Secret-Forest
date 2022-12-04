using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    public class OwnedItem
    {
        public bool equipped;
        public string id;
        public ItemType type;
        public List<ValueType> values;

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

    }
}