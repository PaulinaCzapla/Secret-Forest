using GameManager;
using PlayerInteractions;
using UnityEngine;


namespace CombatSystem
{
    public class CombatManager
    {
        private static CombatManager _instance;
        private static PlayerStatsSO _playerStats;

        private int _lastRecalculatedLevel;
        private int _recalculationInterval = 5;
        float _dmg, _defense, _dodge, _critical;
        private bool _shouldHelpPlayer;
        private Enemy _currentEnemy;

        public static CombatManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CombatManager();
            }

            return _instance;
        }

        CombatManager()
        {
            _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");
            _lastRecalculatedLevel = PlayerPrefs.GetInt("LastRecalculatedLevel", 0);
            StaticCombatEvents.SubscribeToCombatStarted(CombatStarted);
        }

        private void CombatStarted(Enemy enemy)
        {
            if (_playerStats.currentHealthValue / _playerStats.currentMaxHealthValue < .2)
            {
                _critical = 0f;
                _shouldHelpPlayer = true;
            }
            else
            {
                _shouldHelpPlayer =false;
            }

            bool playerStarts = Random.Range(0, 2) != 0;
        }
        

        public (float, float, float, float) GetEnemyStats(DifficultyLevel difficulty)
        {
            if (GameController.GetInstance().CurrentLevelNum - _recalculationInterval > _lastRecalculatedLevel)
            {
                if (difficulty == DifficultyLevel.Easy)
                {
                    _dmg = _playerStats.currentDamage * 0.6f;
                    _defense = _playerStats.currentDefense * 0.6f;
                    _critical = _playerStats.currentCritical * 0.6f;
                    _dodge = _playerStats.currentDodgeChance * 0.6f;
                }
                else
                {
                    _dmg = _playerStats.currentDamage * 0.85f;
                    _defense = _playerStats.currentDefense * 0.85f;
                    _critical = _playerStats.currentCritical * 0.85f;
                    _dodge = _playerStats.currentDodgeChance * 0.85f;
                }
            }

            return (_dmg, _defense, _dodge, _critical);
        }
    }
}