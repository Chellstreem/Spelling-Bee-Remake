using Spawn;
using UnityEngine;
using Zenject;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Spawn Module", menuName = "Scriptable Objects/Services/Spawn Module")]
    public class SpawnModule : GameModule
    {
        public override void Install(GameServices services, Installers.Installer installer, GameConfig config)
        {
            DiContainer container = installer.DiContainer;

            UnitPool unitPool = new(container, config.SpawnConfig);
            services.Register(unitPool);

            installer.DiContainer.Bind<UnitPool>()
                .FromInstance(unitPool)
                .AsSingle()
                .NonLazy();

            UnitSpawner spawner = new(unitPool);
            services.Register(spawner);

            container.Bind<UnitSpawner>()
                .FromInstance(spawner)
                .AsSingle()
                .NonLazy();
        }
    }
}