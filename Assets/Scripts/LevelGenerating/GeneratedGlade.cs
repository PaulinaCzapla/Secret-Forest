using System;
using System.Collections.Generic;
using Glades;
using LevelGenerating.LevelGrid;
using Newtonsoft.Json;

namespace LevelGenerating
{
    [Serializable]
    [JsonObject]
    public class GeneratedGlade
    {
        [JsonProperty] public GladeType type;
        [JsonProperty] public GridPosition pos;
        [JsonProperty] public bool hasLeftAdjacent;
        [JsonProperty] public bool hasRightAdjacent;
        [JsonProperty] public bool hasUpAdjacent;
        [JsonProperty] public bool hasDownAdjacent;
        [JsonProperty] public bool isVisible;

        public GeneratedGlade(GladeType type, GridPosition pos, bool hasLeftAdjacent, bool hasRightAdjacent,
            bool hasUpAdjacent, bool hasDownAdjacent, bool isVisible)
        {
            this.type = type;
            this.pos = pos;
            this.hasLeftAdjacent = hasLeftAdjacent;
            this.hasRightAdjacent = hasRightAdjacent;
            this.hasUpAdjacent = hasUpAdjacent;
            this.hasDownAdjacent = hasDownAdjacent;
            this.isVisible = isVisible;
        }
    }
}