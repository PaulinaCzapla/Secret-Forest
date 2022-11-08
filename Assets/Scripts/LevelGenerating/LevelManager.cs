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
            levelGenerator.GenerateLevel(GameManager.GameManager.GetInstance().CurrentLevelNum);
        }
        
        public void LevelFinished()
        {
            GameManager.GameManager.GetInstance().CurrentLevelNum++;
            levelGenerator.GenerateLevel(GameManager.GameManager.GetInstance().CurrentLevelNum);
        }
    }
}