using System;
using GameManager;
using UnityEngine;

namespace LevelGenerating
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelGenerator levelGenerator;

        private void Start()
        {
            levelGenerator.GenerateLevel(GameStats.GetInstance().CurrentLevelNum);
        }

        public void LevelFinished()
        {
            GameStats.GetInstance().CurrentLevelNum++;
            levelGenerator.GenerateLevel(GameStats.GetInstance().CurrentLevelNum);
        }
    }
}