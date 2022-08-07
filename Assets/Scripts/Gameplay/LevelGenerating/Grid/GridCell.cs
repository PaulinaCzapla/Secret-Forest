using UnityEngine;

namespace Gameplay.LevelGenerating.Grid
{
    public class GridCell
    {
        public string Id { get; set; }
        public Vector2 Position { get; set; }

        public GridCell(Vector2 pos)
        {
            Position = pos;
        }
    }
}