using System;
using System.Collections;
using GameManager;
using PlayerInteractions;
using PlayerInteractions.StaticEvents;
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
        private int _lastRecalculated = -10;

        private int _lastRecalculatedLevel = -10;
        private int _recalculationInterval = 3;
        float _dmg, _defense, _dodge, _critical;
        float _dmgHard, _defenseHard, _dodgeHard, _criticalHard;
        private bool _shouldHelpPlayer;
        private Enemy _currentEnemy;
        
        private void OnEnable()
        {
            _playerStats = Resources.Load<PlayerStatsSO>("PlayerStatsSO");
            _lastRecalculatedLevel = PlayerPrefs.GetInt("LastRecalculatedLevel", -10);
            StaticCombatEvents.SubscribeToCombatStarted(CombatStarted);
            StaticCombatEvents.SubscribeToPlayerBowAttack(PlayerBowAttack);
            StaticCombatEvents.SubscribeToPlayerSwordAttack(PlayerSwordAttack);
            PlayerStatsStaticEvents.SubscribeToPlayerDied(PlayerDied);
        }

        private void OnDisable()
        {
            StaticCombatEvents.UnsubscribeFromCombatStarted(CombatStarted);
            StaticCombatEvents.UnsubscribeFromPlayerBowAttack(PlayerBowAttack);
            StaticCombatEvents.UnsubscribeFromPlayerSwordAttack(PlayerSwordAttack);
            PlayerStatsStaticEvents.UnsubscribeFromPlayerDied(PlayerDied);
        }

        private void PlayerDied()
        {
            _lastRecalculated = -10;
        }

        private void PlayerBowAttack()
        {
            var chance = _playerStats.CurrentCriticalBow / 10;

            if (_shouldHelpPlayer)
                chance = Mathf.Max(chance, (Mathf.Clamp(chance + 0.4f, 0, 0.8f)));

            bool isCritical = RandomElementsGenerator.GetRandom(chance, 1 - chance);
            playerAnimationController.AttackBow(isCritical);
            StartCoroutine(PlayerAttack(isCritical ? 2 * _playerStats.CurrentBowDamage : _playerStats.CurrentBowDamage,
                0.25f));
        }

        private IEnumerator PlayerAttack(float dmg, float hittime)
        {
            StaticCombatEvents.InvokeToggleCombatButtonsUI(false);
            yield return new WaitForSeconds(hittime);
            _currentEnemy.Hit(dmg, _shouldHelpPlayer);
            StartCoroutine(EnemyTurn());
        }

        private void PlayerSwordAttack()
        {
            var chance = _playerStats.CurrentCriticalSword / 10;
            if (_shouldHelpPlayer)
                chance = Mathf.Max(chance, (Mathf.Clamp(chance + 0.4f, 0, 0.8f)));

            bool isCritical = RandomElementsGenerator.GetRandom(chance, 1 - chance);
            playerAnimationController.AttackSword(isCritical);
            StartCoroutine(
                PlayerAttack(isCritical ? 2 * _playerStats.CurrentSwordDamage : _playerStats.CurrentSwordDamage,
                    0.35f));
        }

        private void CombatStarted(Enemy enemy)
        {
            _currentEnemy = enemy;
            StaticCombatEvents.InvokeToggleCombatUI(true);
            StaticCombatEvents.InvokeUpdateEnemyHealthUI(enemy.Defense, enemy.Defense);

            _shouldHelpPlayer = _playerStats.currentHealthValue / _playerStats.currentMaxHealthValue < .2;

            bool playerStarts = Random.Range(0, 2) != 0;
            StaticCombatEvents.InvokeToggleCombatButtonsUI(playerStarts);

            if (!playerStarts)
                StartCoroutine(EnemyTurn());
        }

        private IEnumerator EnemyTurn()
        {
            if (!_currentEnemy.IsDead)
            {
                yield return new WaitForSeconds(1.3f);

                var dmg = _currentEnemy.GetAttackValue(_shouldHelpPlayer);
                var chance = _playerStats.CurrentDodgeChance / 10;

                if (_shouldHelpPlayer)
                    chance = Mathf.Max(chance, (Mathf.Clamp(chance + 0.4f, 0, 0.8f)));

                bool dodged = RandomElementsGenerator.GetRandom(chance,
                    1 - chance);

                yield return new WaitForSeconds(0.25f);

                if (dodged)
                    playerAnimationController.Dodged();
                else
                {
                    playerAnimationController.GetHit(dmg);
                    PlayerStatsStaticEvents.InvokeHealthValueChanged(-dmg);
                }

                yield return new WaitForSeconds(0.6f);
                StaticCombatEvents.InvokeToggleCombatButtonsUI(true);
            }
        }

        public (float, float, float, float) GetEnemyStats(DifficultyLevel difficulty)
        {
            if (GameController.GetInstance().CurrentLevelNum - _lastRecalculatedLevel >= _recalculationInterval)
            {
                _dmg = Mathf.Max(1,
                    Mathf.Max(_playerStats.CurrentBowDamage, _playerStats.CurrentSwordDamage) * 0.6f);
                _defense = (int)(_playerStats.CurrentDefense * 0.6f);
                _critical = Mathf.Max(_playerStats.CurrentCriticalSword, _playerStats.CurrentCriticalBow) * 0.5f;
                _dodge = _playerStats.CurrentDodgeChance * 0.5f;

                _dmgHard = Mathf.Max(1,
                    Mathf.Max(_playerStats.CurrentBowDamage, _playerStats.CurrentSwordDamage) * 0.9f);
                _defenseHard = ValueRounder.RoundUp(_playerStats.CurrentDefense * 0.8f);
                _criticalHard = Mathf.Max(_playerStats.CurrentCriticalSword, _playerStats.CurrentCriticalBow);
                _dodgeHard = _playerStats.CurrentDodgeChance;

                _lastRecalculatedLevel = GameController.GetInstance().CurrentLevelNum;
            }

            return (difficulty == DifficultyLevel.Easy
                ? (_defense, _dmg, _dodge, _critical)
                : (_defenseHard, _dmgHard, _dodgeHard, _criticalHard));
        }
    }
}