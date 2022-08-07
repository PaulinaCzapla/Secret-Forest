using UnityEngine;

namespace Gameplay.LevelGenerating.Grid
{
    public class GridDrawer
    {
        private GridSettings _gridSettings;
        
        public GridDrawer(GridSettings settings)
        {
            _gridSettings = settings;
        }

        public void DrawGizmos()
        {
            DrawColumns();
            DrawRows();
        }

        private void DrawRows()
        {
            int spaceMultiplier = 0;
            int heightMultiplier = 0;
            for (int i = 0; i < 2 * _gridSettings.rows; i++)
            {
                if (i > 0)
                {
                    if (i % 2 == 0)
                        spaceMultiplier++;
                    else
                        heightMultiplier++;
                }

                Gizmos.DrawLine(
                    new Vector3(0,
                        _gridSettings.roomHeight * heightMultiplier + _gridSettings.spaceBetween * spaceMultiplier, 0),
                    new Vector3(_gridSettings.columns * _gridSettings.roomWidth + (_gridSettings.columns - 1) * _gridSettings.spaceBetween,
                        _gridSettings.roomHeight * heightMultiplier + _gridSettings.spaceBetween * spaceMultiplier));
            }
        }

        private void DrawColumns()
        {
            int spaceMultiplier = 0;
            int widthMultiplier = 0;
            for (int i = 0; i < 2 * _gridSettings.columns; i++)
            {
                if (i > 0)
                {
                    if (i % 2 == 0)
                        spaceMultiplier++;
                    else
                        widthMultiplier++;
                }

                Gizmos.DrawLine(
                    new Vector3(
                        _gridSettings.roomWidth * widthMultiplier + _gridSettings.spaceBetween * spaceMultiplier,
                        0, 0),
                    new Vector3(
                        _gridSettings.roomWidth * widthMultiplier + _gridSettings.spaceBetween * spaceMultiplier,
                        _gridSettings.rows * _gridSettings.roomHeight + (_gridSettings.rows - 1) * _gridSettings.spaceBetween));
            }
        }
    }
}