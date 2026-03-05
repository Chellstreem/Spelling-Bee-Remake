using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Rock Installer", menuName = "Installers/Rock Installer")]
public class RockInstaller : ScriptableObjectInstaller
{
    [SerializeField] private RockConfig config;

    public override void InstallBindings()
    {
        Container.Bind<RockConfig>()
            .FromInstance(config)
            .AsSingle();

        Container.BindInterfacesTo<RockPool>().AsSingle().NonLazy();
        Container.Bind<IRockDropper>().To<Rockfall>().AsSingle();
        Container.BindInterfacesTo<RockFallHandler>().AsSingle().NonLazy();
    }
}
