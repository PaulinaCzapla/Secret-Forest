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
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
        }
        
        public void LevelFinished()
        {
            GameManager.GameController.GetInstance().CurrentLevelNum++;
            levelGenerator.GenerateLevel(GameManager.GameController.GetInstance().CurrentLevelNum);
        }
    }
}