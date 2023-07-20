using Owl.Attribute;
using UnityEngine;

public class TestEditor : MonoBehaviour
{
    [Title("Test")]
    [SerializeField] private bool show;
    [SerializeField, Scene, ShowIf("show")] private string scene;
}
