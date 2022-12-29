using LevelGenerating;
using PlayerInteractions.Interfaces;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items.InteractableItems
{
    /// <summary>
    /// A class that represents end level portal. Player can interact with an object of this class.
    /// </summary>
    public class EndLevelPortal  : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Sets level as finished.
        /// </summary>
        public void Interact()
        {
            LevelManager manager = FindObjectOfType<LevelManager>();

            if (manager)
            {
                manager.LevelFinished();
            }
        }
    }
}