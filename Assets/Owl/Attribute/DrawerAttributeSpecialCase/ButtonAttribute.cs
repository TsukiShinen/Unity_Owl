using System;

namespace Owl.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ButtonAttribute : SpecialCaseDrawerAttribute
    {
        public string Text { get; private set; }

        public ButtonAttribute(string text = null)
        {
            this.Text = text;
        }
    }
}