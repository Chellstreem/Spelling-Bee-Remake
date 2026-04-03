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
using SceneControl;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;

    [Header("Scene References")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private Player _player;

    private GameStateController _stateController;
    private UIBarController _uIBarController;
    private GameSpeedController _speedController;
    private ObjectPool _objectPool;
    private Spawner _spawner;
    private WordController _wordController;
    private IInput _input;
    private AudioSourcePool _audioSourcePool;
    private SoundController _soundController;
    private ParticlePlayer _particlePlayer;
    private GameContext _gameContext;
    private GameplayController _gameplayController;

    public override void InstallBindings()
    {
        Container.Bind<GameConfig>()
        .FromInstance(_gameConfig)
        .AsSingle()
        .NonLazy();

        Container.Bind<CoroutineRunner>()
        .FromInstance(_coroutineRunner)
        .AsSingle()
        .NonLazy();

        InstallSceneControl();
        InstallVFX();
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

        _gameContext = new(_stateController, _coroutineRunner, _objectPool, _spawner, _speedController, _audioSourcePool,
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
        _objectPool = new(Container, _gameConfig.SpawnConfig);

        Container.Bind<ObjectPool>()
            .FromInstance(_objectPool)
            .AsSingle()
            .NonLazy();
    }

    private void InstallSpawner()
    {
        _spawner = new(_objectPool);

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
        _audioSourcePool = new(_gameConfig.SoundConfig);

        Container.Bind<AudioSourcePool>()
            .FromInstance(_audioSourcePool)
            .AsSingle()
            .NonLazy();

        _soundController = new(_audioSourcePool, _gameConfig.SoundConfig);
    }

    private void InstallVFX()
    {
        ParticlePool pool = new(_gameConfig.ParticleConfig);
        _particlePlayer = new(pool, _coroutineRunner);

        Container.Bind<ParticlePlayer>()
            .FromInstance(_particlePlayer)
            .AsSingle()
            .NonLazy();

        ObjectScaler scaler = new();

        VisualEffectServices services = new();
        services.RegisterService(_particlePlayer);
        services.RegisterService(scaler);

        Container.Bind<VisualEffectServices>()
            .FromInstance(services)
            .AsSingle()
            .NonLazy();
    }

    private void InstallSceneControl()
    {
        SceneController sceneController = new(_gameConfig.SceneCollection);

        Container.Bind<SceneController>()
            .FromInstance(sceneController)
            .AsSingle()
            .NonLazy();
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
