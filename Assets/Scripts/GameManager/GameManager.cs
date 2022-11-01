using LevelGenerating;
using PlayerInteractions;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameManager
{
    public class GameManager
    {
        private static GameManager _instance;
        public int CurrentLevelNum { get; set; } = 0;
        public string CurrentGladeID { get; set; }
        public PlayerEquipment Equipment { get; private set; }
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

        public LevelsConfigSO LevelsConfig => _levelConfig;
        private PlayerStatsSO _playerStats;

        private LevelsConfigSO _levelConfig;
        public static GameManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }

        public void Init(PlayerStatsSO playerStats, LevelsConfigSO levelsConfigSo)
        {
            _playerStats = playerStats;
            _levelConfig = levelsConfigSo;
            Equipment = new PlayerEquipment();
        }
    }
}