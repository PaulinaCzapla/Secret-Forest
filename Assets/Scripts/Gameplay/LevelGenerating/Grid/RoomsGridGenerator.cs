using System;
using UnityEngine;

namespace Gameplay.LevelGenerating.Grid
{
    public class RoomsGridGenerator : MonoBehaviour
    {
        [SerializeField] private GridSettings gridSettings;
        [SerializeField] private GameObject testObj;
        private GridDrawer _gridDrawer;


        private void Start()
        {
            GenerateGrid();
        }

        public void GenerateGrid()
        {
            GridCell[,] grid = new GridCell[gridSettings.columns, gridSettings.rows];
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
                {
                    isColumnCell = false;
                }

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
                    {
                        isCell = false;
                    }

                    if (isCell)
                    {
                        Debug.Log(indexColumns + " " + indexRows);
                        grid[indexColumns, indexRows] = new GridCell(new Vector2(x, y));
                    }
                }
            }

            for (int j = 0; j < gridSettings.columns; j++)
            {
                for (int i = 0; i < gridSettings.rows; i++)
                {
                    Instantiate(testObj, grid[j, i].Position, Quaternion.Euler(Vector3.zero));
                }
            }
        }

        private void CreateRoomsGrid(GridCell[,] grid)
        {
            
        }

        private void OnDrawGizmos()
        {
            if (_gridDrawer != null)
                _gridDrawer.DrawGizmos();
            else
            {
                _gridDrawer = new GridDrawer(gridSettings);
            }
        }
    }
}