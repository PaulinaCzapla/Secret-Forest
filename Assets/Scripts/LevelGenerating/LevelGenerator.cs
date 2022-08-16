﻿using System;
using System.Collections.Generic;
using Gizmos;
using Glades;
using LevelGenerating.LevelGrid;
using UnityEngine;
using Grid = LevelGenerating.LevelGrid.Grid;
using Random = UnityEngine.Random;

namespace LevelGenerating
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2 firstRoom;
        [SerializeField] private LevelsConfig levelsConfig;
        [SerializeField] private Glade glade;

        [SerializeField] private List<GameObject> spawnedRooms;

        [SerializeField] private Grid grid;
       // [Header("Grid generator")] [SerializeField]
        //private GridGenerator gridGenerator = new GridGenerator();
        private static GridGizmoDrawer _gridGizmoDrawer;

        private void Start()
        {
            DontDestroyOnLoad(this);
           // gridGenerator.GenerateGrid();
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            var obj = Instantiate(glade.gameObject, grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y].Position,
                Quaternion.Euler(Vector3.zero));

            obj.AddComponent<SpawnedGlade>().GridCell = grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y];
            var attributes = levelsConfig.GetLevelAttributes(1);

            SpawnedGlade firstSpawned = obj.GetComponent<SpawnedGlade>();

            int roomsNum = Random.Range(attributes.minRoomsNum, attributes.maxRoomsNum);

            SpawnedGlade spawned = firstSpawned;

            for (int i = 0; i < roomsNum; i++)
            {
                int rooms = Random.Range(0, 100);

                if (rooms >= 0 && rooms <= 45)
                {
                    rooms = 1;
                }
                else if (rooms > 45 && rooms <= 90)
                {
                    rooms = 2;
                }
                else
                {
                    rooms = 3;
                }

                if (rooms > (roomsNum - i))
                {
                    rooms = 1;
                }
                
                
            }

            // for (int j = 0; j < gridSettings.columns; j++)
            //     {
            //         for (int i = 0; i < gridSettings.rows; i++)
            //         {
            //             Instantiate(testObj, grid[j, i].Position, Quaternion.Euler(Vector3.zero));
            //         }
            //     }
        }
        public void OnDrawGizmos()
        {
            if (grid == null)
                return;
            
            if (_gridGizmoDrawer != null)
                _gridGizmoDrawer.DrawGizmos();
            else
            {
                _gridGizmoDrawer = new GridGizmoDrawer(grid);
            }
        }
    }
}