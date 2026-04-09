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
            foreach (var module in _gameModules)
                module.Install(this, _gameConfig);
        }

        private void OnDestroy()
        {
            foreach (var module in _gameModules)
                module.Dispose(this);
        }
    }
}