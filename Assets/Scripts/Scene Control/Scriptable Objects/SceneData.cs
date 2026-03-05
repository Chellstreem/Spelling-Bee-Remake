using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/Scene/Scene Data")]
public class SceneData : ScriptableObject
{
    [SerializeField] private SceneName sceneName;
    [SerializeField] private string scenePath;

    public SceneName SceneName => sceneName;
    public string ScenePath => scenePath;      
}
