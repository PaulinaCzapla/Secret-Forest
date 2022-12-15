using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Jobs;
using UnityEngine;

namespace LevelGenerating.LevelGrid
{
    [Serializable]
    public class GridCell
    {
        [field: SerializeField] public Vector2 Position { get; private set; }
        [field: SerializeField] public GridPosition PositionInGrid { get; private set; }

        public GridCell(GridPosition positionInGrid, Vector2 position)
        {
            PositionInGrid = positionInGrid;
            Position = position;
        }
    }
}