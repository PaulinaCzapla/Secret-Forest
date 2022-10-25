using System;
using System.Collections.Generic;

using UI.Eq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace InteractableItems.CollectableItems
{
    public class Food : Item, IUsable
    {
        public float Value => value;
        
        [SerializeField] private  Image image;
        [SerializeField] private List<Sprite> sprites;
        private  int _spriteNum;

        [SerializeField] private float value = 5;
        
        public override void Initialize()
        {
            _spriteNum = Random.Range(0, sprites.Count);
            image.sprite = sprites[_spriteNum];
        }
        public void Initialize(int sprite)
        {
            _spriteNum = sprite;
            image.sprite = sprites[_spriteNum];
        }
        public override void Collect()
        {
            //add to inventory
            // if collected
            StorageUI.Instance.ItemCollected(this);
        }

        public void Use()
        {
            
        }
    }
}