using System;
using System.Collections.Generic;

namespace LevelGenerating.LevelGrid
{
    [Serializable]
    public class Matrix
    {
        public List<CellsList> arrays;

        public Matrix(int x, int y)
        {
            arrays = new List<CellsList>(new CellsList[x]);

            for (int i = 0; i < x; i++)
            {
                arrays[i] = new CellsList(y);
            }
        }
        
        public GridCell this[int x, int y]
        {
            get { return arrays[x][y]; }
            set { arrays[x][y] = value; }
        }
    }
    
    [Serializable]
    public class CellsList
    {
        public List<GridCell> cells;

        public CellsList(int x)
        {
            cells = new List<GridCell>(new GridCell[x]);
        }

        public GridCell this[int index]
        {
            get { return cells[index]; }
            set { cells[index] = value; }
        }
    }
}