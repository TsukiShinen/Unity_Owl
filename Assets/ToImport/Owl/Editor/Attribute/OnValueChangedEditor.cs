using OwlAttribute;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace OwlEditor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class OnValueChangedDrawer : Editor
    {
        private bool shouldInvokeCallback;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Object targetObject = target;
            MethodInfo methodInfo = null;

            SerializedProperty property = serializedObject.GetIterator();
            bool enterChildren = true;
            while (property.NextVisible(enterChildren))
            {
                if (property.propertyType != SerializedPropertyType.Generic)
                {
                    OnValueChangedAttribute attribute = OwlEditorUtils.GetCustomAttribute<OnValueChangedAttribute>(property);
                    if (attribute != null)
                    {
                        string methodName = attribute.CallbackMethodName;

                        methodInfo = targetObject.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (methodInfo != null)
                        {
                            EditorGUI.BeginChangeCheck();
                            EditorGUI.PropertyField(EditorGUILayout.GetControlRect(), property, true);
                            if (EditorGUI.EndChangeCheck())
                            {
                                shouldInvokeCallback = true;
                            }
                        }
                        else
                        {
                            Debug.LogWarning($"Callback method '{methodName}' not found in {targetObject.GetType()}");
                        }
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(property, true);
                    }
                }
                else
                {
                    EditorGUILayout.PropertyField(property, true);
                }

                enterChildren = false;
            }

            serializedObject.ApplyModifiedProperties();
            if (shouldInvokeCallback)
            {
                methodInfo.Invoke(targetObject, null);
                shouldInvokeCallback = false;
            }
        }
    }
}