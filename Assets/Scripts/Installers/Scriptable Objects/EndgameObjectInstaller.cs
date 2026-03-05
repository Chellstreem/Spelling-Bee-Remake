using Spawn;
using System.ComponentModel;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Endgame Object Installer", menuName = "Installers/Endgame Object Installer")]
public class EndgameObjectInstaller : ScriptableObjectInstaller
{
    [SerializeField] private EndgameObjectCollection endgameObjectCollection;

    public override void InstallBindings()
    {
        Container.Bind<EndgameObjectCollection>()
            .FromInstance(endgameObjectCollection)
            .AsSingle();

        Container.Bind<IEndgameObjectGetter>().To<EndgameObjectPool>().AsSingle().NonLazy();
        Container.Bind<IEndgameObjectActivator>().To<EndgameObjectActivator>().AsSingle();
        Container.Bind<EndgameObjectHandler>().AsSingle().NonLazy();
    }    
}
