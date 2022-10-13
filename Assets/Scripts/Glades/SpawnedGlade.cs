using System.Collections.Generic;
using LevelGenerating.LevelGrid;
using PlayerInteractions.Interfaces;
using PlayerInteractions.StaticEvents;
using UnityEditor;
using UnityEngine;

namespace Glades
{
    public class SpawnedGlade : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject upGate;
        [SerializeField] private GameObject downGate;
        [SerializeField] private GameObject leftGate;
        [SerializeField] private GameObject rightGate;
        public string Id { get; private set; }
        public GridCell GridCell { get; set; }

        public Glade Glade
        {
            get
            {
                if (_glade == null)
                    _glade = GetComponent<Glade>();
                return _glade;
            }
        }

        private Glade _glade;

        public Dictionary<AdjacentSide, AdjacentGlade> AdjacentGlades { get; set; } =
            new Dictionary<AdjacentSide, AdjacentGlade>();

        private void Awake()
        {
            Id = GUID.Generate().ToString();
        }

        public void Initialize()
        {
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
            AdjacentGlades.Clear();
            GridCell = null;
            upGate.SetActive(false);
            downGate.SetActive(false);
            leftGate.SetActive(false);
            rightGate.SetActive(false);
        }
    }
}