using LevelGenerating.LevelGrid;
using UnityEditor;
using UnityEngine;
using Grid = LevelGenerating.LevelGrid.Grid;

namespace Editor
{
    /// <summary>
    /// An editor window that is used for baking/generating the grid according to the given parameters.
    /// </summary>
    public class GridBakerWindowEditor : EditorWindow
    {
        private static EditorWindow _editor;
        private Grid _grid;
        private int _columns;
        private int _rows;
        private bool _bakedSuccessfully;
        private bool _baked;
        private bool _wasSizeSet;

        /// <summary>
        /// Show the editor window.
        /// </summary>
        [MenuItem("SecretForest/Grid Baker", false, 1)]
        static void ShowWindow()
        {
            _editor = EditorWindow.GetWindow(typeof(GridBakerWindowEditor));
            _editor.Show();
        }

        /// <summary>
        /// Shows fields and buttons that can be used to generate grid.
        /// </summary>
        void OnGUI()
        {
            _grid = (Grid) EditorGUILayout.ObjectField(_grid, typeof(Grid), false);
            Undo.RecordObject(_grid, "Setting Value");
            if (_grid == null)
            {
                EditorGUILayout.HelpBox("Grid Settings asset is missing!", MessageType.Warning);
                return;
            }
            if (!_wasSizeSet)
            {
                _columns = _grid.columns;
                _rows = _grid.rows;
                _wasSizeSet = true;
            }

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Columns");
            _columns = EditorGUILayout.IntSlider(_columns, 2, 100);
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Rows");
            _rows = EditorGUILayout.IntSlider(_rows, 2, 100);

            if (GUILayout.Button("Bake grid"))
            {
                _grid.columns = _columns;
                _grid.rows = _rows;
                GridGenerator.Initialize(_grid);
                _bakedSuccessfully = Bake();
                _baked = true;
            }

            if (_baked)
            {
                if (!_bakedSuccessfully)
                {
                    EditorGUILayout.HelpBox("Grid wasn't generated!", MessageType.Error);
                    return;
                }

                EditorUtility.SetDirty(_grid);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                EditorGUILayout.HelpBox("Grid generated. Save project in order to save generated grid.",
                    MessageType.Info);
            }
        }

        /// <summary>
        /// Bakes the grid.
        /// </summary>
        ///  <returns>Information if baking succeeded.</returns>
        bool Bake()
        {
            return GridGenerator.GenerateGrid();
        }
    }
}