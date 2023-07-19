using UnityEditor;

namespace Owl.Attribute
{
    public class ValidatorAttribute : System.Attribute, IOwlAttribute 
    {
        public virtual void ValidateProperty(SerializedProperty property) { }
    }
}