using UnityEngine;

namespace PlayerInteractions
{
    [CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "ScriptableObjects/PlayerStatsSO", order = 0)]
    public class PlayerStatsSO : ScriptableObject
    {
        [Header("Hunger")] 
        [Header("Init values")]
        [SerializeField] private float initialMaxHunger = 10;
        [Header("Current game values")]
        public float hungerLostPerMinute;
        public float hungerLostPerGlade;
        public float currentMaxHungerValue;
        public float currentHungerValue;

        [Header("Health")] 
        public readonly float HealthIncreasedPerMinute;
        public readonly float HealthIncreasedPerGlade;
        [Header("Init values")]
        [SerializeField] private float initialMaxHealth = 10;
        [Header("Current game values")]
        public float currentMaxHealthValue;
        public float currentHealthValue;

        [Header("Combat stats")] 
        [Header("Init values")]
        [SerializeField] private float initialBowDamage = 1;
        [SerializeField] private float initialKnifeDamage = 1;
        [Range(0, 1)] [SerializeField] private float initialCriticalBowChance = 0.1f;
        [Range(0, 1)] [SerializeField] private float initialCriticalKnifeChance = 0.1f;
        [Range(0, 1)] [SerializeField] private float initialDodgeChance = 0.1f;
        [Header("Current game values")]
        public float currentBowDamage = 1;
        public float currentKnifeDamage = 1;
        [Range(0, 1)] public float currentCriticalBowChance = 0.1f;
        [Range(0, 1)] public float currentCriticalKnifeChance = 0.1f;
        [Range(0, 1)] public float currentDodgeChance = 0.1f;

        
        public void InitWithDefaults()
        {
            currentHealthValue = initialMaxHealth;
            currentMaxHealthValue = initialMaxHealth;
            currentHungerValue = initialMaxHunger;
            currentMaxHungerValue = initialMaxHunger;
        }
    }
}