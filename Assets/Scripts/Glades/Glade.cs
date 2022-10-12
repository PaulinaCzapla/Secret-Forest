using LevelGenerating;
using PlayerInteractions.Interfaces;
using UnityEngine;

namespace Glades
{
    public class Glade : MonoBehaviour, IInteractable
    {
        public GladeType Type => type;
        [SerializeField] private GladeType type;


        public void Interact()
        {
            Debug.Log("Clicked!");
        }
    }
}