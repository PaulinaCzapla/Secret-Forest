using System.Collections.Generic;
using Glades.GladeTypes;
using LevelGenerating.LevelGrid;
using PlayerInteractions.Interfaces;
using PlayerInteractions.StaticEvents;
using UnityEditor;
using UnityEngine;

namespace Glades
{
    /// <summary>
    /// Representation of the spawned glade. Contains it's word and grid positions,adjacent glades, id and information about visibility.
    /// </summary>
    public class SpawnedGlade : MonoBehaviour, IInteractable
    {
        public Vector2 SpawnPosition => spawnPosition.position;
        public string Id { get; private set; }
        public bool IsVisible { get; private set; }
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

        public Dictionary<AdjacentSide, AdjacentGlade> AdjacentGlades { get; set; } =
            new Dictionary<AdjacentSide, AdjacentGlade>();

        [SerializeField] private Transform spawnPosition;
        [Header("Gates")] [SerializeField] private GameObject upGate;
        [SerializeField] private GameObject downGate;
        [SerializeField] private GameObject leftGate;
        [SerializeField] private GameObject rightGate;

        [SerializeField] private Mask mask;

        private BaseGlade _glade;

        private void Awake()
        {
            mask.SetMask();
            Id = System.Guid.NewGuid().ToString();
            DisableGates();
        }

        private void OnEnable()
        {
            Glade.OnPlayerArrived.AddListener(PlayerArrived);
        }

        private void OnDisable()
        {
            Glade.OnPlayerArrived.RemoveListener(PlayerArrived);
        }

        /// <summary>
        /// Sets visibility when player arrived to glade.
        /// </summary>
        private void PlayerArrived()
        {
            SetVisibility(true);
        }

        /// <summary>
        /// Initializes glade by setting a mask and activation path objects to adjacent glades.
        /// </summary>
        /// <param name="isVisible"> Information if mask should be activated. </param>
        public void Initialize(bool isVisible)
        {
            Glade.Initialize();

            if (Glade.Type != GladeType.Start)
                SetVisibility(isVisible);
            else
                SetVisibility(true);

            if (AdjacentGlades.ContainsKey(AdjacentSide.Up) &&
                AdjacentGlades[AdjacentSide.Up].Type != AdjacentType.Blocked)
                upGate.SetActive(true);

            if (AdjacentGlades.ContainsKey(AdjacentSide.Down) &&
                AdjacentGlades[AdjacentSide.Down].Type != AdjacentType.Blocked)
                downGate.SetActive(true);

            if (AdjacentGlades.ContainsKey(AdjacentSide.Left) &&
                AdjacentGlades[AdjacentSide.Left].Type != AdjacentType.Blocked)
                leftGate.SetActive(true);

            if (AdjacentGlades.ContainsKey(AdjacentSide.Right) &&
                AdjacentGlades[AdjacentSide.Right].Type != AdjacentType.Blocked)
                rightGate.SetActive(true);
        }
        /// <summary>
        /// Sets mask.
        /// </summary>
        public void SetVisibility(bool isVisible)
        {
            IsVisible = isVisible;

            if (!isVisible)
                mask.SetMask();
            else
            {
                mask.DeactivateMask();
                foreach (var glade in AdjacentGlades)
                {
                    glade.Value.SpawnedGlade.mask.ShowFrame();
                }
            }
        }

        /// <summary>
        /// Action performed when player interacts with glade.
        /// </summary>
        public void Interact()
        {
            PlayerMovementStaticEvents.InvokeTryMovePlayerToPosition(this);
        }

        /// <summary>
        /// Reset glade by removing all adjacent glades, grid cell and disabling gates.
        /// </summary>
        public void Reset()
        {
            Debug.Log("cleared");
            AdjacentGlades.Clear();
            AdjacentGlades = new Dictionary<AdjacentSide, AdjacentGlade>();

            GridCell = null;
            DisableGates();
        }

        /// <summary>
        /// Disables all gates.
        /// </summary>
        private void DisableGates()
        {
            upGate.SetActive(false);
            downGate.SetActive(false);
            leftGate.SetActive(false);
            rightGate.SetActive(false);
        }
    }
}