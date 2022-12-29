using CombatSystem;
using InteractableItems.CollectableItems.Items.InteractableItems;
using PlayerInteractions.StaticEvents;
using UnityEngine;
using UnityEngine.Events;

namespace Glades.GladeTypes
{
    /// <summary>
    /// Representation of the glade with camp.
    /// </summary>
    public class GladeCamp : BaseGlade
    {
        [SerializeField] private Tent tent;
        private static UnityEvent _tentUsed = new UnityEvent();
        private int _gladeCounter;
        private const int MinGladesCount =8;

        private void OnEnable()
        {
            PlayerMovementStaticEvents.SubscribeToPlayerMovedToGlade(OnPlayerMoved);
            tent.TentUsed.AddListener(_tentUsed.Invoke);
            _tentUsed.AddListener(() => _gladeCounter = 0);
            _gladeCounter = 0;
        }
        

        private void OnDisable()
        {
            PlayerMovementStaticEvents.UnsubscribeFromPlayerMovedToGlade(OnPlayerMoved);
            tent.TentUsed.RemoveListener(_tentUsed.Invoke);
            _tentUsed.RemoveAllListeners();
        }

        /// <summary>
        /// Increments counter when player moved to new glade.
        /// </summary>
        private void OnPlayerMoved(SpawnedGlade glade)
        {
            _gladeCounter++;
            tent.CanSleep = _gladeCounter >= MinGladesCount;
        }

        public override void Initialize()
        {
          base.Initialize();
        }
    }
}