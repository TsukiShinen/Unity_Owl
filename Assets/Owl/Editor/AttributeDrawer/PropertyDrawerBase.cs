using Owl.Attribute;
using Owl.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Owl.Editor
{

    public abstract class PropertyDrawerBase : PropertyDrawer
    {
        public sealed override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            bool visible = PropertyUtility.IsVisible(property);
            if (!visible)
            {
                return;
            }

            ValidatorAttribute[] validatorAttributes = PropertyUtility.GetAttributes<ValidatorAttribute>(property);
            foreach (var validatorAttribute in validatorAttributes)
            {
                validatorAttribute.GetValidator().ValidateProperty(property);
            }

            EditorGUI.BeginChangeCheck();
            bool enabled = PropertyUtility.IsEnabled(property);

            using (new EditorGUI.DisabledScope(disabled: !enabled))
            {
                OnGUI_Internal(rect, property, PropertyUtility.GetLabel(property));
            }

            if (EditorGUI.EndChangeCheck())
            {
                PropertyUtility.CallOnValueChangedCallbacks(property);
            }
        }

        protected abstract void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label);

        sealed override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool visible = PropertyUtility.IsVisible(property);
            if (!visible)
            {
                return 0.0f;
            }

            return GetPropertyHeight_Internal(property, label);
        }

        protected virtual float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, includeChildren: true);
        }

        protected float GetPropertyHeight(SerializedProperty property)
        {
            return EditorGUI.GetPropertyHeight(property, includeChildren: true);
        }

        public virtual float GetHelpBoxHeight()
        {
            return EditorGUIUtility.singleLineHeight * 2.0f;
        }

        public void DrawDefaultPropertyAndHelpBox(Rect rect, SerializedProperty property, string message, MessageType messageType)
        {
            float indentLength = OwlEditorGUI.GetIndentLength(rect);
            Rect helpBoxRect = new Rect(
                rect.x + indentLength,
                rect.y,
                rect.width - indentLength,
                GetHelpBoxHeight());

            OwlEditorGUI.HelpBox(helpBoxRect, message, MessageType.Warning, context: property.serializedObject.targetObject);

            Rect propertyRect = new Rect(
                rect.x,
                rect.y + GetHelpBoxHeight(),
                rect.width,
                GetPropertyHeight(property));

            EditorGUI.PropertyField(propertyRect, property, true);
        }
    }
}