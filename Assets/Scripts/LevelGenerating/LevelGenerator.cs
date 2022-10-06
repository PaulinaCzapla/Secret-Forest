using System;
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
        [Header("First room position in grid")] [SerializeField]
        private Vector2 firstRoom;

        [Header("Scriptable objects")] [SerializeField]
        private LevelsConfigSO levelsConfigSo;

        [SerializeField] private GladesSO gladesSo;

        //[SerializeField] private Glade glade;

        [SerializeField] private List<GameObject> spawnedRooms;

        [Header("Game grid")] 
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
            //Generate first glade
            var firstGlade = Instantiate(glade.gameObject,
                grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y].Position,
                Quaternion.Euler(Vector3.zero));

            firstGlade.AddComponent<SpawnedGlade>().GridCell = grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y];
            var attributes = levelsConfigSo.GetLevelAttributes(1);

            SpawnedGlade firstSpawned = firstGlade.GetComponent<SpawnedGlade>();

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