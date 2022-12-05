using TMPro;
using UnityEngine;

namespace UI.MainMenuHistoryList
{
    public class HistoryListElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }
    }
}