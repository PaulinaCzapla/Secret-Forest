using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.Types;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    /// <summary>
    /// A class that is used as a container to store information about single item owned by the player.
    /// This class is saved to .json file.
    /// </summary>
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