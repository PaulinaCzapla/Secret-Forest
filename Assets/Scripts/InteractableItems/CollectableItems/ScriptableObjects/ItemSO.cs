using System;
using System.Collections.Generic;
using System.ComponentModel;
using InteractableItems.CollectableItems.Items;
using UnityEditor;
using UnityEngine;
using Attributes;
using InteractableItems.CollectableItems.Items.Types;
using ValueType = InteractableItems.CollectableItems.Items.Types.ValueType;

namespace InteractableItems.CollectableItems.ScriptableObjects
{
    /// <summary>
    /// A base scriptable object class that represents an item. It contains it's name, type, sprite and it.
    /// </summary>
    public abstract class ItemSO : ScriptableObject
    {
        public string ID => id;
        
        [SerializeField] protected ItemType type;
        [SerializeField] protected string name;
        [SerializeField] protected Sprite sprite;

      [HideInInspector] [SerializeField] protected string id;

       [HideInInspector] [SerializeField] private bool wasIdSet = false;

#if UNITY_EDITOR

        /// <summary>
        /// Called every time the scriptable object is edited. It sets an unique ID if it wasn't set yet.
        /// </summary>
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
        /// <summary>
        /// Returns an Item object.
        /// </summary>
        public abstract Item GetItem();
        /// <summary>
        /// Returns an Item object that is initialized with given parameters.
        /// </summary>
        public abstract Item GetItem(List<ValueType> initValues);
    }
}