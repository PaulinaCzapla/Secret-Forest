using UnityEngine;

namespace Glades.GladeTypes
{
    public abstract class BaseGlade : MonoBehaviour
    {
        public GladeType Type => type;
        [SerializeField] private GladeType type;

        public abstract void Initialize();
    }
}