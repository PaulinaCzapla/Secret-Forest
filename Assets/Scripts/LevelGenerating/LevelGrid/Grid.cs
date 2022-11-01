using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace LevelGenerating.LevelGrid
{
    [CreateAssetMenu(fileName = "Grid", menuName = "ScriptableObjects/Grid", order = 0)]
    public class Grid : ScriptableObject
    {
        public Matrix levelsGrid;


        [Header("Grid size")] [Tooltip("Rows and columns can be set in GridBaker.")]
        public int rows = 15;

        [Tooltip("Rows and columns can be set in GridBaker.")]
        public int columns = 15;

        [Header("Grid attributes")] public float cellHeight = 3;
        public float cellWidth = 5;
        public float spaceBetween = 1.5f;
        public bool drawGridGizmo = true;
    }
}