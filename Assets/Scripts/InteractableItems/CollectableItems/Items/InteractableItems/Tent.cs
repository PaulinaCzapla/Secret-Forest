using System;
using System.Collections;
using DG.Tweening;
using Glades;
using PlayerInteractions;
using PlayerInteractions.Interfaces;
using PlayerInteractions.StaticEvents;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems.Items.InteractableItems
{
    public class Tent : MonoBehaviour, IInteractable
    {
        private static UnityEvent _tentUsed = new UnityEvent();
        
        [SerializeField] private TextMeshPro text;
        [SerializeField] private PlayerStatsSO stats;
        [Range(0,1)]
        [SerializeField] private float hpRestored;
        [Range(0,1)]
        [SerializeField] private float foodLost;
        [SerializeField] private int minGladesCount;

        private int _gladeCounter;
        private Sequence _sequence;

        private void Awake()
        {
            _gladeCounter = minGladesCount;
            text.DOFade(0, 0);
            text.text = "You are rested.";
        }

        private void OnEnable()
        {
            PlayerMovementStaticEvents.SubscribeToPlayerMovedToGlade(MovedToGlade);
            _tentUsed.AddListener(() => _gladeCounter = 0);
        }

        private void OnDisable()
        {
            PlayerMovementStaticEvents.UnsubscribeFromPlayerMovedToGlade(MovedToGlade);
            _tentUsed.RemoveAllListeners();
        }

        private void MovedToGlade(SpawnedGlade spawnedGlade)
        {
            _gladeCounter++;
        }

        public void Interact()
        {
            if (_gladeCounter >= minGladesCount)
                StartCoroutine(Sleep());
            else
                _sequence = DOTween.Sequence().Append(text.DOFade(1, 0.05f))
                    .AppendInterval(1.5f)
                    .Append(text.DOFade(0, 0.6f));
        }

        private IEnumerator Sleep()
        {
            _tentUsed.Invoke();
            SleepUI.OnSleep.Invoke(0.7f,0.7f,0.4f, transform.position);

            yield return new WaitForSeconds(1.8f);
             PlayerStatsStaticEvents.InvokeHealthValueChanged(hpRestored * stats.currentMaxHealthValue);
             PlayerStatsStaticEvents.InvokeHungerValueChanged(-(foodLost * stats.currentMaxHungerValue));
        }
        
        
    }
}