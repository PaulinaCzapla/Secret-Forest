using System;
using System.Collections.Generic;
using Glades;
using UnityEngine;

namespace LevelGenerating
{
    /// <summary>
    /// Scriptable object that contains glades.
    /// </summary>
    [CreateAssetMenu(fileName = "Glades", menuName = "ScriptableObjects/Glades")]
    public class GladesSO : ScriptableObject
    {
        /// <summary>
        /// Dictionary that contains glades. The keys are types of glades.
        /// </summary>
        public Dictionary<GladeType, GameObject> Glades
        {
            get
            {
                if (_glades == null)
                {
                    _glades = new Dictionary<GladeType, GameObject>();

                    foreach (var glade in glades)
                    {
                        if (!_glades.ContainsKey(glade.type))
                            _glades.Add(glade.type, glade.gameObject);
                    }
                }

                return _glades;
            }
        }
        
        [SerializeField] private List<GladeObject> glades;

        private Dictionary<GladeType, GameObject> _glades = null;
    }

    /// <summary>
    /// Struct that represents a glade with it's type.
    /// </summary>
    [Serializable]
    public struct GladeObject
    {
        public GladeType type;
        public GameObject gameObject;
    }
}