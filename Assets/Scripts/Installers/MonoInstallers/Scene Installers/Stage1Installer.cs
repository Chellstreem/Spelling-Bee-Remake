using Zenject;
using UnityEngine;
using CameraControl;
using GameStates;

public class Stage1Installer : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;

    [Header("Scene References")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [SerializeField] private RectTransform gameoverMenuTransform;
    [SerializeField] private GameObject countdownBar;
    [SerializeField] private GameObject missileAlertBar;

    [Inject]
    private GameStateController _gameStateController;

    public override void InstallBindings()
    {
        Container.Bind<CoroutineRunner>()
            .FromInstance(_coroutineRunner)
            .AsSingle()
            .NonLazy();

        InstallCamera();

        Container.Bind<GameSpeedController>()
            .AsSingle()
            .WithArguments(_gameConfig)
            .NonLazy();

        Container.Bind<GameConfig>()
            .FromInstance(_gameConfig)
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
        Container.Bind<GameSpeedController>().AsSingle().NonLazy();

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
        CameraController cameraController = new(_mainCamera, cameraMover, _gameStateController);
    }
}
