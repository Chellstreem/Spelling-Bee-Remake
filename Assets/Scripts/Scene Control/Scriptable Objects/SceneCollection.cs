using UnityEngine;

[CreateAssetMenu(fileName = "Scene Collection", menuName = "Scriptable Objects/Scene/Scene Collection")]
public class SceneCollection : ScriptableObject
{
    [SerializeField] private SceneData[] scenes;

    public SceneData[] Scenes => scenes;
}
