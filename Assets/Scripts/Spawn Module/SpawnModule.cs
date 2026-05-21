using SpawnModule;
using UnityEngine;
using Zenject;
using GameModules;

namespace SpawnModule
{
    [CreateAssetMenu(fileName = "Spawn Module", menuName = "Game/Spawn/Spawn Module")]
    public class SpawnModule : GameModule
    {
        [SerializeField] private SpawnConfig _spawnConfig;

        public override void Install(Installers.SceneInstaller installer, GameConfig config)
        {
            DiContainer container = installer.DiContainer;

            UnitPool unitPool = new(container, _spawnConfig);

            installer.DiContainer.Bind<UnitPool>()
                .FromInstance(unitPool)
                .AsSingle()
                .NonLazy();

            UnitSpawner spawner = new(unitPool);

            container.Bind<UnitSpawner>()
                .FromInstance(spawner)
                .AsSingle()
                .NonLazy();
        }
    }
}