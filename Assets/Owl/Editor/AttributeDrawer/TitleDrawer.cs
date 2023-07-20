using Owl.Attribute;
using Owl.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Owl.Editor
{
    [CustomPropertyDrawer(typeof(TitleAttribute))]
    public class TitleDrawer : PropertyDrawerBase
    {

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            TitleAttribute attribute = PropertyUtility.GetAttribute<TitleAttribute>(property);

            var gui = new GUIStyle();
            gui.fontStyle = FontStyle.Bold;
            gui.fontSize = 15;
            gui.normal.textColor = Color.white;
            gui.padding.top = 10;

            GUILayout.Space(15);
            EditorGUI.LabelField(rect, attribute.Name, gui);
            Rect lineRect = EditorGUILayout.GetControlRect(false, 1);
            lineRect.height = 1;
            EditorGUI.DrawRect(lineRect, Color.white);
            GUILayout.Space(5);

            EditorGUILayout.PropertyField(property, true);

            EditorGUI.EndProperty();
        }
    }
}
