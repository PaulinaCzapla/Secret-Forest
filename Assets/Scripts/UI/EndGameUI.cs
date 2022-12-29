using System;
using DG.Tweening;
using PlayerInteractions.Input;
using PlayerInteractions.StaticEvents;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// A class that implements end game UI.
    /// </summary>
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
            mainMenu.onClick.RemoveListener(OpenMainMenu);
        }
        /// <summary>
        /// Called when player is dead. Activates the end game UI.
        /// </summary>
        private void PlayerDied()
        {
            InputManager.TapEnable = false;
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1, 0.5f);
        }

        /// <summary>
        /// Called when player clicks "Main Menu" button. Displays main menu UI.
        /// </summary>
        private void OpenMainMenu()
        {
            mainMenuManager.Open();
            DOTween.Sequence()
                .AppendInterval(0.1f)
                .Append(canvasGroup.DOFade(0, 0.2f))
                .AppendCallback(() => canvasGroup.gameObject.SetActive(false));
        }
    }
}