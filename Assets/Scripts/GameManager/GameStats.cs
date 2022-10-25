namespace GameManager
{
    public class GameStats
    {
        private static GameStats _instance;
        
        public int CurrentLevelNum { get; set; } = 0;
        
        public static GameStats GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameStats();
            }
            return _instance;
        }
    }
}