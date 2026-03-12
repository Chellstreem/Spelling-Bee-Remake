using Zenject;
using UnityEngine;
using CameraControl;
using GameStates;
using Spawn;

public class Stage1Installer : MonoInstaller
{
    [Header("Scene References")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [Inject] private GameConfig _gameConfig;
    private GameStateController _stateController;
    private GameSpeedController _speedController;
    private ObjectPool _pool;
    private Spawner _spawner;

    [SerializeField] private RectTransform gameoverMenuTransform;
    [SerializeField] private GameObject countdownBar;
    [SerializeField] private GameObject missileAlertBar;

    public override void InstallBindings()
    {
        Container.Bind<CoroutineRunner>()
            .FromInstance(_coroutineRunner)
            .AsSingle()
            .NonLazy();

        InstallSpeedController();
        InstallObjectPool();
        InstallSpawner();
        InstallGameStates();
        InstallCamera();

        /*Container.Bind<RectTransform>()
            .WithId(UiObjectType.GameoverMenu)
            .FromInstance(gameoverMenuTransform)
            .AsSingle();

        Container.Bind<GameObject>()
            .WithId(UiObjectType.CountDownBar)
            .FromInstance(countdownBar)
            .AsTransient();

        Container.Bind<GameObject>()
            .WithId(UiObjectType.MissileAlertBar)
            .FromInstance(missileAlertBar)
            .AsTransient();

        Container.Bind<EventManager>().AsSingle();
        Container.BindInterfacesTo<LetterGenerator>().AsSingle().NonLazy();        

        InstallUIHandlers();
        InstallCursor();*/
    }

    private void InstallCursor()
    {
        Container.Bind<CursorActivator>().AsSingle();
        Container.Bind<CursorHandler>().AsSingle().NonLazy();
    }

    private void InstallUIHandlers()
    {
        Container.Bind<IInitializable>().To<GameOverMenuHandler>().AsSingle().NonLazy();
        Container.Bind<ActivatorUI>().AsSingle().NonLazy();
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

        _pool.InitializePool();
    }

    private void InstallSpawner()
    {
        _spawner = new(_pool);

        Container.Bind<Spawner>()
        .FromInstance(_spawner)
            .AsSingle()
            .NonLazy();
    }
}
