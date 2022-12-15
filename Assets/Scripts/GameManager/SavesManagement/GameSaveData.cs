using System.Collections.Generic;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
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