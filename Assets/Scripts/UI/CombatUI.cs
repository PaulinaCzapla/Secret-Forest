using System;
using CombatSystem;
using LevelGenerating;
using UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// A class that implements combat UI.
    /// </summary>
    public class CombatUI : MonoBehaviour
    {
        [SerializeField] private GameObject uiObject;
        [SerializeField] private Button swordButton;
        [SerializeField] private Button bowButton;

        private void OnEnable()
        {
            uiObject.SetActive(false);
            StaticCombatEvents.SubscribeToToggleCombatUI(ToggleUI);
            StaticCombatEvents.SubscribeToToggleCombatButtonsUI(ToggleButtons);
            swordButton.onClick.AddListener(SwordAttack);
            bowButton.onClick.AddListener(BowAttack);
            LevelGenerator.OnLevelGenerated += () => ToggleUI(false);
        }

        private void OnDisable()
        {
            StaticCombatEvents.UnsubscribeFromToggleCombatUI(ToggleUI);
            StaticCombatEvents.UnsubscribeFromToggleCombatButtonsUI(ToggleButtons);
            swordButton.onClick.RemoveListener(SwordAttack);
            bowButton.onClick.RemoveListener(BowAttack);
            LevelGenerator.OnLevelGenerated -= () => ToggleUI(false);
        }

        /// <summary>
        /// Makes buttons interactable or not.
        /// </summary>
        /// <param name="shouldBeActive"> Indicates if buttons should be active. </param>
        private void ToggleButtons(bool shouldBeActive)
        {
            bowButton.interactable = shouldBeActive;
            swordButton.interactable = shouldBeActive;
        }

        /// <summary>
        /// Is called when player clicked sword attack button.
        /// </summary>
        private void SwordAttack()
        {
            StaticCombatEvents.InvokePlayerSwordAttack();
        }
        /// <summary>
        /// Is called when player clicked bow attack button.
        /// </summary>
        private void BowAttack()
        {
            StaticCombatEvents.InvokePlayerBowAttack();
        }
        /// <summary>
        /// Enables/disables UI.
        /// </summary>
        /// <param name="isOpen"> Indicates if UI should be active. </param>
        private void ToggleUI(bool isOpen)
        {
            uiObject.SetActive(isOpen);
        }
    }
}