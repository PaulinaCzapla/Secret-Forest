using Newtonsoft.Json;

namespace GameManager.SavesManagement
{
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