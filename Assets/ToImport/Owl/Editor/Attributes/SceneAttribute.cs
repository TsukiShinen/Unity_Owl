using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OwlEditor
{
    public class SceneAttribute : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginChangeCheck();

                string[] scenePaths = EditorBuildSettings.scenes
                    .Where(scene => scene.enabled)
                    .Select(scene => scene.path)
                    .ToArray();

                string[] sceneNames = scenePaths
                    .Select(scenePath => System.IO.Path.GetFileNameWithoutExtension(scenePath))
                    .ToArray();

                int selectedIndex = Mathf.Max(0, Array.IndexOf(scenePaths, property.stringValue));

                selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneNames);

                if (EditorGUI.EndChangeCheck())
                {
                    property.stringValue = sceneNames[selectedIndex];
                }
            }
            else if (property.propertyType == SerializedPropertyType.Integer)
            {
                EditorGUI.BeginChangeCheck();

                string[] scenePaths = EditorBuildSettings.scenes
                    .Where(scene => scene.enabled)
                    .Select(scene => scene.path)
                    .ToArray();

                string[] sceneNames = scenePaths
                    .Select(scenePath => System.IO.Path.GetFileNameWithoutExtension(scenePath))
                    .ToArray();

                int selectedIndex = Mathf.Max(0, property.intValue);

                selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneNames);

                if (EditorGUI.EndChangeCheck())
                {
                    property.intValue = selectedIndex;
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Scene Attribute with string or int fields only.");
            }

            EditorGUI.EndProperty();
        }
    }
}