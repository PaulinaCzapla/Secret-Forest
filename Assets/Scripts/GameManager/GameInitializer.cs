﻿using System;
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
        private void Start()
        {
            playerStats.InitWithDefaults();
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
            GameManager.GetInstance().Init(playerStats, levelsConfigSo);
        }
    }
}