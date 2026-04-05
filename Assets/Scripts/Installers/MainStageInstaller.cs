using Zenject;
using UnityEngine;
using Units;
using GameModules;

namespace Installers
{
    public class MainStageInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Player _player;

        [SerializeField] private GameModule[] _gameServices;

        private GameConfig _gameConfig;
        private CoroutineRunner _coroutineRunner;
        private GameContext _gameContext;
        private GameplayController _gameplayController;

        public Camera Camera => _mainCamera;
        public DiContainer DiContainer => Container;

        [Inject]
        public void Construct(GameConfig gameConfig, CoroutineRunner coroutineRunner)
        {
            _gameConfig = gameConfig;
            _coroutineRunner = coroutineRunner;
        }

        public override void InstallBindings()
        {
            _gameContext = new();
            _gameContext.Register(_coroutineRunner);

            foreach (var service in _gameServices)
                service.Install(_gameContext, this, _gameConfig);

            InstallGameplayController();
        }

        private void InstallGameplayController()
        {
            _gameplayController = new(_gameConfig, _player, _gameContext);

            Container.Bind<GameplayController>()
               .FromInstance(_gameplayController)
               .AsSingle()
               .NonLazy();
        }
    }
}
