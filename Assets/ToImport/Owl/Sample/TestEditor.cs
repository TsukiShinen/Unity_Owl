using UnityEngine;

public class TestEditor : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField, Scene] private string _sceneName;
    [SerializeField, Scene] private int _sceneId;

    [Header("Enums")]
    [SerializeField, EnumPaging] private Test _enumPaging;


    public enum Test { None, Test, Test2 }
}
