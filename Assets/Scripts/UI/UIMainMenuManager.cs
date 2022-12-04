﻿using System;
using DG.Tweening;
using GameManager.SavesManagement;
using LevelGenerating;
using PlayerInteractions.Input;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMainMenuManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private Button continueGame;
        [SerializeField] private Button newGame;

        private void Awake()
        {
            
            Open();
            //TODO: fill list
        }

        private void OnEnable()
        {
            continueGame.onClick.AddListener(ContinueGame);
            newGame.onClick.AddListener(NewGame);
        }

        private void OnDisable()
        {
            continueGame.onClick.RemoveListener(ContinueGame);
            newGame.onClick.RemoveListener(NewGame);
        }

        private void NewGame()
        {
            levelManager.StartNewGame();
            Close();
        }

        private void ContinueGame()
        {
            levelManager.LoadGame();
            Close();
        }

        public void Close()
        {
            DOTween.Sequence().Append(canvasGroup.DOFade(0, 0.2f))
                .AppendCallback(() => gameObject.SetActive(false))
                .AppendCallback(() => InputManager.TapEnable = true);
        }

        public void Open()
        {
            continueGame.gameObject.SetActive(SaveManager.HasSavedCurrentGame());
            InputManager.TapEnable = false;
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, 0.2f);
        }
    }
}