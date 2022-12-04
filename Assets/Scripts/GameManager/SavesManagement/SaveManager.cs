using System.Collections.Generic;
using Glades;
using Glades.GladeTypes;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using UI.Eq;

namespace GameManager.SavesManagement
{
    public static class SaveManager
    {
        private const string CurrentGameSaveDataFilename = "currentGame.json";
        private const string GamesHistoryFilename = "gameHistory.json";

        public static GameSaveData Stats { get; private set; }
        public static List<GameStats> GameHistory{ get; private set; }

        public static void PrepareSaveData()
        {
            Stats = new GameSaveData();
            Stats.levelNum = GameController.GetInstance().CurrentLevelNum;
            
            if (LevelGenerator.SpawnedGlades != null && LevelGenerator.SpawnedGlades.Count != 0)
            {
                Stats.glades = new List<GeneratedGlade>();
                foreach (var glade in LevelGenerator.SpawnedGlades)
                {
                    
                    Stats.glades.Add(new GeneratedGlade(glade.Glade.Type,glade.GridCell.PositionInGrid,
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

            foreach (var item in InventoryUI.Instance.StoredItems)
            {
                List<ValueType> values = null;
                if (item is WearableItem)
                    values = ((WearableItem) item).Values;
                
                items.Add(new OwnedItem(false, item.ID, item.Type, values));
            }

            Stats.items = items;
        }
        
        public static void SaveAllData()
        {
            
        }

        public static bool HasSavedCurrentGame()
        {
           return SaveSystem.HasFile(CurrentGameSaveDataFilename);
        }
        public static void SaveCurrentGame()
        {
            SaveSystem.SaveFile(CurrentGameSaveDataFilename, Stats);
        }

        public static void ReadData()
        {
            Stats = SaveSystem.ReadFile<GameSaveData>(CurrentGameSaveDataFilename);
        }

        public static void DeleteCurrentGameSaveData()
        {
            SaveSystem.DeleteFile(CurrentGameSaveDataFilename);
            Stats = null;
        }
    }
}