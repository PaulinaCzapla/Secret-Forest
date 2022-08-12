using System;
using UnityEngine;

namespace LevelGenerating.Grid
{
    [Serializable]
    public class LevelsGridGenerator
    {
        public GridCell[,] Grid { get; private set; } = null;
        
        [SerializeField] private GridSettings gridSettings;

        private GridGizmoDrawer _gridGizmoDrawer;
        
        public void GenerateGrid()
        {
            Grid = new GridCell[gridSettings.columns, gridSettings.rows];
            int spaceRowsMultiplier = 0;
            int heightMultiplier = 0;
            int spaceMultiplier = 0;
            int widthMultiplier = 0;
            float x = 0;
            float y = 0;
            bool isCell = false;
            bool isColumnCell;
            int indexColumns = -1, indexRows = -1;

            for (int j = 0; j < 2 * gridSettings.columns; j++)
            {
                if (j > 0)
                {
                    if (j % 2 == 0)
                        spaceMultiplier++;
                    else
                        widthMultiplier++;
                }

                if (j % 2 == 0)
                {
                    indexColumns++;
                    indexRows = -1;
                    isColumnCell = true;
                    x = gridSettings.roomWidth * widthMultiplier + gridSettings.spaceBetween * spaceMultiplier +
                        gridSettings.roomWidth / 2;
                }
                else
                    isColumnCell = false;

                heightMultiplier = 0;
                spaceRowsMultiplier = 0;

                for (int i = 0; i < 2 * gridSettings.rows; i++)
                {
                    if (i > 0)
                    {
                        if (i % 2 == 0)
                            spaceRowsMultiplier++;
                        else
                            heightMultiplier++;
                    }

                    if (i % 2 == 0 && isColumnCell)
                    {
                        indexRows++;
                        isCell = true;
                        y = gridSettings.roomHeight * heightMultiplier +
                            gridSettings.spaceBetween * spaceRowsMultiplier + gridSettings.roomHeight / 2;
                    }
                    else
                        isCell = false;

                    if (isCell)
                        Grid[indexColumns, indexRows] = new GridCell(new GridPosition(indexColumns, indexRows), new Vector2(x,y));
                }
            }
        }

        public void DrawGizmos()
        {
            if (_gridGizmoDrawer != null)
                _gridGizmoDrawer.DrawGizmos();
            else
            {
                _gridGizmoDrawer = new GridGizmoDrawer(gridSettings);
            }
        }
    }
}