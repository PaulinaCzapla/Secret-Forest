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
        [Header("Current hunger game values")]
        public float hungerLostPerMinute;
        public float hungerLostPerGlade;
        public float currentMaxHungerValue;
        public float currentHungerValue;
   
        [Header("Current health game values")]
        public float currentMaxHealthValue;
        public float currentHealthValue;
        public float healthRestoredPerGlade;
        public readonly float healthLostPerGladeWhenHungry;
        
        [Header("Combat stats")]
        
        [Header("Initial combat values")]
        [SerializeField] private float initialBaseDamage = 1;
        [field:Range(0, 1)] [field:SerializeField] public float initialBaseCritical { get; private set; }= 0f;
        [SerializeField] private float initialDefense = 0;
        [field:Range(0, 1)] [field:SerializeField] public float initialBaseDodgeChance { get; private set; } =0f;
        
        [Header("Current combat values")]
        [SerializeField] private float currentBaseDamage = 1;

        public float CurrentBowDamage
        {
            get { return _currentBowDamage+ currentBaseDamage; }
            set { _currentBowDamage = value ; }
        }

        public float CurrentSwordDamage
        {
            get { return _currentSwordDamage + currentBaseDamage; }
            set { _currentSwordDamage = value; }
        }

        public float CurrentCriticalBow
        {
            get { return _currentCriticalBow + initialBaseCritical; }
            set { _currentCriticalBow = value; }
        }

        public float CurrentCriticalSword
        {
            get { return _currentCriticalSword+ initialBaseCritical; }
            set { _currentCriticalSword = value ; }
        }
        
        private float _currentBowDamage;
        private float _currentSwordDamage = 0;
        private float _currentCriticalBow = 0f;
        private  float _currentCriticalSword = 0f;
        private float _currentDefense = 0;
        private float _currentDodgeChance = 0f;

        public float CurrentDefense
        {
            get { return _currentDefense+ initialDefense; }
            set { _currentDefense = value ; }
        }
        
        public float CurrentDodgeChance 
        {
            get { return _currentDodgeChance+ initialBaseDodgeChance; }
            set { _currentDodgeChance= value ; }
        }
        
        
        [Header("Inventory")]
        [Range(0, 16)] [SerializeField] private int initialEqSlotsCount=  4;
        [Range(0, 16)] public int currentEqSlotsCount=  4;
        public void InitWithDefaults()
        {
            currentHealthValue = initialDefense;
            currentMaxHealthValue = initialDefense;
            currentHungerValue = initialMaxHunger;
            currentMaxHungerValue = initialMaxHunger;
            currentEqSlotsCount = initialEqSlotsCount;
            CurrentDefense = initialDefense;
            CurrentDodgeChance = initialBaseDodgeChance;
            CurrentBowDamage = 0;
            CurrentSwordDamage = 0;
            CurrentCriticalBow = 0;
            CurrentCriticalSword = 0;
        }

        public void Init()
        {
            
        }
        
    }
}