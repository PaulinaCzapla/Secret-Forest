using UnityEngine;

namespace LevelGenerating.LevelGrid
{
    public static class GridGenerator
    {
        private static LevelGrid.Grid _grid;
        private static bool _isInitialized;

        public static void Initialize(ref LevelGrid.Grid grid)
        {
            if (!_isInitialized)
            {
                _grid = grid;
                _isInitialized = true;
            }
        }
        public static bool GenerateGrid()
        {
            if (_isInitialized)
            {
                Debug.Log("baking");
                _grid.LevelsGrid = null;
                _grid.LevelsGrid = new Matrix(_grid.columns, _grid.rows);
               // _grid.LevelsGrid = new GridCell[_grid.columns, _grid.rows];
                int spaceRowsMultiplier = 0;
                int heightMultiplier = 0;
                int spaceMultiplier = 0;
                int widthMultiplier = 0;
                float x = 0;
                float y = 0;
                bool isCell = false;
                bool isColumnCell;
                int indexColumns = -1, indexRows = -1;

                for (int j = 0; j < 2 * _grid.columns; j++)
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
                        x = _grid.cellWidth * widthMultiplier + _grid.spaceBetween * spaceMultiplier +
                            _grid.cellWidth / 2;
                    }
                    else
                        isColumnCell = false;

                    heightMultiplier = 0;
                    spaceRowsMultiplier = 0;

                    for (int i = 0; i < 2 * _grid.rows; i++)
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
                            y = _grid.cellHeight * heightMultiplier +
                                _grid.spaceBetween * spaceRowsMultiplier + _grid.cellHeight / 2;
                        }
                        else
                            isCell = false;

                        if (isCell)
                        {
                            Debug.Log("cell");
                            _grid.LevelsGrid[indexColumns, indexRows] =
                                new GridCell(new GridPosition(indexColumns, indexRows), new Vector2(x, y));
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

      
    }
}