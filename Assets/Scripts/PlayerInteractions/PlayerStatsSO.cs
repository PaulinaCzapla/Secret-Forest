using InteractableItems.CollectableItems.Items;
using UnityEngine;

namespace PlayerInteractions
{
    [CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "ScriptableObjects/PlayerStatsSO", order = 0)]
    public class PlayerStatsSO : ScriptableObject
    {
        [Header("Hunger")] 
        [Header("Init values")]
        [SerializeField] private float initialMaxHunger;
        [Header("Current game values")]
        public float hungerLostPerMinute;
        public float hungerLostPerGlade;
        public float currentMaxHungerValue;
        public float currentHungerValue;
        

        
        [Header("Health")] 
        public readonly float HealthIncreasedPerMinute;
        public readonly float HealthIncreasedPerGlade;
        [Header("Init values")]
        [SerializeField] private float initialMaxHealth;
        [Header("Current game values")]
        public float currentMaxHealthValue;
        public float currentHealthValue;
        public float healthRestoredPerGlade;

        [Header("Combat stats")]
        // [Header("Init values")]
        // [SerializeField] private float initialBaseDamage = 1;
        // [SerializeField] private float initialBowDamage = 1;
        // [Range(0, 1)] [SerializeField] private float initialCriticalBowChance = 0.1f;
        // [SerializeField] private float initialKnifeDamage = 1;
        // [Range(0, 1)] [SerializeField] private float initialCriticalKnifeChance = 0.1f;
        // [Range(0, 1)] [SerializeField] private float initialDodgeChance = 0.1f;
        //
        [Header("Initial combat values")]
        [SerializeField] private float initialDamage = 0;
        [Range(0, 1)] [SerializeField] private float initialCritical = 0f;
        public readonly float initialDefense = 0;
        [Range(0, 1)] [SerializeField] private float initialDodgeChance =0f;
        
        [Header("Current combat values")]
        public float currentBaseDamage = 1;
        public float currentDamage = 0;
        [Range(0, 1)] public float currentCritical = 0f;
        public float currentDefense = 0;
        [Range(0, 1)] public float currentDodgeChance = 0f;
        
        
        
        [Header("Inventory")]
        [Range(0, 16)] [SerializeField] private int initialEqSlotsCount=  4;
        [Range(0, 16)] public int currentEqSlotsCount=  4;
        public void InitWithDefaults()
        {
            currentHealthValue = initialMaxHealth;
            currentMaxHealthValue = initialMaxHealth;
            currentHungerValue = initialMaxHunger;
            currentMaxHungerValue = initialMaxHunger;
            currentEqSlotsCount = initialEqSlotsCount;
            currentDamage = initialDamage;
            currentCritical = initialCritical;
            currentDefense = initialDefense;
            currentDodgeChance = initialDodgeChance;
        }
        
    }
}