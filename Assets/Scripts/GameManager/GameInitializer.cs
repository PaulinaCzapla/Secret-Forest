using System;
using LevelGenerating;
using PlayerInteractions;
using UI.Events;
using UnityEngine;

namespace GameManager
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerStats;
        [SerializeField] private LevelsConfigSO levelsConfigSo;
        [SerializeField] private PlayerAnimationController player;
        private void Start()
        {
            playerStats.InitWithDefaults();
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
            GameController.GetInstance().Init(playerStats, levelsConfigSo, player);
        }
    }
}