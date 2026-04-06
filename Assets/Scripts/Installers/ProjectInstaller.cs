using SceneControl;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "Project Installer", menuName = "Scriptable Objects/Project Installer")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(_config).AsSingle().NonLazy();

            GameObject newObject = new("Coroutine Runner");
            CoroutineRunner runner = newObject.AddComponent<CoroutineRunner>();
            Container.Bind<CoroutineRunner>().FromInstance(runner).AsSingle().NonLazy();

            SceneController sceneController = new(_config.SceneCollection);
            Container.Bind<SceneController>().FromInstance(sceneController).AsSingle().NonLazy();
        }
    }
}