using System;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelGenerating.LevelGrid
{
    [Serializable]
    public struct GridPosition
    {
        [JsonProperty] [field: SerializeField] public int X { get; private set; }
        [JsonProperty] [field: SerializeField] public int Y { get; private set; }
        [JsonIgnore] public Vector2 Position => new Vector2(X, Y);

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}