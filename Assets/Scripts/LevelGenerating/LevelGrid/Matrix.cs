using System.Collections.Generic;

namespace LevelGenerating.LevelGrid
{
    [System.Serializable]
    public class Matrix
    {
        public List<Array> arrays;

        public Matrix(int x, int y)
        {
            arrays = new List<Array>(new Array[x]);

            for (int i = 0; i < x; i++)
            {
                arrays[i] = new Array(y);
            }
        }

        public GridCell this[int x, int y]
        {
            get { return arrays[x][y]; }
            set { arrays[x][y] = value; }
        }
    }
    
    [System.Serializable]
    public class Array
    {
        public List<GridCell> cells;

        public Array(int x)
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