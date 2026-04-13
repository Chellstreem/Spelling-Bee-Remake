using UnityEngine;

namespace SceneControl
{
    [System.Serializable]
    public struct SceneInfo
    {
        [Tooltip("Enum identifier for the scene")]
        [SerializeField] private SceneType _sceneName;
        [Tooltip("Path used by the SceneManager to load this scene")]
        [SerializeField] private string _scenePath;

        public readonly SceneType SceneName => _sceneName;
        public readonly string ScenePath => _scenePath;
    }

    [CreateAssetMenu(fileName = "Scene Collection", menuName = "Scriptable Objects/Scene Collection")]
    public class SceneCollection : ScriptableObject
    {
        [Tooltip("Array of scene entries that can be loaded by name")]
        [SerializeField] private SceneInfo[] _scenes;

        public string GetScenePath(SceneType name)
        {
            foreach (var scene in _scenes)
            {
                if (scene.SceneName == name)
                    return scene.ScenePath;
            }

            Debug.LogError($"Scene {name} not found!");
            return null;
        }
    }
}
