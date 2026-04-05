using SceneControl;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "Project Installer", menuName = "Scriptable Objects/Project Installer")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameConfig _config;
        private CoroutineRunner _runner;

        public override void InstallBindings()
        {
            Container.Bind<GameConfig>()
                .FromInstance(_config)
                .AsSingle()
                .NonLazy();

            InstallCoroutineRunner();
            InstallSceneController();
        }

        private void InstallCoroutineRunner()
        {
            GameObject newObject = new("Coroutine Runner");
            _runner = newObject.AddComponent<CoroutineRunner>();

            Container.Bind<CoroutineRunner>()
                .FromInstance(_runner)
                .AsSingle()
                .NonLazy();
        }

        private void InstallSceneController()
        {
            SceneController sceneController = new(_config.SceneCollection);

            Container.Bind<SceneController>()
                .FromInstance(sceneController)
                .AsSingle()
                .NonLazy();
        }
    }
}