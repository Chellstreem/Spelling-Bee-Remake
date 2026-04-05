using Zenject;
using UnityEngine;
using CameraControl;
using GameStates;
using Spawn;
using WordControl;
using InputControl;
using Units;
using UserInterface;
using Sound;
using VFX;

namespace Installers
{
    public class MainStageInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Player _player;

        private GameConfig _gameConfig;
        private CoroutineRunner _coroutineRunner;
        private ParticlePlayer _particlePlayer;
        private GameStateController _stateController;
        private UIBarController _uIBarController;
        private GameSpeedController _speedController;
        private UnitPool _unitPool;
        private Spawner _spawner;
        private WordController _wordController;
        private IInput _input;
        private AudioSourcePool _audioSourcePool;
        private SoundController _soundController;
        private GameContext _gameContext;
        private GameplayController _gameplayController;

        [Inject]
        public void Construct(GameConfig gameConfig, CoroutineRunner coroutineRunner, ParticlePlayer particlePlayer)
        {
            _gameConfig = gameConfig;
            _coroutineRunner = coroutineRunner;
            _particlePlayer = particlePlayer;
        }

        public override void InstallBindings()
        {
            InstallSound();
            InstallInput();
            InstallObjectPool();
            InstallSpeedController();
            InstallSpawner();
            InstallWordController();
            InstallGameStates();
            InstallUIBarController();
            InstallCamera();
            InstallCursor();

            _gameContext = new(_stateController, _coroutineRunner, _unitPool, _spawner, _speedController, _audioSourcePool,
                _particlePlayer, _wordController, _input);

            _stateController.Initialize(_gameContext);

            _gameplayController = new(_gameConfig, _player, _gameContext);

            Container.Bind<GameplayController>()
               .FromInstance(_gameplayController)
               .AsSingle()
               .NonLazy();
        }

        private void InstallCamera()
        {
            Container.Bind<Camera>()
               .FromInstance(_mainCamera)
               .AsSingle()
               .NonLazy();

            CameraMover cameraMover = new(_coroutineRunner);
            CameraController cameraController = new(_gameConfig.CameraConfig, _mainCamera, cameraMover, _stateController);
        }

        private void InstallSpeedController()
        {
            _speedController = new(_coroutineRunner, _gameConfig);

            Container.Bind<GameSpeedController>()
                .FromInstance(_speedController)
                .AsSingle()
                .NonLazy();
        }

        private void InstallGameStates()
        {
            _stateController = new(_gameConfig.GameStateConfig);

            Container.Bind<GameStateController>()
                .FromInstance(_stateController)
                .AsSingle()
                .NonLazy();
        }

        private void InstallObjectPool()
        {
            _unitPool = new(Container, _gameConfig.SpawnConfig);

            Container.Bind<UnitPool>()
                .FromInstance(_unitPool)
                .AsSingle()
                .NonLazy();
        }

        private void InstallSpawner()
        {
            _spawner = new(_unitPool);

            Container.Bind<Spawner>()
                .FromInstance(_spawner)
                .AsSingle()
                .NonLazy();
        }

        private void InstallWordController()
        {
            _wordController = new(_gameConfig.WordControlConfig);

            Container.Bind<WordController>()
                .FromInstance(_wordController)
                .AsSingle()
                .NonLazy();
        }

        private void InstallInput()
        {
            _input = new DesktopInput();

            Container.Bind<IInput>()
                .FromInstance(_input)
                .AsSingle()
                .NonLazy();
        }

        private void InstallUIBarController()
        {
            _uIBarController = new(_stateController);

            Container.Bind<UIBarController>()
                .FromInstance(_uIBarController)
                .AsSingle()
                .NonLazy();
        }

        private void InstallSound()
        {
            _audioSourcePool = new(_gameConfig.SoundConfig, _mainCamera);

            Container.Bind<AudioSourcePool>()
                .FromInstance(_audioSourcePool)
                .AsSingle()
                .NonLazy();

            _soundController = new(_audioSourcePool, _gameConfig.SoundConfig);
        }

        private void InstallCursor()
        {
            CursorController controller = new(_stateController);
        }

        private void OnDisable()
        {
            _soundController.Dispose();
            _uIBarController.Dispose();
        }
    }
}
