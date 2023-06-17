using OwlEditor;
using UnityEngine;

public class TestEditor : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField, Scene] private string _sceneName;
    [SerializeField, Scene] private int _sceneId;

    [Header("Enums")]
    [SerializeField, EnumPaging] private Test _enumPaging;
    [SerializeField, EnumPaging, OnValueChanged("OnEnumChanged")] private Test _enumPagingWithDebug;

    private void OnEnumChanged()
    {
        Debug.Log("New Enum Value : " + _enumPagingWithDebug);
    }

    public enum Test { None, Test, Test2 }
}
