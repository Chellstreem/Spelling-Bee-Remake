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

public class Stage1Installer : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private Player _player;

    [Inject] private GameConfig _gameConfig;
    private GameStateController _stateController;
    private UIBarController _uIBarController;
    private GameSpeedController _speedController;
    private ObjectPool _pool;
    private Spawner _spawner;
    private WordController _wordController;
    private IInput _input;
    private GameplayController _gameplayController;

    public override void InstallBindings()
    {
        Container.Bind<CoroutineRunner>()
        .FromInstance(_coroutineRunner)
        .AsSingle()
        .NonLazy();

        InstallInput();
        InstallWordController();
        InstallSpeedController();
        InstallObjectPool();
        InstallSpawner();
        InstallGameStates();
        InstallUIBarController();
        InstallCamera();
        InstallSound();

        _wordController.StartGame();
        _pool.InitializePool();
        _input.Enable();
        _gameplayController = new(_player, _wordController, _stateController);
    }

    private void InstallCamera()
    {
        Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle().NonLazy();
        CameraMover cameraMover = new(_coroutineRunner, _gameConfig.CameraConfig);

        CameraController cameraController = new(_mainCamera, cameraMover, _stateController);
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
        _stateController.Initialize(_coroutineRunner, _spawner, _speedController);

        Container.Bind<GameStateController>()
            .FromInstance(_stateController)
            .AsSingle()
            .NonLazy();
    }

    private void InstallObjectPool()
    {
        _pool = new(Container, _gameConfig.SpawnConfig);

        Container.Bind<ObjectPool>()
            .FromInstance(_pool)
            .AsSingle()
            .NonLazy();
    }

    private void InstallSpawner()
    {
        _spawner = new(_pool);

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
        AudioSourcePool pool = new(_gameConfig.SoundConfig);
        SoundController soundController = new(pool, _gameConfig.SoundConfig);
    }
}
