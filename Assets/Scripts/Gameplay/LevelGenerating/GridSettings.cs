using UnityEngine;

namespace Gameplay.LevelGenerating
{
    [CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObjects/GridSettings", order = 0)]
    public class GridSettings : ScriptableObject
    {
        public float roomHeight;
        public float roomWidth;
        public float spaceBetween;
    }
}