using System;
using System.Collections.Generic;
using GameManager.SavesManagement;
using UnityEngine;

namespace UI.MainMenuHistoryList
{
    public class HistoryList : MonoBehaviour
    {
        [SerializeField] private RectTransform parent;
        [SerializeField] private HistoryListElement element;

        private List<HistoryListElement> _elements;

        private void Awake()
        {
            SaveManager.ReadHistory();
        }

        private void OnEnable()
        {
            
            if (SaveManager.History == null)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                _elements = new List<HistoryListElement>();
                foreach (var historyElement in SaveManager.History.gameHistory)
                {
                    var el = Instantiate(element, parent);
                    el.SetText(historyElement.date + "\n" + "Level: " + (historyElement.level+1));
                    _elements.Add(el);
                }   
            }
        }

        private void OnDisable()
        {
            if (_elements != null)
                                foreach (var e in _elements)
                                    Destroy(e.gameObject);
        }
    }
}