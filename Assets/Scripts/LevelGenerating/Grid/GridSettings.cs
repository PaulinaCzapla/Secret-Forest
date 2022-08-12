using UnityEngine;

namespace LevelGenerating.Grid
{
    [CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObjects/GridSettings", order = 0)]
    public class GridSettings : ScriptableObject
    {
        public float roomHeight;
        public float roomWidth;
        public float spaceBetween;
        public int rows;
        public int columns;
    }
}