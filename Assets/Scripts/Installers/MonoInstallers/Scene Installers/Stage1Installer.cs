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

    [SerializeField] private RectTransform gameoverMenuTransform;
    [SerializeField] private GameObject countdownBar;
    [SerializeField] private GameObject missileAlertBar;

    public override void InstallBindings()
    {
        Container.Bind<CoroutineRunner>()
            .FromInstance(_coroutineRunner)
            .AsSingle()
            .NonLazy();

        Container.Bind<GameSpeedController>()
            .AsSingle()
            .NonLazy();

        InstallGameStates();
        InstallCamera();

        ObjectPool pool = new(Container, _gameConfig.SpawnConfig);

        Container.Bind<ObjectPool>()
            .FromInstance(pool)
            .AsSingle()
            .NonLazy();

        pool.InitializePool();

        Container.Bind<Spawner>()
            .AsSingle()
            .NonLazy();

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

    private void InstallGameStates()
    {
        _stateController = new(_gameConfig.GameStateConfig);

        Container.Bind<GameStateController>()
            .FromInstance(_stateController)
            .AsSingle()
            .NonLazy();

        foreach (var state in _gameConfig.GameStateConfig.GameStates)
        {
            Container.Inject(state);
        }
    }
}
