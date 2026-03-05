using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneControl
{
    public class SceneSwitcher : ISceneSwitcher
    {
        private readonly SceneCollection sceneCollection;

        public SceneSwitcher(SceneCollection sceneCollection)
        {
            this.sceneCollection = sceneCollection;
        }

        public void SwitchScene(SceneName sceneName)
        {
            string scenePath = GetScenePath(sceneName);
            if (!string.IsNullOrEmpty(scenePath))
            {
                SceneManager.LoadScene(scenePath);
            }
        }

        private string GetScenePath(SceneName name)
        {
            foreach (var scene in sceneCollection.Scenes)
            {
                if (scene.SceneName == name)
                    return scene.ScenePath;
            }

            Debug.LogError($"Scene {name} not found!");
            return null;
        }
    }
}
