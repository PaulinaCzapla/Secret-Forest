using System;
using System.Collections.Generic;
using Glades;
using UnityEngine;

namespace LevelGenerating
{
    public class GladesSO : ScriptableObject
    {
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

    public struct GladeObject
    {
        public GladeType type;
        public GameObject gameObject;
    }
}