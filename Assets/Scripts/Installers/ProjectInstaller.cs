using SceneControl;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "Project Installer", menuName = "Game/Project Installer")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        [Tooltip("Project-wide configuration asset injected into the DI container")]
        [SerializeField] private GameConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<GameConfig>().FromInstance(_config).AsSingle().NonLazy();

            SceneController sceneController = new(_config.SceneCollection);
            Container.Bind<SceneController>().FromInstance(sceneController).AsSingle().NonLazy();
        }
    }
}