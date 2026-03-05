using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScreenObjectInstaller", menuName = "Installers/ScreenObjectInstaller")]
public class ScreenObjectInstaller : ScriptableObjectInstaller
{
    [SerializeField] private ScreenObjectConfig screenObjectConfig;
    public override void InstallBindings()
    {
        Container.Bind<ScreenObjectConfig>()
            .FromInstance(screenObjectConfig)
            .AsSingle();

        Container.Bind<IScreenObjectGetter>().To<ScreenObjectPool>().AsSingle().NonLazy();
        Container.BindInterfacesTo<ScreenObjectMover>().AsSingle();
        Container.Bind<ScreenObjectHandler>().AsSingle().NonLazy();
    }
}
