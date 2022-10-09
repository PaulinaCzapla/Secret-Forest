﻿using System.Collections.Generic;
using Glades;

namespace LevelGenerating
{
    public class GeneratedLevel
    {
        public Dictionary<string, SpawnedGlade> GeneratedGlades { get; } = null;

        public GeneratedLevel()
        {
            GeneratedGlades = new Dictionary<string, SpawnedGlade>();
        }
    }
}