using System;
using System.Collections.Generic;
using CombatSystem;
using PlayerInteractions;
using PlayerInteractions.StaticEvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Glades.GladeTypes
{
    public class GladeFight : BaseGlade
    {
        [SerializeField] private DifficultyLevel difficulty;
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private Transform enemySpawnPoint;
        [SerializeField] private PlayerStatsSO stats;
        
        private Enemy _enemy;
        private bool _initialized;

        private void OnEnable()
        {
            OnPlayerArrived.AddListener(PlayerArrived);
        }

        private void OnDisable()
        {
            OnPlayerArrived.RemoveListener(PlayerArrived);
            _initialized = false;
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
            }
        }

        private void PlayerArrived()
        {
            if(_enemy.IsDead)
                return;
            GameManager.GameController.GetInstance().IsGameplayInputLocked = true;
            StaticCombatEvents.InvokeCombatStarted(_enemy);
        }

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