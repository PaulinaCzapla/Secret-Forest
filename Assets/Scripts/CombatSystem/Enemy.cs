using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Glades.GladeTypes;
using RandomGenerators;
using UnityEngine;

namespace CombatSystem
{
    public class Enemy : MonoBehaviour
    {
        public bool IsDead => _currentHealth <= 0;
        private Animator _animator;
        public float Defense => _defense;

        private float _defense;
        private float _damage;
        private float _dodge;
        private float _critical;
        private float _currentHealth;
        private Sequence _sequence;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public float GetAttackValue()
        {
            _sequence = DOTween.Sequence().Append(transform.DOMoveX(transform.position.x - 1f, 0.15f))
                .AppendCallback(() => _animator.Play("Attack"))
                .AppendInterval(0.7f)
                .Append(transform.DOMoveX(transform.position.x, 0.15f));
            bool isCritical = RandomWithProbabilityGenerator.GetRandom(_critical, 1 - _critical);

            return isCritical ? 2 * _damage : _damage;
        }
        

        public void Hit(float dmg)
        {
            bool isDodged = RandomWithProbabilityGenerator.GetRandom(_dodge, 1 - _dodge);

            if (!isDodged) 
            {
                _currentHealth -= dmg;
                StaticCombatEvents.InvokeUpdateEnemyHealthUI(_currentHealth,_defense);
                _animator.Play("Hit");
                
                if(_currentHealth <= 0)
                    Die();
            }
            else // dodged an attack
            {
                
            }
        }

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
            _currentHealth = _defense;
        }
    }
}