using SceneControl;
using UnityEngine;
using VFX;
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
            InstallVFX();
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

        private void InstallVFX()
        {
            ParticlePool pool = new(_config.ParticleConfig);
            ParticlePlayer particlePlayer = new(pool, _runner);

            Container.Bind<ParticlePlayer>()
                .FromInstance(particlePlayer)
                .AsSingle()
                .NonLazy();

            ObjectScaler scaler = new();

            VisualEffectServices services = new();
            services.RegisterService(particlePlayer);
            services.RegisterService(scaler);

            Container.Bind<VisualEffectServices>()
                .FromInstance(services)
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