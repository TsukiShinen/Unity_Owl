using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace OwlEditor
{
    public class OnValueChangedAttribute : PropertyAttribute
    {
        public string CallbackMethodName { get; }

        public OnValueChangedAttribute(string callbackMethodName)
        {
            CallbackMethodName = callbackMethodName;
        }
    }

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
                    OnValueChangedAttribute attribute = GetCustomAttribute<OnValueChangedAttribute>(property);
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

        private TAttribute GetCustomAttribute<TAttribute>(SerializedProperty property) where TAttribute : System.Attribute
        {
            System.Type targetType = property.serializedObject.targetObject.GetType();
            FieldInfo fieldInfo = targetType.GetField(property.name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            return fieldInfo?.GetCustomAttribute<TAttribute>();
        }

        private System.Collections.IEnumerator DelayedCallback(float delay, System.Action callback)
        {
            yield return new WaitForSeconds(delay);
            callback?.Invoke();
        }
    }
}