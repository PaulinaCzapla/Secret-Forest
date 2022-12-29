using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Glades.GladeTypes
{
    /// <summary>
    /// Abstract base class that represents glade.
    /// </summary>
    public abstract class BaseGlade : MonoBehaviour
    {
        public UnityEvent OnPlayerArrived { get; set; }= new UnityEvent();
        public GladeType Type => type;
        [SerializeField] private GladeType type;

        [SerializeField] protected List<GameObject> configurations;

        private int _currentConfiguration;

        /// <summary>
        /// Initializes a glade by drawing a configuration from the list.
        /// </summary>
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

        /// <summary>
        /// Resets current configuration.
        /// </summary>
        protected virtual void ResetGlade()
        {
            foreach (var conf in configurations)
            {
                conf.SetActive(false);
            }
        }
    }
}