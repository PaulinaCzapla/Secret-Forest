using System;
using CombatSystem;
using PlayerInteractions.StaticEvents;

namespace Glades.GladeTypes
{
    public sealed class GladeFight :BaseGlade
    {
        private SpawnedGlade _spawnedGlade;

        private void Awake()
        {
            _spawnedGlade = gameObject.GetComponent<SpawnedGlade>();
        }

        private void OnEnable()
        {
            OnPlayerArrived.AddListener(PlayerArrived);
        }

        private void OnDisable()
        {
            OnPlayerArrived.RemoveListener(PlayerArrived);
        }

        private void PlayerArrived()
        {
            GameManager.GameManager.GetInstance().IsGameplayInputLocked = true;
            StaticCombatEvents.InvokeCombatStarted();
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        
        
    }
}