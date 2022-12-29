using System;
using System.Collections.Generic;
using CombatSystem;
using PlayerInteractions;
using PlayerInteractions.StaticEvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Glades.GladeTypes
{
    /// <summary>
    /// Representation of a fight glade (easy or hard).
    /// </summary>
    public class GladeFight : BaseGlade
    {
        [SerializeField] private DifficultyLevel difficulty;
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private PlayerStatsSO stats;

        private Enemy _enemy;
        private bool _initialized;
        private int _gladeCounter;

        private void OnEnable()
        {
            OnPlayerArrived.AddListener(PlayerArrived);
            PlayerMovementStaticEvents.SubscribeToPlayerMovedToGlade(OnPlayerMoved);
            _gladeCounter = 0;
        }


        private void OnDisable()
        {
            OnPlayerArrived.RemoveListener(PlayerArrived);
            _initialized = false;
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
            
            PlayerMovementStaticEvents.UnsubscribeFromPlayerMovedToGlade(OnPlayerMoved);
        }

        /// <summary>
        /// Increments counter when player moves between glades. When counter has proper value, revives and enemy.
        /// </summary>
        private void OnPlayerMoved(SpawnedGlade glade)
        {
            if (_enemy.IsDead)
                _gladeCounter++;

            if (_gladeCounter >= 10)
            {
                _enemy.Revive();
                _gladeCounter = 0;
            }
        }
        
        /// <summary>
        /// Resets the counter and initiates the fight.
        /// </summary>
        private void PlayerArrived()
        {
            _gladeCounter = 0;
            if (_enemy.IsDead)
                return;
            GameManager.GameController.GetInstance().IsGameplayInputLocked = true;
            StaticCombatEvents.InvokeCombatStarted(_enemy);
        }

        /// <summary>
        /// Draws enemy from the list and initializes it.
        /// </summary>
        public override void Initialize()
        {
            if (_initialized)
                return;

            _initialized = true;

            base.Initialize();
            _enemy = enemies[Random.Range(0, enemies.Count)];
            //  _enemy.transform.position = enemySpawnPoint.position;
            _enemy.gameObject.SetActive(true);
            var stats = CombatManager.Instance.GetEnemyStats(difficulty);

            _enemy.Initialize(stats.Item1, stats.Item2, stats.Item3, stats.Item4);
        }
        
        protected override void ResetGlade()
        {
            _initialized = false;
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }
    }
}