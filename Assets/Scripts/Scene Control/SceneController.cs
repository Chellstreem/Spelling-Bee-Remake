using UnityEngine.SceneManagement;

namespace SceneControl
{
    public enum SceneType
    { Intro, MainMenu, MainStage }

    public class SceneController
    {
        private readonly SceneCollection sceneCollection;

        public SceneController(SceneCollection sceneCollection) => this.sceneCollection = sceneCollection;

        public void SwitchScene(SceneType sceneName)
        {
            string scenePath = sceneCollection.GetScenePath(sceneName);

            if (!string.IsNullOrEmpty(scenePath))
                SceneManager.LoadScene(scenePath);
        }
    }
}
