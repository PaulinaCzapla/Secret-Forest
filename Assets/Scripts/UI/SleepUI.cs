using System;
using DG.Tweening;
using GameManager;
using PlayerInteractions.Input;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    /// <summary>
    /// Implements sleep UI animation.
    /// </summary>
    public class SleepUI : MonoBehaviour
    {
        public static UnityEvent<float, float, float, Vector3> OnSleep = new UnityEvent<float, float, float, Vector3>();
        [SerializeField] private CanvasGroup _canvasGroup;

        private Sequence _sequence;
        private void Awake()
        {
            _canvasGroup.gameObject.SetActive(false);
            OnSleep.AddListener(Sleep);
        }
        
        private void Sleep(float fadeInTime, float fadedTime, float fadeOutTime, Vector3 tentPos)
        {
            _sequence = DOTween.Sequence()
                .AppendCallback(() => GameController.GetInstance().PlayerAnimation.GoToSleep(tentPos, 0.2f))
                .AppendInterval(0.18f)
                .AppendCallback(() => _canvasGroup.gameObject.SetActive(true))
                .Append(_canvasGroup.DOFade(0.95f, fadeInTime))
                .AppendInterval(fadedTime)
                .Append(_canvasGroup.DOFade(0, fadeOutTime))
                .AppendCallback(() => GameController.GetInstance().PlayerAnimation.StopSleeping())
                .AppendCallback(() => _canvasGroup.gameObject.SetActive(false));
        }
    }
}