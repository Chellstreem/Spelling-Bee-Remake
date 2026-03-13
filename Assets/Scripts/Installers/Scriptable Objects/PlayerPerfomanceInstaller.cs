using PlayerPerfomance;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerPerfomanceInstaller", menuName = "Installers/PlayerPerfomanceInstaller")]
public class PlayerPerfomanceInstaller : ScriptableObjectInstaller
{
    [SerializeField] private PlayerPerfomanceConfig config;

    public override void InstallBindings()
    {
        Container.Bind<PlayerPerfomanceConfig>()
            .FromInstance(config)
            .AsSingle();

        Container.Bind<WrongLetterHandler>().AsSingle().NonLazy();
        Container.BindInterfacesTo<PlayerPerfomanceHandler>().AsSingle().NonLazy();
    }
}
