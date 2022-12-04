using System.Collections.Generic;
using LevelGenerating;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    public class GameSaveData
    {
        public int levelNum;
       [JsonProperty] public List<GeneratedGlade> glades;
    }
}