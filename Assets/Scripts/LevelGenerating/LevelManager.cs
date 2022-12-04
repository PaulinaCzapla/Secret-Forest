using System;
using GameManager;
using GameManager.SavesManagement;
using PlayerInteractions;
using PlayerInteractions.StaticEvents;
using UI.Events;
using UnityEngine;

namespace LevelGenerating
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;
        [SerializeField] private PlayerStatsSO playerStats;
        [SerializeField] private LevelsConfigSO levelsConfigSo;
        [SerializeField] private PlayerAnimationController player;
        
        private void Start()
        {
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
            GameController.GetInstance().Init(playerStats, levelsConfigSo, player);
        }
        private void OnEnable()
        {
            PlayerStatsStaticEvents.SubscribeToPlayerDied(OnPlayerDied);
        }

        private void OnDisable()
        {
            PlayerStatsStaticEvents.UnsubscribeFromPlayerDied(OnPlayerDied);
        }

        public void LoadGame()
        {
            SaveManager.ReadData();
            levelGenerator.RetrieveLevelData(SaveManager.Stats);
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
        }

        public void StartNewGame()
        {
            playerStats.InitWithDefaults();
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
        }
        public void LevelFinished()
        {
            GameManager.GameController.GetInstance().CurrentLevelNum++;
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
        }

        public void OnPlayerDied()
        {
            //TODO: prepare level history
            SaveManager.DeleteCurrentGameSaveData();
        }
    }
}