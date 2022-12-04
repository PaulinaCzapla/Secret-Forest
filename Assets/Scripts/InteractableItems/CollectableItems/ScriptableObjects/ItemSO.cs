using System;
using System.Collections.Generic;
using System.ComponentModel;
using InteractableItems.CollectableItems.Items;
using UnityEditor;
using UnityEngine;
using Attributes;
using ValueType = InteractableItems.CollectableItems.Items.ValueType;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    public abstract class ItemSO : ScriptableObject, IRandomizable<Item>
    {
        public string ID => id;
        
        [SerializeField] protected ItemType type;
        [SerializeField] protected string name;
        [SerializeField] protected Sprite sprite;

       [Attributes.ReadOnly] [SerializeField] protected string id;

       [HideInInspector] [SerializeField] private bool wasIdSet = false;
       private void OnValidate()
        {
            if (!wasIdSet)
            {
                wasIdSet = true;
                id = GUID.Generate().ToString();
                EditorUtility.SetDirty(this);
            }
        }

        public abstract Item GetRandom();
        public abstract Item GetItem(ItemType initType, List<ValueType> initValues);
    }
}