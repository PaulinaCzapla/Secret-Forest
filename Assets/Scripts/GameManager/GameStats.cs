using PlayerInteractions;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameManager
{
    public class GameStats
    {
        private static GameStats _instance;
        public int CurrentLevelNum { get; set; } = 0;
        public string CurrentGladeID { get; set; }

        public PlayerStatsSO PlayerStats
        {
            get
            {
                if (_playerStats == null)
                    _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");

                return _playerStats;
            }
            private set
            {
                _playerStats = value;
            }
        }

        private PlayerStatsSO _playerStats;
        public static GameStats GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameStats();
            }
            return _instance;
        }

        public void Init(PlayerStatsSO playerStats)
        {
            _playerStats = playerStats;
        }
    }
}