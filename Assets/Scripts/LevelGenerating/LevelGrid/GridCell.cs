using System;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace LevelGenerating.LevelGrid
{
    [Serializable]
    public class GridCell
    {
        public Vector2 Position { get; private set; }
        public GridPosition PositionInGrid { get; private set; }

        public GridCell(GridPosition positionInGrid, Vector2 position)
        {
            PositionInGrid = positionInGrid;
            Position = position;
        }
    }

    [Serializable]
    public struct GridPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
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

    [System.Serializable]
        public class Matrix
        {
            public List<Array> arrays;

            public Matrix(int x, int y)
            {
                arrays = new List<Array>(new Array[x]);

                for(int i =0; i< x;i++)
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
    }