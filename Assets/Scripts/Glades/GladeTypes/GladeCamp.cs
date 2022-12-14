using InteractableItems.CollectableItems.Items.InteractableItems;
using UnityEngine;

namespace Glades.GladeTypes
{
    public class GladeCamp : BaseGlade
    {
        [SerializeField] private Tent tent;
        public override void Initialize()
        {
          base.Initialize();
        }
    }
}