using System.Collections.Generic;
using System.Linq;
using Glades;
using LevelGenerating;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    public class ExitUncoverer : Item, IUsable
    {
        public ExitUncoverer(Sprite sprite, string name, string id, ItemType type) 
            : base(sprite, name, id, type)
        {
        }

        public override bool Collect()
        {
            if (Inventory.Instance.ItemCollected(this))
            {
                onCollected?.Invoke();
                return true;
            }
            return false;
        }

        public override string GetString()
        {
            return "An enchanted compass that always points to the portal.";
        }

        public void Use()
        {
            List<SpawnedGlade> glades = new List<SpawnedGlade>();
            glades.Add(LevelGenerator.EndGlade);
            LevelGenerator.EndGlade.SetVisibility(true);
            LevelGenerator.EndGlade.SetVisibility(true); 
            GladesStaticEvents.InvokeUnlockGlades(glades);
        }
    }
}