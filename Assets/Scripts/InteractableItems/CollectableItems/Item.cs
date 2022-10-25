using System.Collections.Generic;
using Glades.GladeTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InteractableItems.CollectableItems
{
    public abstract class Item : MonoBehaviour, ICollectable, IInitializable
    {
        public UnityEvent onCollected { get; set; } = new UnityEvent();


        public virtual void Initialize() { }

        public abstract void Collect();

        public override string ToString()
        {
            return this.gameObject.name;
        }
    }
}