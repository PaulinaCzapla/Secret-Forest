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

        private List<SpawnedGlade> spawnedGlades;
        private Dictionary<GladeType, List<SpawnedGlade>> gladesPools;
        private SpawnedGlade startGlade;
        private SpawnedGlade endGlade;

        [Header("Game grid")] [SerializeField] private Grid grid;

        // [Header("Grid generator")] [SerializeField]
        //private GridGenerator gridGenerator = new GridGenerator();
        private static GridGizmoDrawer _gridGizmoDrawer;

        private void Start()
        {
            DontDestroyOnLoad(this);
            spawnedGlades = new List<SpawnedGlade>();
            gladesPools = new Dictionary<GladeType, List<SpawnedGlade>>();

            foreach (GladeType type in Enum.GetValues(typeof(GladeType)))
            {
                if (type != GladeType.End && type != GladeType.Start)
                    gladesPools.Add(type, new List<SpawnedGlade>());
            }

            // gridGenerator.GenerateGrid();
            GenerateLevel();
        }

        private void UnloadLevel()
        {
            if (spawnedGlades != null && spawnedGlades.Count > 0)
            {
                foreach (var glade in spawnedGlades)
                {
                    glade.gameObject.SetActive(false);
                    glade.Reset();
                    
                    var type = glade.Glade.Type;

                    if (type == GladeType.Start)
                        startGlade = glade;
                    else if (type == GladeType.End)
                        endGlade = glade;
                    else
                        gladesPools[glade.Glade.Type].Add(glade);
                }

                spawnedGlades.Clear();
            }
        }

        public void GenerateLevel(int levelNum = 1)
        {
            UnloadLevel();

            var levelAttributes = levelsConfigSo.GetLevelAttributes(levelNum);

            //Generate first glade
            SpawnedGlade spawnedGlade;
            if (startGlade == null)
            {
                var firstGlade = Instantiate(gladesSo.Glades[GladeType.Start],
                    grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y].Position,
                    Quaternion.Euler(Vector3.zero));

                spawnedGlade = firstGlade.GetComponent<SpawnedGlade>();
            }
            else
            {
                spawnedGlade = startGlade;
                spawnedGlade.gameObject.transform.position =
                    grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y].Position;
                spawnedGlade.gameObject.SetActive(true);
            }

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

                        SpawnedGlade newGlade;

                        if ((endGlade != null && type == GladeType.End) ||
                            (gladesPools.ContainsKey(type) && gladesPools[type].Count > 0))
                        {
                            if (type == GladeType.End)
                            {
                                newGlade = endGlade;
                            }
                            else
                            {
                                newGlade = gladesPools[type][0];
                                gladesPools[type].RemoveAt(0);
                            }

                            newGlade.gameObject.SetActive(true);
                            newGlade.gameObject.transform.position =
                                grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y].Position;
                        }
                        else
                        {
                            var glade = Instantiate(gladesSo.Glades[type],
                                grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y].Position,
                                Quaternion.Euler(Vector3.zero));
                            newGlade = glade.GetComponent<SpawnedGlade>();
                        }

                        newGlade.GridCell = grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y];

                        var adjacent = new AdjacentGlade(AdjacentType.Basic);
                        spawned.AdjacentGlades.Add(side, adjacent);
                        newGlade.AdjacentGlades.Add(GetOppositeSide(side), adjacent);

                        CheckOtherAdjacent(newGlade);
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
        }

        private void CheckOtherAdjacent(SpawnedGlade newGlade)
        {
            foreach (AdjacentSide adjacentSide in GetFreeSides(newGlade))
            {
                Vector2 positionToCheck = newGlade.GridCell.PositionInGrid.Position;

                if (adjacentSide == AdjacentSide.Up)
                    positionToCheck += new Vector2(0, 1);
                else if (adjacentSide == AdjacentSide.Down)
                    positionToCheck += new Vector2(0, -1);
                else if (adjacentSide == AdjacentSide.Left)
                    positionToCheck += new Vector2(-1, 0);
                else if (adjacentSide == AdjacentSide.Right)
                    positionToCheck += new Vector2(1, 0);

                SpawnedGlade adjacentGlade = GetGladeAtPosition(positionToCheck);

                if (adjacentGlade != null)
                {
                    var adjacent = new AdjacentGlade(AdjacentType.Blocked);
                    newGlade.AdjacentGlades.Add(adjacentSide, adjacent);
                    adjacentGlade.AdjacentGlades.Add(GetOppositeSide(adjacentSide), adjacent);
                    adjacentGlade.Initialize();
                }
            }
        }

        private SpawnedGlade GetGladeAtPosition(Vector2 pos)
        {
            foreach (var glade in spawnedGlades)
            {
                if (glade.GridCell.PositionInGrid.Position == pos)
                    return glade;
            }

            return null;
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

            Debug.Log(glade.GridCell.PositionInGrid.Position);
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
                positions.Remove(AdjacentSide.Down);
            }
            else if (glade.GridCell.PositionInGrid.Y == grid.rows - 1)
            {
                positions.Remove(AdjacentSide.Up);
            }

            // check occupied

            foreach (AdjacentSide side in Enum.GetValues(typeof(AdjacentSide)))
            {
                if (glade.AdjacentGlades.ContainsKey(side))
                    positions.Remove(side);
            }

            Debug.Log(positions.Count);
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