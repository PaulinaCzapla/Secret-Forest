using LevelGenerating;
using PlayerInteractions.Interfaces;
using UnityEngine;

namespace InteractableItems
{
    public class EndLevelPortal  : MonoBehaviour, IInteractable
    {
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