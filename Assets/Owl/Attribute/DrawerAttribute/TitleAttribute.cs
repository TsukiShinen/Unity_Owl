using UnityEngine;

namespace Owl.Attribute
{
    public class TitleAttribute : DrawerAttribute
    {
        public string Name { get; }

        public TitleAttribute (string name = "New Title")
        {
            Name = name;
        }
    }
}
