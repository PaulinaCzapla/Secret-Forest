using System;
using DG.Tweening;
using PlayerInteractions.Input;
using PlayerInteractions.StaticEvents;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] private UIMainMenuManager mainMenuManager;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button mainMenu;

        private void OnEnable()
        {
            PlayerStatsStaticEvents.SubscribeToPlayerDied(PlayerDied);
            mainMenu.onClick.AddListener(OpenMainMenu);
        }
        
        private void OnDisable()
        {
            PlayerStatsStaticEvents.UnsubscribeFromPlayerDied(PlayerDied);
            mainMenu.onClick.RemoveListener(OpenMainMenu);
        }

        private void PlayerDied()
        {
            InputManager.TapEnable = false;
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1, 0.5f);
        }

        private void OpenMainMenu()
        {
            mainMenuManager.Open();
            DOTween.Sequence()
                .AppendInterval(0.1f)
                .Append(canvasGroup.DOFade(0, 0.2f))
                .AppendCallback(() => gameObject.SetActive(false));
        }
    }
}