using System;

namespace InteractableItems.CollectableItems.Items
{
    public interface IRandomizable<out T>
    {
        public T GetItem();
    }
}