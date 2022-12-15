using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    [JsonObject]
    [Serializable]
    public class GameHistory
    {
        [JsonProperty] public List<GameStats> gameHistory;

        public GameHistory()
        {
            gameHistory = new List<GameStats>();
        }
    }
}