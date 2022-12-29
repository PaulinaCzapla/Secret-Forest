using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace LevelGenerating.LevelGrid
{
    /// <summary>
    /// A scriptable object that contains grid data. Parameters like cell width, height and space between can be set in the editor.
    /// </summary>
    [CreateAssetMenu(fileName = "Grid", menuName = "ScriptableObjects/Grid", order = 0)]
    public class Grid : ScriptableObject
    {
        [HideInInspector] public Matrix levelsGrid;
        [HideInInspector]public int rows = 15;
        [HideInInspector]public int columns = 15;

        [Header("Grid attributes")] public float cellHeight = 3;
        public float cellWidth = 5;
        public float spaceBetween = 1.5f;
        public bool drawGridGizmo = true;
    }
}