using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    /// <summary>
    /// A class that is used as a container to store current game stats and values. This class is saved to .json file.
    /// </summary>
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