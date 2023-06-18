using UnityEngine;

namespace OwlAttribute
{
    public class TitleAttribute : PropertyAttribute
    {
        public string Name { get; }

        public TitleAttribute (string name = "New Title")
        {
            Name = name;
        }
    }
}
