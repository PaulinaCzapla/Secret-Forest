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
        public UnityEvent TentUsed { get; set; }= new UnityEvent();
        public bool CanSleep { get; set; } = true;
        
        [SerializeField] private TextMeshPro text;
        [SerializeField] private PlayerStatsSO stats;
        [Range(0,1)]
        [SerializeField] private float hpRestored;
        [Range(0,1)]
        [SerializeField] private float foodLost;

        private Sequence _sequence;

        private void Awake()
        {
            text.DOFade(0, 0);
            text.text = "You are rested.";
        }
        
        public void Interact()
        {
            if (CanSleep)
                StartCoroutine(Sleep());
            else
                _sequence = DOTween.Sequence().Append(text.DOFade(1, 0.05f))
                    .AppendInterval(1.5f)
                    .Append(text.DOFade(0, 0.6f));
        }

        private IEnumerator Sleep()
        {
            TentUsed.Invoke();
            SleepUI.OnSleep.Invoke(0.7f,0.7f,0.4f, transform.position);

            yield return new WaitForSeconds(1.8f);
             PlayerStatsStaticEvents.InvokeHealthValueChanged(hpRestored * stats.currentMaxHealthValue);
             PlayerStatsStaticEvents.InvokeHungerValueChanged(-(foodLost * stats.currentMaxHungerValue));
        }
    }
}