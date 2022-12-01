using System.Collections.Generic;
using GameManager;
using Glades;
using LevelGenerating;
using RandomGenerators;
using UI.Eq;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace InteractableItems.CollectableItems.Items
{
    public class RandomGladesUncoverer : Item, IUsable
    {
        private int _gladesToUncover = 3;
        public RandomGladesUncoverer(Sprite sprite, string name) : base(sprite, name)
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
            return "The enchanted eye of the witch's cat. No distance can keep the secret from it.";
        }

        public void Use()
        {
            List<SpawnedGlade> glades = new List<SpawnedGlade>();

            foreach (var glade in LevelGenerator.SpawnedGlades)
            {
                if(!glade.IsVisible)
                    glades.Add(glade);
            }

            glades =RandomElementsGenerator.GetRandom(glades, Mathf.Min(_gladesToUncover, glades.Count));
           
            if (glades.Count > 0)
            {
                foreach (var glade in glades)
                {
                    glade.SetVisibility(true);
                    glade.SetVisibility(true);
                }

                ItemsStaticEvents.InvokeUnlockGlades(glades);
            }
        }
    }
}