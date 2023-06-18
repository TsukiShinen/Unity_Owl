using OwlAttribute;
using UnityEditor;
using UnityEngine;

namespace OwlEditor
{
    [CustomPropertyDrawer(typeof(EnumPagingAttribute))]
    public class EnumPagingDrawer : PropertyDrawer
    {
        private const float ButtonWidth = 20f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.Enum)
            {
                EditorGUI.BeginChangeCheck();

                Rect dropdownPosition = position;
                dropdownPosition.width -= ButtonWidth * 2;

                Rect previousButtonPosition = position;
                previousButtonPosition.width = ButtonWidth;
                previousButtonPosition.x = position.xMax - ButtonWidth * 2;

                Rect nextButtonPosition = position;
                nextButtonPosition.width = ButtonWidth;
                nextButtonPosition.x = position.xMax - ButtonWidth;

                System.Type enumType = fieldInfo.FieldType;
                System.Enum enumValue = System.Enum.ToObject(enumType, property.intValue) as System.Enum;

                enumValue = EditorGUI.EnumPopup(dropdownPosition, label, enumValue);

                if (EditorGUI.EndChangeCheck())
                {
                    property.intValue = System.Convert.ToInt32(enumValue);
                }

                System.Array enumValues = System.Enum.GetValues(enumType);
                int currentIndex = System.Array.IndexOf(enumValues, enumValue);

                if (currentIndex > 0)
                {
                    if (GUI.Button(previousButtonPosition, "<"))
                    {
                        currentIndex--;
                        enumValue = enumValues.GetValue(currentIndex) as System.Enum;
                        property.intValue = System.Convert.ToInt32(enumValue);
                    }
                }

                if (currentIndex < enumValues.Length - 1)
                {
                    if (GUI.Button(nextButtonPosition, ">"))
                    {
                        currentIndex++;
                        enumValue = enumValues.GetValue(currentIndex) as System.Enum;
                        property.intValue = System.Convert.ToInt32(enumValue);
                    }
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use EnumPaging with enum fields only.");
            }

            EditorGUI.EndProperty();
        }
    }
}