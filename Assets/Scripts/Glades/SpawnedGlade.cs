using System.Collections.Generic;
using Glades.GladeTypes;
using LevelGenerating.LevelGrid;
using PlayerInteractions.Interfaces;
using PlayerInteractions.StaticEvents;
using UnityEditor;
using UnityEngine;

namespace Glades
{
    public class SpawnedGlade : MonoBehaviour, IInteractable
    {
        public Vector2 SpawnPosition => spawnPosition.position;
        
        [SerializeField] private Transform spawnPosition;
        [Header("Gates")]
        [SerializeField] private GameObject upGate;
        [SerializeField] private GameObject downGate;
        [SerializeField] private GameObject leftGate;
        [SerializeField] private GameObject rightGate;
        public string Id { get; private set; }
        public GridCell GridCell { get; set; }

        public BaseGlade Glade
        {
            get
            {
                if (_glade == null)
                    _glade = GetComponent<BaseGlade>();
                return _glade;
            }
        }

        private BaseGlade _glade;

        public Dictionary<AdjacentSide, AdjacentGlade> AdjacentGlades { get; set; } =
            new Dictionary<AdjacentSide, AdjacentGlade>();

        private void Awake()
        {
            Id = System.Guid.NewGuid().ToString();
            DisableGates();
        }

        public void Initialize()
        {
            Glade.Initialize();
            
            if (AdjacentGlades.ContainsKey(AdjacentSide.Up) &&
                AdjacentGlades[AdjacentSide.Up].type != AdjacentType.Blocked)
                upGate.SetActive(true);

            if (AdjacentGlades.ContainsKey(AdjacentSide.Down) &&
                AdjacentGlades[AdjacentSide.Down].type != AdjacentType.Blocked)
                downGate.SetActive(true);

            if (AdjacentGlades.ContainsKey(AdjacentSide.Left) &&
                AdjacentGlades[AdjacentSide.Left].type != AdjacentType.Blocked)
                leftGate.SetActive(true);

            if (AdjacentGlades.ContainsKey(AdjacentSide.Right) &&
                AdjacentGlades[AdjacentSide.Right].type != AdjacentType.Blocked)
                rightGate.SetActive(true);
        }
        
        public void Interact()
        {
            Debug.Log("Clicked!");
            PlayerMovementStaticEvents.InvokeTryMovePlayerToPosition(this);
        }

        public void Reset()
        {
            Debug.Log("cleared");
            AdjacentGlades.Clear();
            AdjacentGlades = new Dictionary<AdjacentSide, AdjacentGlade>();
            
            GridCell = null;
            DisableGates();
        }

        private void DisableGates()
        {
            upGate.SetActive(false);
            downGate.SetActive(false);
            leftGate.SetActive(false);
            rightGate.SetActive(false); 
        }
    }
}