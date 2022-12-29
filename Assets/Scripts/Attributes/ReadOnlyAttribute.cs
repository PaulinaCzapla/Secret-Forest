using UnityEditor;
using UnityEngine;

namespace Attributes
{
#if UNITY_EDITOR
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }

    /// <summary>
    ///  A class that is an attribute that allows to create read-only fields in a editor window
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
            GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif
}