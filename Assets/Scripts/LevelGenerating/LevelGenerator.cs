using System;
using System.Collections.Generic;
using Glades;
using LevelGenerating.Grid;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LevelGenerating
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Vector2 firstRoom;
        [SerializeField] private LevelsConfig levelsConfig;
        [SerializeField] private Glade glade;

        [SerializeField] private List<GameObject> spawnedRooms;

        [Header("Grid generator")] [SerializeField]
        private LevelsGridGenerator gridGenerator = new LevelsGridGenerator();

        private void Start()
        {
            DontDestroyOnLoad(this);
            gridGenerator.GenerateGrid();
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            var obj = Instantiate(glade.gameObject, gridGenerator.Grid[(int) firstRoom.x, (int) firstRoom.y].Position,
                Quaternion.Euler(Vector3.zero));

            obj.AddComponent<SpawnedGlade>().GridCell = gridGenerator.Grid[(int) firstRoom.x, (int) firstRoom.y];
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

        private void OnDrawGizmos()
        {
            gridGenerator.DrawGizmos();
        }
    }
}