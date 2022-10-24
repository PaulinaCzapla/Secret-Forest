using UnityEngine;

namespace Glades.GladeTypes
{
    public class Glade : MonoBehaviour, IInitializable
    {
        public GladeType Type => type;
        [SerializeField] private GladeType type;
        public virtual void Initialize(){}
    }
}