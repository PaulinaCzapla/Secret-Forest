using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
    /// <summary>
    /// Class that contains information about single previous finished game.
    /// </summary>
    public class GameStats
    {
        [JsonProperty] public string date;
        [JsonProperty] public int level;

        public GameStats(string date, int level)
        {
            this.date = date;
            this.level = level;
        }
    }
}