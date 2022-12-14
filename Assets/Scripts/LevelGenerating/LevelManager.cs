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
    /// <summary>
    /// A class that manages level loading.
    /// </summary>
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

        /// <summary>
        /// Loads game based on current save data.
        /// </summary>
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
            Inventory.Instance.InitializeStorage(playerStats.CurrentEqSlotsCount, items);
            
            levelGenerator.RetrieveLevelData(SaveManager.Stats);
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
        }

        /// <summary>
        /// Starts new game and resets data.
        /// </summary>
        public void StartNewGame()
        {
            GameController.GetInstance().CurrentLevelNum = 0;
            playerStats.InitWithDefaults();
            GameController.GetInstance().Init(playerStats, levelsConfigSo, player, new PlayerEquipment(null));
            Inventory.Instance.InitializeStorage(4, null);
            
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
        }

        /// <summary>
        /// Generates new level.
        /// </summary>
        public void LevelFinished()
        {
            GameManager.GameController.GetInstance().CurrentLevelNum++;
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
            SaveCurrentGame();
        }
        
        /// <summary>
        /// Saves game progress every time player moves.
        /// </summary>
        private void OnPlayerMoved(SpawnedGlade arg0)
        {
            SaveCurrentGame();
        }
        /// <summary>
        /// Saves game history when player dies. Removes previous current game save.
        /// </summary>
        public void OnPlayerDied()
        {
            SaveManager.SaveHistory();
            SaveManager.DeleteCurrentGameSaveData();
        }

        /// <summary>
        /// Saves game progress.
        /// </summary>
        public void SaveCurrentGame()
        {
            SaveManager.PrepareSaveData();
            SaveManager.SaveCurrentGame();
        }
    }
}