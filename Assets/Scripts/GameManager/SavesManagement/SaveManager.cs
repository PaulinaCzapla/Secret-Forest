using System;
using System.Collections.Generic;
using Glades;
using Glades.GladeTypes;
using InteractableItems.CollectableItems.Items;
using InteractableItems.CollectableItems.Items.Types;
using LevelGenerating;
using Newtonsoft.Json;
using PlayerInteractions;
using UI.Eq;
using UnityEngine;
using ValueType = InteractableItems.CollectableItems.Items.Types.ValueType;

namespace GameManager.SavesManagement
{
    /// <summary>
    /// A static class that is responsible for preparing, saving and reading save data.
    /// </summary>
    public static class SaveManager
    {
        private const string CurrentGameSaveDataFilename = "currentGame.json";
        private const string GamesHistoryFilename = "gameHistory.json";

        public static GameSaveData Stats { get; private set; }
        public static GameHistory History { get; private set; }
        private static PlayerStatsSO _playerStats;

        /// <summary>
        /// Prepares current save data to be saved to file.
        /// </summary>
        public static void PrepareSaveData()
        {
            if (_playerStats == null)
                _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");


            Stats = new GameSaveData();
            Stats.levelNum = GameController.GetInstance().CurrentLevelNum;
            Stats.currentHealthValue = _playerStats.currentHealthValue;
            Stats.currentHungerValue = _playerStats.currentHungerValue;
            Stats.currentEqSlotsCount = _playerStats.CurrentEqSlotsCount;

            if (LevelGenerator.SpawnedGlades != null && LevelGenerator.SpawnedGlades.Count != 0)
            {
                Stats.glades = new List<GeneratedGlade>();
                foreach (var glade in LevelGenerator.SpawnedGlades)
                {
                    Stats.glades.Add(new GeneratedGlade(glade.Glade.Type, glade.GridCell.PositionInGrid,
                        glade.AdjacentGlades.ContainsKey(AdjacentSide.Left),
                        glade.AdjacentGlades.ContainsKey(AdjacentSide.Right),
                        glade.AdjacentGlades.ContainsKey(AdjacentSide.Up),
                        glade.AdjacentGlades.ContainsKey(AdjacentSide.Down),
                        glade.IsVisible));
                }
            }

            ItemType[] types =
            {
                ItemType.Boots, ItemType.Breastplate, ItemType.Helmet, ItemType.ShinGuards, ItemType.Bow,
                ItemType.WhiteWeapon
            };

            List<OwnedItem> items = new();

            foreach (var type in types)
            {
                var item = GameController.GetInstance().Equipment.GetCurrentEquippedItem(type);

                if (item != null)
                {
                    items.Add(new OwnedItem(true, item.ID, item.Type, item.Values));
                }
            }

            foreach (var item in Inventory.Instance.StoredItems)
            {
                List<ValueType> values = null;
                if (item is WearableItem)
                    values = ((WearableItem) item).Values;

                items.Add(new OwnedItem(false, item.ID, item.Type, values));
            }

            Stats.items = items;
        }

        /// <summary>
        /// Prepares game history data and saves it to file.
        /// </summary>
        public static void SaveHistory()
        {
            if (History == null)
                History = new GameHistory();

            History.gameHistory.Add(new GameStats(DateTime.Today.ToShortDateString(), Stats.levelNum));
            SaveSystem.SaveFile(GamesHistoryFilename, History);
        }

        /// <summary>
        /// Read history data from file.
        /// </summary>
        public static void ReadHistory()
        {
            History = SaveSystem.ReadFile<GameHistory>(GamesHistoryFilename);
        }

        /// <summary>
        /// Checks if there is any current, not finished game saved to file.
        /// </summary>
        /// <returns> True if there is saved current game, false if there is no such file. </returns>
        public static bool HasSavedCurrentGame()
        {
            return SaveSystem.HasFile(CurrentGameSaveDataFilename);
        }

        /// <summary>
        /// Saves current game data to file.
        /// </summary>
        public static void SaveCurrentGame()
        {
            if (_playerStats.currentHealthValue > 0)
                SaveSystem.SaveFile(CurrentGameSaveDataFilename, Stats);
        }
        /// <summary>
        /// Reads current game data from file.
        /// </summary>
        public static void ReadData()
        {
            Stats = SaveSystem.ReadFile<GameSaveData>(CurrentGameSaveDataFilename);
        }

        /// <summary>
        /// Deletes current save data.
        /// </summary>
        public static void DeleteCurrentGameSaveData()
        {
            SaveSystem.DeleteFile(CurrentGameSaveDataFilename);
            Stats = null;
        }
    }
}