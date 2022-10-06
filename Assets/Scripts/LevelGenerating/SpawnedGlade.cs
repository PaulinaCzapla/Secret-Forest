using System;
using System.Collections.Generic;
using Glades;
using LevelGenerating.LevelGrid;
using UnityEditor;
using UnityEngine;

namespace LevelGenerating
{
    public class SpawnedGlade : MonoBehaviour
    {
        [SerializeField] private GameObject upGate;
        [SerializeField] private GameObject downGate;
        [SerializeField] private GameObject leftGate;
        [SerializeField] private GameObject rightGate;
        public string Id { get; private set; }
        public GridCell GridCell { get; set; }
        public Glade Glade { get; set; }

        public Dictionary<AdjacentSide, AdjacentGlade> AdjacentGlades { get; set; } =
            new Dictionary<AdjacentSide, AdjacentGlade>();

        private void Awake()
        {
            Id = GUID.Generate().ToString();
        }

        public void Initialize()
        {
            if(AdjacentGlades.ContainsKey(AdjacentSide.Up))
                upGate.SetActive(true);
            
            if(AdjacentGlades.ContainsKey(AdjacentSide.Down))
                downGate.SetActive(true);
            
            if(AdjacentGlades.ContainsKey(AdjacentSide.Left))
                leftGate.SetActive(true);
            
            if(AdjacentGlades.ContainsKey(AdjacentSide.Right))
                rightGate.SetActive(true);
        }
    }
}