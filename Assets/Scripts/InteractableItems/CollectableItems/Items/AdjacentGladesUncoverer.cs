using System.Collections.Generic;
using GameManager;
using Glades;
using InteractableItems.CollectableItems.Interfaces;
using InteractableItems.CollectableItems.Items.Types;
using LevelGenerating;
using UI.Eq;
using UnityEngine;

namespace InteractableItems.CollectableItems.Items
{
    /// <summary>
    /// A class that represents an item, that uncovers adjacent glades. It implements IUsable interface, so player can use this item.
    /// </summary>
    public class AdjacentGladesUncoverer : Item, IUsable
    {
        public AdjacentGladesUncoverer(Sprite sprite, string name, string id, ItemType type) 
            : base(sprite, name, id, type)
        {
        }
        public override string GetString()
        {
            return "Witch cat's eye. Reveals the nearest glades.";
        }
        /// <summary>
        /// Changes visibility of all glades adjacent to the one player is on.
        /// </summary>
        public void Use()
        {
            List<SpawnedGlade> glades = new List<SpawnedGlade>();
            foreach (var glade in GameController.GetInstance().CurrentGlade.AdjacentGlades)
            {
                if (!glade.Value.SpawnedGlade.IsVisible)
                {
                    glades.Add(glade.Value.SpawnedGlade);
                    glade.Value.SpawnedGlade.SetVisibility(true);
                    glade.Value.SpawnedGlade.SetVisibility(true);
                }
            }
            
            if(glades.Count > 0)
                GladesStaticEvents.InvokeUnlockGlades(glades);
        }

    }
}