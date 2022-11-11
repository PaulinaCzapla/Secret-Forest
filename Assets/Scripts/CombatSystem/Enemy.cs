using System;
using Glades.GladeTypes;
using UnityEngine;

namespace CombatSystem
{
    public class Enemy : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Attack()
        {
            
        }

        public void Initialize(float defense, float damage, float dodgeChance, float criticalChance)
        {
            
        }
    }
}