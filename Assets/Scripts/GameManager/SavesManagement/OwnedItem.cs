using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
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
}