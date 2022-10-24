using System.Collections.Generic;
using InteractableItems;
using UnityEngine;

namespace Glades.GladeTypes
{
    public class GladeStorage : Glade
    {
        [SerializeField] private List<Chest> chests;
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}