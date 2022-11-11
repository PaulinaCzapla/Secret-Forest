using System;
using UI.Events;
using UnityEngine;

namespace UI
{
    public class CombatUI : MonoBehaviour
    {
        [SerializeField] private GameObject uiObject;

        private void OnEnable()
        {
            UIStaticEvents.SubscribeToToggleCombatUI(ToggleUI);
        }

        private void OnDisable()
        {
            UIStaticEvents.UnsubscribeFromToggleCombatUI(ToggleUI);
        }

        private void ToggleUI(bool arg0)
        {
            
        }
    }
}