using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Glades.GladeTypes
{
    public abstract class BaseGlade : MonoBehaviour
    {
        public UnityEvent OnPlayerArrived { get; set; }= new UnityEvent();
        public GladeType Type => type;
        [SerializeField] private GladeType type;

        [SerializeField] protected List<GameObject> configurations;

        private int _currentConfiguration;

        public virtual void Initialize()
        {
            ResetGlade();
            if (configurations == null || configurations.Count == 0)
            {
                _currentConfiguration = 0;
            }
            else
            {
                _currentConfiguration = Random.Range(0, configurations.Count);
                configurations[_currentConfiguration].SetActive(true);
            }
        }

        protected virtual void ResetGlade()
        {
            foreach (var conf in configurations)
            {
                conf.SetActive(false);
            }
        }
    }
}