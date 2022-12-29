using PlayerInteractions.Interfaces;
using PlayerInteractions.StaticEvents;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items.InteractableItems
{
    /// <summary>
    /// A class that represents an object that is edible. It implements IInteractable interface, so player can interact with an object of this class.
    /// </summary>
    public class EdibleObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private float value;
        [Tooltip("Will be desactivated when eaten")]
        [SerializeField] private GameObject foodObject;
        private bool _isEaten;
        
        /// <summary>
        /// Sets _isEaten flag to true and changes player's hunger value. If it's already eaten it does nothing.
        /// </summary>
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