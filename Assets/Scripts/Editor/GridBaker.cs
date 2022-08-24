using LevelGenerating.LevelGrid;
using UnityEditor;
using UnityEngine;
using Grid = LevelGenerating.LevelGrid.Grid;

namespace Editor
{
    public class GridBakerWindowEditor : EditorWindow
    {
        private static EditorWindow _editor;
        private Grid _grid;
        private int _columns;
        private int _rows;
        private bool _bakedSuccessfully;
        private bool _baked;
        private bool _wasSizeSet;

        [MenuItem("SecretForest/Grid Baker", false, 1)]
        static void ShowWindow()
        {
            _editor = EditorWindow.GetWindow(typeof(GridBakerWindowEditor));
            _editor.Show();
        }

        void OnGUI()
        {
            _grid = (Grid) EditorGUILayout.ObjectField(_grid, typeof(Grid), false);

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
            _columns = EditorGUILayout.IntSlider(_columns, 2, 30);
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Rows");
            _rows = EditorGUILayout.IntSlider(_rows, 2, 30);

            if (GUILayout.Button("Bake grid"))
            {
                _grid.columns = _columns;
                _grid.rows = _rows;
                GridGenerator.Initialize(ref _grid);
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

                EditorGUILayout.HelpBox("Grid generated. Save project in order to save generated grid.",
                    MessageType.Info);
            }
        }

        bool Bake()
        {
            return GridGenerator.GenerateGrid();
        }
    }
}