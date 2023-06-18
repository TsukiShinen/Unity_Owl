
using System.Reflection;
using UnityEditor;

namespace Owl.ditor
{
    internal class OwlEditorUtils
    {
        internal static TAttribute GetCustomAttribute<TAttribute>(SerializedProperty property) where TAttribute : System.Attribute
        {
            System.Type targetType = property.serializedObject.targetObject.GetType();
            FieldInfo fieldInfo = targetType.GetField(property.name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return fieldInfo?.GetCustomAttribute<TAttribute>();
        }
    }
}
