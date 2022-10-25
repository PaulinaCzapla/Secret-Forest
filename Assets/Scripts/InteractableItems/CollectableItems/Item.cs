using UnityEngine;
using UnityEngine.Events;

namespace InteractableItems.CollectableItems
{
    public abstract class Item : MonoBehaviour, ICollectable
    {
        public UnityEvent onCollected = new UnityEvent();

        public abstract void Collect();

        public override string ToString()
        {
            return this.gameObject.name;
        }
    }
}