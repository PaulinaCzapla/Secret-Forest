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

    [Serializable]
    public struct GridPosition
    {
       [JsonProperty] [field: SerializeField] public int X { get; private set; }
       [JsonProperty] [field: SerializeField] public int Y { get; private set; }
       [JsonIgnore] public Vector2 Position => new Vector2(X, Y);

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}