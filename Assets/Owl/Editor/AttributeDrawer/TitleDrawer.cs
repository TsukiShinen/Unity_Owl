using Owl.Attribute;
using Owl.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace Owl.Editor
{
    [CustomPropertyDrawer(typeof(TitleAttribute))]
    public class TitleDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            TitleAttribute attribute = PropertyUtility.GetAttribute<TitleAttribute>(property);

            var gui = new GUIStyle();
            gui.fontStyle = FontStyle.Bold;
            gui.fontSize = 15;
            gui.normal.textColor = Color.white;
            gui.padding.top = 10;

            GUILayout.Space(15);
            EditorGUI.LabelField(position, attribute.Name, gui); 
            Rect lineRect = EditorGUILayout.GetControlRect(false, 1);
            lineRect.height = 1;
            EditorGUI.DrawRect(lineRect, Color.white);
            GUILayout.Space(5);

            EditorGUILayout.PropertyField(property, true);

            EditorGUI.EndProperty();
        }
    }
}
