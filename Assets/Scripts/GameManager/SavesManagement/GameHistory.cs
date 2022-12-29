using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    /// <summary>
    /// A class that is used as a container for data about previous games. This class is saved to a .json file.
    /// </summary>
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