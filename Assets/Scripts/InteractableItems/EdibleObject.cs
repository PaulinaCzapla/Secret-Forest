using PlayerInteractions.Interfaces;
using PlayerInteractions.StaticEvents;
using UnityEngine;

namespace InteractableItems
{
    public class EdibleObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private float value;
        [Tooltip("Will be desactivated when eaten")]
        [SerializeField] private GameObject foodObject;
        private bool _isEaten;
        
        public void Interact()
        {
            if(_isEaten)
                return; 
            
            PlayerStatsStaticEvents.InvokeTryHungerValueChanged(value);
            foodObject.SetActive(false);
        }
    }
}