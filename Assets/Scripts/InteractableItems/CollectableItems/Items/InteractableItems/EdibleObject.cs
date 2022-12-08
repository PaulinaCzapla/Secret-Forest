using PlayerInteractions.Interfaces;
using PlayerInteractions.StaticEvents;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items.InteractableItems
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
            
            _isEaten = true;
            PlayerStatsStaticEvents.InvokeHungerValueChanged(value);
            foodObject.SetActive(false);
        }

        private void OnEnable()
        {
            _isEaten = false;
            foodObject.SetActive(true);
        }
    }
}