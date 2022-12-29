using System;
using System.Collections.Generic;

namespace LevelGenerating.LevelGrid
{
    /// <summary>
    /// A class that represents a matrix. 
    /// </summary>
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
        
        /// <summary>
        /// An overloaded operator that provides access to a GridCell by giving position in grid as parameters.
        /// </summary>
        public GridCell this[int x, int y]
        {
            get { return arrays[x][y]; }
            set { arrays[x][y] = value; }
        }
    }
    
    /// <summary>
    /// A class that represents a list with grid cells.
    /// </summary>
    [Serializable]
    public class CellsList
    {
        public List<GridCell> cells;

        public CellsList(int x)
        {
            cells = new List<GridCell>(new GridCell[x]);
        }
        /// <summary>
        /// An overloaded operator that provides access to a GridCell by given index value.
        /// </summary>
        public GridCell this[int index]
        {
            get { return cells[index]; }
            set { cells[index] = value; }
        }
    }
}