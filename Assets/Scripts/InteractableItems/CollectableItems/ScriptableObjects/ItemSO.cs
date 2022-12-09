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

      [HideInInspector] [SerializeField] protected string id;

       [HideInInspector] [SerializeField] private bool wasIdSet = false;

#if UNITY_EDITOR

       private void OnValidate()
        {
            if (!wasIdSet)
            {
                wasIdSet = true;
                id = GUID.Generate().ToString();
                EditorUtility.SetDirty(this);
            }
        }
        
#endif
        public abstract Item GetItem();
        public abstract Item GetItem(List<ValueType> initValues);
    }
}