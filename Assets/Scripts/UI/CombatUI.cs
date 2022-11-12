using System;
using CombatSystem;
using UI.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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
        }

        private void OnDisable()
        {
            StaticCombatEvents.UnsubscribeFromToggleCombatUI(ToggleUI);
            StaticCombatEvents.UnsubscribeFromToggleCombatButtonsUI(ToggleButtons);
            swordButton.onClick.RemoveListener(SwordAttack);
            bowButton.onClick.RemoveListener(BowAttack);
        }

        private void ToggleButtons(bool shouldBeActive)
        {
            bowButton.interactable = shouldBeActive;
            swordButton.interactable = shouldBeActive;
        }

        private void SwordAttack()
        {
            StaticCombatEvents.InvokePlayerSwordAttack();
        }

        private void BowAttack()
        {
            StaticCombatEvents.InvokePlayerBowAttack();
        }

        private void ToggleUI(bool isOpen)
        {
            uiObject.SetActive(isOpen);
        }
    }
}