using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public abstract class UIBar : MonoBehaviour
    {
        [SerializeField] protected Slider slider;
        [SerializeField] protected TextMeshProUGUI valueText;

        protected abstract void Refresh();
    }
}