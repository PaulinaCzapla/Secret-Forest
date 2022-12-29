using TMPro;
using UnityEngine;

namespace UI.MainMenuHistoryList
{
    /// <summary>
    /// A class that represents single element of history list.
    /// </summary>
    public class HistoryListElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;

        public void SetText(string text)
        {
            textMeshPro.text = text;
        }
    }
}