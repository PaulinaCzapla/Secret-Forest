using System.Collections.Generic;
using Glades;
using LevelGenerating;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class CatEyeNear : Item, IUsable
    {
        public CatEyeNear(Sprite sprite, string name) : base(sprite, name)
        {
        }
        public override bool Collect()
        {
            if (InventoryUI.Instance.ItemCollected(this))
            {
                onCollected?.Invoke();
                return true;
            }
            return false;
        }

        public override string GetString()
        {
            return "Witch cat's eye. Reveals the nearest glades.";
        }

        public void Use()
        {
            List<SpawnedGlade> glades = new List<SpawnedGlade>();
            glades.Add(LevelGenerator.EndGlade);
           
            LevelGenerator.EndGlade.SetVisibility(true);
            LevelGenerator.EndGlade.SetVisibility(true); 
            ItemsStaticEvents.InvokeUnlockGlades(glades);
        }

    }
}