﻿using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using PlayerInteractions;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameManager
{
    public class GameManager
    {
        public int CurrentLevelNum { get; set; } = 0;
        public string CurrentGladeID { get; set; }
        public PlayerEquipment Equipment { get; private set; }
        public LevelsConfigSO LevelsConfig => _levelConfig;
        private PlayerStatsSO _playerStats;
        private static GameManager _instance;
        private LevelsConfigSO _levelConfig;

        public PlayerStatsSO PlayerStats
        {
            get
            {
                if (_playerStats == null)
                    _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");

                return _playerStats;
            }
            private set { _playerStats = value; }
        }
        
        public static GameManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }

        public (float,float) GetValueMultiplierAndMin(ItemType itemType, ItemValueType type)
        {
          
            float multiplier = 1;
            float minValue = 0;


            var item = Equipment.GetCurrentEquippedItem(itemType);
                switch (type)
                {
                    case ItemValueType.Damage:
                    case ItemValueType.Defence:  
                        multiplier = item.GetTypeValue(type )+1.5f;
                        minValue = (item.GetTypeValue(type)- 10) < 0 ? 0 : item.GetTypeValue(type) - 10;
                        break;

                    case ItemValueType.DodgeChance:
                        multiplier = Mathf.Clamp(item.GetTypeValue(type )+0.5f, 0, 22.5f);
                        minValue = (item.GetTypeValue(type)- 10) < 0 ? 0 : item.GetTypeValue(type) - 10;
                        break;
                    case ItemValueType.CriticalDamageChance:
                        multiplier = Mathf.Clamp(item.GetTypeValue(type )+0.5f, 0, 50f);
                        minValue = (item.GetTypeValue(type)- 10) < 0 ? 0 : item.GetTypeValue(type) - 10;
                        break;
                }

            return (multiplier,minValue);
        }
        public void Init(PlayerStatsSO playerStats, LevelsConfigSO levelsConfigSo)
        {
            _playerStats = playerStats;
            _levelConfig = levelsConfigSo;
            Equipment = new PlayerEquipment();
        }
    }
}