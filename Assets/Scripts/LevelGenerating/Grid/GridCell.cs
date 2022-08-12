using UnityEditor;
using UnityEngine;

namespace LevelGenerating.Grid
{
    public class GridCell
    {
        public Vector2 Position { get; private set; }
        public GridPosition PositionInGrid { get; private set; }

        public GridCell(GridPosition positionInGrid, Vector2 position)
        {
            PositionInGrid = positionInGrid;
            Position = position;
        }
    }

    public struct GridPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}