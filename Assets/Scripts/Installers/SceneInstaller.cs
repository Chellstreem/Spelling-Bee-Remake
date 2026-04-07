using GameModules;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameModule[] _gameModules;
        protected GameConfig _gameConfig;
        protected CoroutineRunner _coroutineRunner;
        protected GameServices _services;

        public Camera Camera => _camera;
        public DiContainer DiContainer => Container;

        [Inject]
        public void Construct(GameConfig gameConfig, CoroutineRunner coroutineRunner)
        {
            _gameConfig = gameConfig;
            _coroutineRunner = coroutineRunner;
        }

        public override void InstallBindings()
        {
            _services = new();
            _services.Register(_coroutineRunner);

            foreach (var module in _gameModules)
                module.Install(_services, this, _gameConfig);
        }
    }
}