using System;
using System.Collections.Generic;
using CameraManagement;
using DebugTools.Gizmos;
using Glades;
using LevelGenerating.LevelGrid;
using PlayerInteractions.StaticEvents;
using RandomGenerators;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Grid = LevelGenerating.LevelGrid.Grid;
using Random = UnityEngine.Random;

namespace LevelGenerating
{
    public class LevelGenerator : MonoBehaviour
    {
        public static UnityAction OnLevelGenerated { get; set; }
        
        [Header("First room position in grid")] [SerializeField]
        private Vector2 firstRoom;

        [Header("Scriptable objects")] [SerializeField]
        private LevelsConfigSO levelsConfigSo;

        [SerializeField] private GladesSO gladesSo;


        [Header("Game grid")] [SerializeField] private Grid grid;
        
        private List<SpawnedGlade> _spawnedGlades;
        private Dictionary<GladeType, List<SpawnedGlade>> _gladesPools;
        [SerializeField] private SpawnedGlade startGlade;
        private SpawnedGlade _endGlade;
        private LevelAttributes _levelAttributes;

        private static GridGizmoDrawer _gridGizmoDrawer;

        private void Awake()
        {
            //   DontDestroyOnLoad(this);
            _spawnedGlades = new List<SpawnedGlade>();
            _gladesPools = new Dictionary<GladeType, List<SpawnedGlade>>();

            foreach (GladeType type in Enum.GetValues(typeof(GladeType)))
            {
                if (type != GladeType.End && type != GladeType.Start)
                    _gladesPools.Add(type, new List<SpawnedGlade>());
            }

            // gridGenerator.GenerateGrid();
            //GenerateLevel();
        }

        private void UnloadLevel()
        {
            if(startGlade)
            startGlade.Reset();
            if(_endGlade)
            _endGlade.Reset();
            
            if (_spawnedGlades != null && _spawnedGlades.Count > 0)
            {
                foreach (var glade in _spawnedGlades)
                {
                    glade.Reset();
                    glade.gameObject.SetActive(false);

                    var type = glade.Glade.Type;

                    if (type == GladeType.Start)
                        startGlade = glade;
                    else if (type == GladeType.End)
                        _endGlade = glade;
                    else
                        _gladesPools[glade.Glade.Type].Add(glade);
                }

                _spawnedGlades.Clear();
                _spawnedGlades = new List<SpawnedGlade>();
            }
        }


        /// <summary>
        /// Generates level, starting with first (entry) glade.
        /// </summary>
        public void GenerateLevel(int levelNum = 1)
        {
            UnloadLevel();

            _levelAttributes = levelsConfigSo.GetLevelAttributes();

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
                spawnedGlade.Reset();
            }

            spawnedGlade.GridCell = grid.LevelsGrid[(int) firstRoom.x, (int) firstRoom.y];
            _spawnedGlades.Add(spawnedGlade);

            CameraLimits.MaxX = spawnedGlade.GridCell.Position.x;
            CameraLimits.MinX = spawnedGlade.GridCell.Position.x;
            CameraLimits.MaxY = spawnedGlade.GridCell.Position.y;
            CameraLimits.MinY = spawnedGlade.GridCell.Position.y;

            int roomsToSpawn = Random.Range(_levelAttributes.minRoomsNum, _levelAttributes.maxRoomsNum);
            int currentGladeIndex = _spawnedGlades.Count - 1;

            do
            {
                SpawnedGlade spawned = _spawnedGlades[currentGladeIndex];
                //get available room positions
                List<AdjacentSide> positions = GetFreeSides(spawned);
                //get random count and random pos
                if (positions.Count > 0)
                {
                    var newRoomsNum = Random.Range(1, positions.Count);
                    newRoomsNum = Mathf.Clamp(newRoomsNum, 0, roomsToSpawn);
                    roomsToSpawn -= newRoomsNum;

                    //spawn rooms
                    SpawnNewRooms(newRoomsNum, roomsToSpawn, positions, spawned);

                    currentGladeIndex = _spawnedGlades.Count - 1;
                }
                else
                {
                    currentGladeIndex--;
                    if (currentGladeIndex < 0)
                        return;
                }
            } while (roomsToSpawn > 0);
            
            PlayerMovementStaticEvents.InvokeTryMovePlayerToPosition(startGlade, true);
            OnLevelGenerated?.Invoke();
        }

        /// <summary>
        /// Spawns given glades amount adjacent to given glade. 
        /// </summary>
        private void SpawnNewRooms(int newRoomsNum, int roomsToSpawn, List<AdjacentSide> positions,
            SpawnedGlade spawned)
        {
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
                var type = _levelAttributes.GetRandomGladeType();

                if (roomsToSpawn == 0 && i == newRoomsNum - 1)
                    type = GladeType.End;

                SpawnedGlade newGlade;

                if ((_endGlade != null && type == GladeType.End) ||
                    (_gladesPools.ContainsKey(type) && _gladesPools[type].Count > 0))
                {
                    if (type == GladeType.End)
                    {
                        newGlade = _endGlade;
                    }
                    else
                    {
                        newGlade = _gladesPools[type][0];
                        _gladesPools[type].RemoveAt(0);
                    }

                    newGlade.gameObject.SetActive(true);
                    newGlade.gameObject.transform.position =
                        grid.LevelsGrid[(int) newPosition.x, (int) newPosition.y].Position;
                    newGlade.Reset();
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
                
                _spawnedGlades.Add(newGlade);
                SetCameraLimits(newGlade.GridCell.Position);
            }
        }

        private void SetCameraLimits(Vector2 position)
        {
            if (CameraLimits.MaxX < position.x)
                CameraLimits.MaxX = position.x;
            
            if (CameraLimits.MinX > position.x)
                CameraLimits.MinX= position.x;
            
            if (CameraLimits.MaxY < position.y)
                CameraLimits.MaxY = position.y;
            
            if (CameraLimits.MinY > position.y)
                CameraLimits.MinY = position.y;
        }

        /// <summary>
        ///  Checks if there are other existing adjacent glades
        /// </summary>
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
                    Tuple<AdjacentType, float>[] adjacentTypes = new[]
                    {
                        new Tuple<AdjacentType, float>(AdjacentType.Basic, 0.2f),
                        new Tuple<AdjacentType, float>(AdjacentType.Blocked, 0.8f)
                    };

                    var adjacent = new AdjacentGlade(RandomWithProbabilityGenerator.GetRandom(adjacentTypes));
                    newGlade.AdjacentGlades.Add(adjacentSide, adjacent);
                    adjacentGlade.AdjacentGlades.Add(GetOppositeSide(adjacentSide), adjacent);
                    adjacentGlade.Initialize();
                }
            }
        }

        /// <summary>
        /// Returns glade at given position in grid
        /// </summary>
        private SpawnedGlade GetGladeAtPosition(Vector2 pos)
        {
            foreach (var glade in _spawnedGlades)
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

        /// <summary>
        /// Returns not occupied sides of the given glade
        /// </summary>
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