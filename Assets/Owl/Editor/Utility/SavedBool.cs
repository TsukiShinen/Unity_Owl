using UnityEditor;

namespace Owl.Editor.Utility
{
    internal class SavedBool
    {
        private bool _value;
        private string _name;

        public bool Value
        {
            get { return _value; }
            set
            {
                if (_value == value) return;

                _value = value;
                EditorPrefs.SetBool(_name, _value);
            }
        }

        public SavedBool(string name, bool value)
        {
            _name = name;
            _value = EditorPrefs.GetBool(name, value);
        }
    }
}