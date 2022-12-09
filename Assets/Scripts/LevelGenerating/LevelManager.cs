using System;
using System.Collections.Generic;
using GameManager;
using GameManager.SavesManagement;
using Glades;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.ScriptableObjects;
using PlayerInteractions;
using PlayerInteractions.StaticEvents;
using UI.Eq;
using UI.Events;
using UnityEngine;

namespace LevelGenerating
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private PlayerStatsSO playerStats;
        [SerializeField] private LevelsConfigSO levelsConfigSo;
        [SerializeField] private ItemsSO itemsSo;
        [SerializeField] private PlayerAnimationController player;

        private void Start()
        {
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
        }

        private void OnEnable()
        {
            PlayerStatsStaticEvents.SubscribeToPlayerDied(OnPlayerDied);
            PlayerMovementStaticEvents.SubscribeToPlayerMovedToGlade(OnPlayerMoved);
        }



        private void OnDisable()
        {
            PlayerStatsStaticEvents.UnsubscribeFromPlayerDied(OnPlayerDied);
            PlayerMovementStaticEvents.UnsubscribeFromPlayerMovedToGlade(OnPlayerMoved);
        }

        public void LoadGame()
        {
            SaveManager.ReadData();
            List<Item> items = new();
            List<Item> equippedItems = new();
            GameController.GetInstance().CurrentLevelNum = SaveManager.Stats.levelNum;
            
            foreach (var item in SaveManager.Stats.items)
            {
                var it = itemsSo.Items[item.id];

                if (item.equipped)
                {
                    equippedItems.Add(it.GetItem(item.values));
                }
                else
                {
                    items.Add(it.GetItem(item.values));
                }
            }

            playerStats.Init(SaveManager.Stats.currentHealthValue, SaveManager.Stats.currentHungerValue, SaveManager.Stats.currentEqSlotsCount);
            GameController.GetInstance().Init(playerStats, levelsConfigSo, player, new PlayerEquipment(equippedItems));
            InventoryUI.Instance.InitializeStorage(playerStats.CurrentEqSlotsCount, items);
            
            levelGenerator.RetrieveLevelData(SaveManager.Stats);
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
        }

        public void StartNewGame()
        {
            GameController.GetInstance().CurrentLevelNum = 0;
            playerStats.InitWithDefaults();
            GameController.GetInstance().Init(playerStats, levelsConfigSo, player, new PlayerEquipment(null));
            InventoryUI.Instance.InitializeStorage(4, null);
            
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
        }

        public void LevelFinished()
        {
            GameManager.GameController.GetInstance().CurrentLevelNum++;
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
            SaveCurrentGame();
        }
        private void OnPlayerMoved(SpawnedGlade arg0)
        {
            SaveCurrentGame();
        }
        public void OnPlayerDied()
        {
            SaveManager.SaveHistory();
            SaveManager.DeleteCurrentGameSaveData();
        }

        public void SaveCurrentGame()
        {
            SaveManager.PrepareSaveData();
            SaveManager.SaveCurrentGame();
        }
    }
}