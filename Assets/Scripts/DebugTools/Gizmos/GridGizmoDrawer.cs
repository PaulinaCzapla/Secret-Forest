using UnityEngine;

namespace DebugTools.Gizmos
{
#if UNITY_EDITOR

    /// <summary>
    /// A class that exists for debugging reasons. It draws grid gizmos that are visible only in the editor.
    /// </summary>
    public class GridGizmoDrawer : IGizmoDrawer
    {
        private LevelGenerating.LevelGrid.Grid _grid;

        public GridGizmoDrawer(LevelGenerating.LevelGrid.Grid settings)
        {
            _grid = settings;
        }

        public void DrawGizmos()
        {
            if (_grid.drawGridGizmo)
            {
                DrawColumns();
                DrawRows();
            }
        }

        private void DrawRows()
        {
            int spaceMultiplier = 0;
            int heightMultiplier = 0;
            for (int i = 0; i < 2 * _grid.rows; i++)
            {
                if (i > 0)
                {
                    if (i % 2 == 0)
                        spaceMultiplier++;
                    else
                        heightMultiplier++;
                }

                UnityEngine.Gizmos.DrawLine(
                    new Vector3(0,
                        _grid.cellHeight * heightMultiplier + _grid.spaceBetween * spaceMultiplier, 0),
                    new Vector3(_grid.columns * _grid.cellWidth + (_grid.columns - 1) * _grid.spaceBetween,
                        _grid.cellHeight * heightMultiplier + _grid.spaceBetween * spaceMultiplier));
            }
        }

        private void DrawColumns()
        {
            int spaceMultiplier = 0;
            int widthMultiplier = 0;
            for (int i = 0; i < 2 * _grid.columns; i++)
            {
                if (i > 0)
                {
                    if (i % 2 == 0)
                        spaceMultiplier++;
                    else
                        widthMultiplier++;
                }

                UnityEngine.Gizmos.DrawLine(
                    new Vector3(
                        _grid.cellWidth * widthMultiplier + _grid.spaceBetween * spaceMultiplier,
                        0, 0),
                    new Vector3(
                        _grid.cellWidth * widthMultiplier + _grid.spaceBetween * spaceMultiplier,
                        _grid.rows * _grid.cellHeight + (_grid.rows - 1) * _grid.spaceBetween));
            }
        }
    }
#endif
}