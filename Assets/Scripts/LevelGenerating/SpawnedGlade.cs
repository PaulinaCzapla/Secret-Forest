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
        public string Id { get; private set; }
        public GridCell GridCell { get; set; }
        public Glade Glade{ get; set; }
        public Dictionary<AdjacentSide, AdjacentGlade> AdjacentGlades { get; set; } = new Dictionary<AdjacentSide, AdjacentGlade>();

        private void Awake()
        {
            Id = GUID.Generate().ToString();
        }
    }
}