﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Glades.GladeTypes;
using RandomGenerators;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CombatSystem
{
    /// <summary>
    /// A class that represents an enemy.
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public bool IsDead => _currentHealth <= 0;
        public float Defense => _defense;

        [SerializeField] private TextMeshPro text;
        
        private Animator _animator;
        private float _defense;
        private float _damage;
        private float _dodge;
        private float _critical;
        private float _currentHealth;
        private Sequence _sequence;
        
        private void Awake()
        {
            text.gameObject.SetActive(false);
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Brings the enemy back to life with reduced defense value.
        /// </summary>
        public void Revive()
        {
            _currentHealth = 0.7f * _defense;
            _animator.Play("Idle");
        }

        /// <summary>
        /// Returns attack damage value that depends on circumstances and randomness. 
        /// </summary>
        /// <param name="shouldHelp"> Information if player should be helped. </param>
        /// <returns>Damage value.</returns>
        public float GetAttackValue(bool shouldHelp)
        {
            var chance = _critical/10;

            if (shouldHelp)
                chance = chance * 0.2f;
            
            bool isCritical = RandomElementsGenerator.GetRandom(chance, 1 - chance);
            if (isCritical)
            {
                _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x - 1f, 0.15f))
                    .AppendCallback(() => _animator.Play("Attack"))
                    .AppendCallback(() => text.text = "<color=#FB1412>critical!</color>")
                    .AppendCallback(() => text.gameObject.SetActive(true))
                    .AppendInterval(0.7f)
                    .AppendCallback(() => text.gameObject.SetActive(false))
                    .Append(transform.DOMoveX(transform.position.x, 0.15f));
            }
            else
            {
                _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x - 1f, 0.15f))
                    .AppendCallback(() => _animator.Play("Attack"))
                    .AppendInterval(0.7f)
                    .Append(transform.DOMoveX(transform.position.x, 0.15f));
            }
            
            return isCritical ? 2 * _damage : _damage;
        }
        
        /// <summary>
        /// Invoked when enemy is hit by the player. Value of taken damage depends on circumstances and randomness.
        /// </summary>
        /// <param name="dmg"> Damage value that should be dealt. </param>
        /// <param name="shouldHelp"> Information if player should be helped. </param>
        public void Hit(float dmg, bool shouldHelp)
        {
            var chance = _dodge/10;

            if (shouldHelp)
                chance = chance * 0.2f;
            
            bool isDodged = RandomElementsGenerator.GetRandom(chance, 1 - chance);

            if (!isDodged) 
            {
                _currentHealth -= dmg;
                StaticCombatEvents.InvokeUpdateEnemyHealthUI(_currentHealth,_defense);
                _animator.Play("Hit");
                _sequence = DOTween.Sequence().AppendCallback(() => text.text = "<color=#FB1412>-" + dmg + "</color>")
                    .AppendCallback(() => text.gameObject.SetActive(true))
                    .AppendInterval(0.4f)
                    .AppendCallback(() => text.gameObject.SetActive(false));
                
                if(_currentHealth <= 0)
                    Die();
            }
            else // dodged an attack
            {
                _sequence = DOTween.Sequence().AppendCallback(() => text.text = "dodged!")
                    .AppendCallback(() => text.gameObject.SetActive(true))
                    .Append(transform.DOMoveX(transform.position.x + 0.2f, 0.15f))
                    .AppendInterval(0.1f)
                    .Append(transform.DOMoveX(transform.position.x, 0.15f))
                    .AppendCallback(() => text.gameObject.SetActive(false));
            }
        }

        /// <summary>
        /// Performs enemy's death.
        /// </summary>
        private void Die()
        {
            _animator.Play("Death");
            StaticCombatEvents.InvokeCombatEnded();
            StaticCombatEvents.InvokeToggleCombatUI(false);
        }
        
        
        public void Initialize(float defense, float damage, float dodgeChance, float criticalChance)
        {
            _defense = defense;
            _damage = damage;
            _dodge = dodgeChance;
            _critical = criticalChance;
            _currentHealth = defense;
        }
    }
}