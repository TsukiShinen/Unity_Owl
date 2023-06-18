using UnityEngine;

namespace OwlAttribute
{
    public class OnValueChangedAttribute : PropertyAttribute
    {
        public string CallbackMethodName { get; }

        public OnValueChangedAttribute(string callbackMethodName)
        {
            CallbackMethodName = callbackMethodName;
        }
    }
}