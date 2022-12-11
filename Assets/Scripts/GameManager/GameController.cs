using System.Collections.Generic;
using Glades;
using Glades.GladeTypes;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using PlayerInteractions;
using UI.Eq;
using UnityEngine;

namespace GameManager
{
    public class GameController
    {
        public bool IsGameplayInputLocked { get; set; }
        public int CurrentLevelNum { get; set; } = 0;
        public string CurrentGladeID { get; set; }
        public SpawnedGlade CurrentGlade { get; set; }
        public PlayerAnimationController PlayerAnimation { get; private set; }
        public PlayerEquipment Equipment { get; private set; }
        public LevelsConfigSO LevelsConfig => _levelConfig;
        private PlayerStatsSO _playerStats;
        private static GameController _instance;
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
        
        public static GameController GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameController();
            }

            return _instance;
        }

        public (float,float) GetValueMultiplierAndMin(ItemType itemType, ItemValueType type)
        {
            float multiplier = 1;
            float minValue = 0;
            
            var item = Equipment.GetCurrentEquippedItem(itemType);
            if (item!= null)
            {
                switch (type)
                {
                    case ItemValueType.Damage:
                    case ItemValueType.Defence:
                        multiplier = item.GetTypeValue(type) + 1.5f;
                        minValue = (item.GetTypeValue(type) - 10) < 0 ? 0 : item.GetTypeValue(type) - 10;
                        break;

                    case ItemValueType.DodgeChance:
                        multiplier = Mathf.Clamp(item.GetTypeValue(type) + 0.5f, 0, 10);
                        minValue = (item.GetTypeValue(type) - 10) < 0 ? 0 : item.GetTypeValue(type) - 10;
                        break;
                    case ItemValueType.CriticalDamageChance:
                        multiplier = Mathf.Clamp(item.GetTypeValue(type) + 0.5f, 0, 35f);
                        minValue = (item.GetTypeValue(type) - 10) < 0 ? 0 : item.GetTypeValue(type) - 10;
                        break;
                }
            }

            return (multiplier,minValue);
        }
        public void Init(PlayerStatsSO playerStats, LevelsConfigSO levelsConfigSo, PlayerAnimationController player, PlayerEquipment eq)
        {
            _playerStats = playerStats;
            _levelConfig = levelsConfigSo;
            Equipment = eq;
            PlayerAnimation = player;
        }

        public void UnlockSlot()
        {
            List<Item> items = new List<Item>(Inventory.Instance.StoredItems);

            _playerStats.CurrentEqSlotsCount = Mathf.Clamp(_playerStats.CurrentEqSlotsCount+1, 0, 16);
            Inventory.Instance.InitializeStorage(_playerStats.CurrentEqSlotsCount, items);
        }
    }
}