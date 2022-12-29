using System;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    /// <summary>
    /// Implement an icon that can be toggled with icon change.
    /// </summary>
    public class ToggleIcon : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Image image;
        [SerializeField] private Sprite spriteEnabled, spriteDisabled;

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnToggle);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnToggle);
        }

        private void OnToggle(bool enabled)
        {
            if (enabled)
                image.sprite = spriteEnabled;
            else
            {
                image.sprite = spriteDisabled;
            }
        }
    }
}