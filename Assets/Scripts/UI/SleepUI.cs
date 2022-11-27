using System;
using DG.Tweening;
using PlayerInteractions.Input;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class SleepUI : MonoBehaviour
    {
        public static UnityEvent<float, float, float> OnSleep = new UnityEvent<float, float, float>();
        [SerializeField] private CanvasGroup _canvasGroup;

        private Sequence _sequence;
        private void Awake()
        {
            _canvasGroup.gameObject.SetActive(false);
            OnSleep.AddListener(Sleep);
        }
        
        private void Sleep(float fadeInTime, float fadedTime, float fadeOutTime)
        {
            _sequence = DOTween.Sequence()
                .AppendCallback(() => _canvasGroup.gameObject.SetActive(true))
                .Append(_canvasGroup.DOFade(0.95f, fadeInTime))
                .AppendInterval(fadedTime)
                .Append(_canvasGroup.DOFade(0, fadeOutTime))
                .AppendCallback(() => _canvasGroup.gameObject.SetActive(false));
        }
    }
}