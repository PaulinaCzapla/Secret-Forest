using System;
using System.Collections.Generic;
using Gizmos;
using Glades;
using LevelGenerating.LevelGrid;
using Unity.VisualScripting;
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

        [SerializeField] private List<SpawnedGlade> spawnedGlades;

        [Header("Game grid")] [SerializeField] private Grid grid;

        // [Header("Grid generator")] [SerializeField]
        //private GridGenerator gridGenerator = new GridGenerator();
        private static GridGizmoDrawer _gridGizmoDrawer;

        private void Start()
        {
            DontDestroyOnLoad(this);
            // gridGenerator.GenerateGrid();
            GenerateLevel();
        }


        private void GenerateLevel(int levelNum = 1)
        {
            var levelAttributes = levelsConfigSo.GetLevelAttributes(levelNum);

            //Generate first glade
            var firstGlade = Instantiate(gladesSo.Glades[GladeType.Start],
                grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y].Position,
                Quaternion.Euler(Vector3.zero));
            var spawnedGlade = firstGlade.GetComponent<SpawnedGlade>();
            spawnedGlade.GridCell = grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y];
            spawnedGlades.Add(spawnedGlade);
            
            int roomsToSpawn = Random.Range(levelAttributes.minRoomsNum, levelAttributes.maxRoomsNum);
            int currentGladeIndex = spawnedGlades.Count - 1;

            do
            {
                SpawnedGlade spawned = spawnedGlades[currentGladeIndex];

                //get available room positions
                List<AdjacentSide> positions = GetFreeSides(spawned);
                //get random count and random pos

                if (positions.Count > 0)
                {
                    var newRoomsNum = Random.Range(1, positions.Count);

                    newRoomsNum = Mathf.Clamp(newRoomsNum, 0, roomsToSpawn);
                    roomsToSpawn -= newRoomsNum;

                    Debug.Log(roomsToSpawn);
                    //spawn rooms
                    for (int i = 0; i < newRoomsNum; i++)
                    {
                        Vector2 newPosition = spawned.GridCell.PositionInGrid.Position;
                        AdjacentSide side = positions[Random.Range(0, positions.Count)];
                        positions.Remove(side);

                        if (side == AdjacentSide.Up)
                            newPosition += new Vector2(0, 1);
                        else if (side == AdjacentSide.Down)
                            newPosition += new Vector2(0, -1);
                        else if (side == AdjacentSide.Left)
                            newPosition += new Vector2(-1, 0);
                        else if (side == AdjacentSide.Right)
                            newPosition += new Vector2(1, 0);

                        //get new room type
                        var type = levelAttributes.availableRoomTypes[
                            Random.Range(0, levelAttributes.availableRoomTypes.Count)];

                        if (roomsToSpawn == 0 && i == newRoomsNum - 1)
                            type = GladeType.End;

                        var glade = Instantiate(gladesSo.Glades[type],
                            grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y].Position,
                            Quaternion.Euler(Vector3.zero));

                        var newGlade = glade.GetComponent<SpawnedGlade>();
                        newGlade.GridCell = grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y];

                        spawned.AdjacentGlades.Add(side, new AdjacentGlade());
                        newGlade.AdjacentGlades.Add(GetOppositeSide(side), new AdjacentGlade());

                        spawned.Initialize();
                        newGlade.Initialize();
                        
                        spawnedGlades.Add(newGlade);
                    }

                    currentGladeIndex = spawnedGlades.Count - 1;
                }
                else
                {
                    currentGladeIndex--;
                    if (currentGladeIndex < 0)
                        return;
                }
            } while (roomsToSpawn > 0);

            //spawn last
        }

        private AdjacentSide GetOppositeSide(AdjacentSide side)
        {
            if (side == AdjacentSide.Down)
                return AdjacentSide.Up;

            if (side == AdjacentSide.Up)
                return AdjacentSide.Down;

            if (side == AdjacentSide.Left)
                return AdjacentSide.Right;

            return AdjacentSide.Left;
        }

        private List<AdjacentSide> GetFreeSides(SpawnedGlade glade)
        {
            List<AdjacentSide> positions = new List<AdjacentSide>()
                {AdjacentSide.Up, AdjacentSide.Down, AdjacentSide.Left, AdjacentSide.Right};

            //check if on left

            if (glade.GridCell.PositionInGrid.X == 0)
            {
                positions.Remove(AdjacentSide.Left);
            }
            else if (glade.GridCell.PositionInGrid.X == grid.columns - 1)
            {
                positions.Remove(AdjacentSide.Right);
            }

            //check if up

            if (glade.GridCell.PositionInGrid.Y == 0)
            {
                positions.Remove(AdjacentSide.Up);
            }
            else if (glade.GridCell.PositionInGrid.Y == grid.rows - 1)
            {
                positions.Remove(AdjacentSide.Down);
            }

            // check occupied

            if (glade.AdjacentGlades.ContainsKey(AdjacentSide.Up))
                positions.Remove(AdjacentSide.Up);
            if (glade.AdjacentGlades.ContainsKey(AdjacentSide.Down))
                positions.Remove(AdjacentSide.Down);
            if (glade.AdjacentGlades.ContainsKey(AdjacentSide.Left))
                positions.Remove(AdjacentSide.Left);
            if (glade.AdjacentGlades.ContainsKey(AdjacentSide.Right))
                positions.Remove(AdjacentSide.Right);

            return positions;
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