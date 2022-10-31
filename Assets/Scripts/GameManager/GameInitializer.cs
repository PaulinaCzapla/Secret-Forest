using System;
using PlayerInteractions;
using UI.Events;
using UnityEngine;

namespace GameManager
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerStats;

        private void Start()
        {
            playerStats.InitWithDefaults();
            UIStaticEvents.InvokeUpdateHungerUI();
            UIStaticEvents.InvokeUpdateHealthUI();
            GameStats.GetInstance().Init(playerStats);
        }
    }
}