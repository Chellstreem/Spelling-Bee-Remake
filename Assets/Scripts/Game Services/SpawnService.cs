using Installers;
using Sound;
using Spawn;
using UnityEngine;
using Zenject;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Spawn Service", menuName = "Scriptable Objects/Services/Spawn Service")]
    public class SpawnService : GameModule
    {
        public override void Install(GameContext gameContext, MainStageInstaller installer, GameConfig config)
        {
            DiContainer container = installer.DiContainer;

            UnitPool unitPool = new(container, config.SpawnConfig);
            gameContext.Register(unitPool);

            installer.DiContainer.Bind<UnitPool>()
                .FromInstance(unitPool)
                .AsSingle()
                .NonLazy();

            Spawner spawner = new(unitPool);
            gameContext.Register(spawner);

            container.Bind<Spawner>()
                .FromInstance(spawner)
                .AsSingle()
                .NonLazy();
        }
    }
}