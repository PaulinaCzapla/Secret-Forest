using System;
using System.Collections;
using GameManager;
using PlayerInteractions;
using RandomGenerators;
using UI.Events;
using Unity.VisualScripting;
using UnityEngine;
using ValueRepresentation;
using Random = UnityEngine.Random;


namespace CombatSystem
{
    public class CombatManager : GameManager.Singleton<CombatManager>
    {
        [SerializeField] private PlayerAnimationController playerAnimationController;
        private static PlayerStatsSO _playerStats;

        private int _lastRecalculatedLevel;
        private int _recalculationInterval = 5;
        float _dmg, _defense, _dodge, _critical;
        private bool _shouldHelpPlayer;
        private Enemy _currentEnemy;


        private void OnEnable()
        {
            _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");
            _lastRecalculatedLevel = PlayerPrefs.GetInt("LastRecalculatedLevel", 0);
            StaticCombatEvents.SubscribeToCombatStarted(CombatStarted);
            StaticCombatEvents.SubscribeToPlayerBowAttack(PlayerBowAttack);
            StaticCombatEvents.SubscribeToPlayerSwordAttack(PlayerSwordAttack);
        }

        private void OnDisable()
        {
            StaticCombatEvents.UnsubscribeFromCombatStarted(CombatStarted);
            StaticCombatEvents.UnsubscribeFromPlayerBowAttack(PlayerBowAttack);
            StaticCombatEvents.UnsubscribeFromPlayerSwordAttack(PlayerSwordAttack);
        }

        private void PlayerBowAttack()
        {
            bool isCritical = RandomWithProbabilityGenerator.GetRandom(_critical, 1 - _critical);
            playerAnimationController.AttackBow(isCritical);
            StartCoroutine(PlayerAttack(isCritical ? 2* _playerStats.currentDamage : _playerStats.currentDamage, 0.25f));
        }

        private IEnumerator PlayerAttack(float dmg, float hittime)
        {
            StaticCombatEvents.InvokeToggleCombatButtonsUI(false);
            yield return new WaitForSeconds(hittime);
            _currentEnemy.Hit(dmg);

            StartCoroutine(EnemyTurn());
        }

        private void PlayerSwordAttack()
        {
            bool isCritical = RandomWithProbabilityGenerator.GetRandom(_critical, 1 - _critical);
            playerAnimationController.AttackSword(isCritical);
            StartCoroutine(PlayerAttack(isCritical ? 2* _playerStats.currentDamage : _playerStats.currentDamage, 0.35f));
        }

        private void CombatStarted(Enemy enemy)
        {
            _currentEnemy = enemy;
            StaticCombatEvents.InvokeToggleCombatUI(true);
            StaticCombatEvents.InvokeUpdateEnemyHealthUI(enemy.Defense, enemy.Defense);
            if (_playerStats.currentHealthValue / _playerStats.currentMaxHealthValue < .2)
            {
                _critical = 0f;
                _shouldHelpPlayer = true;
            }
            else
            {
                _shouldHelpPlayer = false;
            }

            bool playerStarts = Random.Range(0, 2) != 0;
            StaticCombatEvents.InvokeToggleCombatButtonsUI(playerStarts);

            if (!playerStarts)
            {
                StartCoroutine(EnemyTurn());
            }
        }

        private IEnumerator EnemyTurn()
        {
            if (!_currentEnemy.IsDead)
            {
                yield return new WaitForSeconds(1.3f);

                var dmg = _currentEnemy.GetAttackValue();
                bool dodged = RandomWithProbabilityGenerator.GetRandom(_playerStats.currentDodgeChance,
                    1 - _playerStats.currentDodgeChance);

                yield return new WaitForSeconds(0.25f);

                if (dodged)
                    playerAnimationController.Dodged();
                else
                {
                    playerAnimationController.GetHit(dmg);
                    _playerStats.currentHealthValue -= dmg;
                    UIStaticEvents.InvokeUpdateHealthUI();

                    if (_playerStats.currentHealthValue <= 0)
                    {
                        playerAnimationController.Die();
                    }
                }

                yield return new WaitForSeconds(0.6f);
                StaticCombatEvents.InvokeToggleCombatButtonsUI(true);
            }
        }

        public (float, float, float, float) GetEnemyStats(DifficultyLevel difficulty)
        {
            if (GameController.GetInstance().CurrentLevelNum == 0 ||
                GameController.GetInstance().CurrentLevelNum - _recalculationInterval >= _lastRecalculatedLevel)
            {
                if (difficulty == DifficultyLevel.Easy)
                {
                    _dmg = Mathf.Max(1, _playerStats.currentDamage * 0.6f);
                    _defense = ValueRounder.RoundUp(_playerStats.currentMaxHealthValue * 0.6f);
                    _critical = _playerStats.currentCritical * 0.5f;
                    _dodge = _playerStats.currentDodgeChance * 0.5f;
                }
                else
                {
                    _dmg = Mathf.Max(1, _playerStats.currentDamage * 0.8f);
                    _defense = ValueRounder.RoundUp(_playerStats.currentMaxHealthValue * 0.9f);
                    _critical = _playerStats.currentCritical * 0.8f;
                    _dodge = _playerStats.currentDodgeChance * 0.8f;
                }
            }

            return (_defense, _dmg, _dodge, _critical);
        }
    }
}