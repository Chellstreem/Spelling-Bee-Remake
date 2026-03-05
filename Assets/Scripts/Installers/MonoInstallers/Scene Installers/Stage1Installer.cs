using Zenject;
using UnityEngine;

public class Stage1Installer : MonoInstaller
{
    [SerializeField] private RectTransform gameoverMenuTransform;
    [SerializeField] private GameObject countdownBar;
    [SerializeField] private GameObject missileAlertBar;
    public override void InstallBindings()
    {
        Container.Bind<RectTransform>()
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
        Container.Bind<GameSpeed>().AsSingle().NonLazy();
        
        InstallUIHandlers();        
        InstallCursor();
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
}
