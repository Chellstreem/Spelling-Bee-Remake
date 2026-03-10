using Spawn;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Spawn Installer", menuName = "Installers/Spawn Installer")]
public class SpawnInstaller : ScriptableObjectInstaller
{
    [SerializeField] private SpawnConfig _spawnConfig;
    public override void InstallBindings()
    {
        Container.Bind<ObjectPool>()
            .AsSingle()
            .WithArguments(_spawnConfig)
            .NonLazy();

        Container.Bind<Spawner>()
            .AsSingle()
            .NonLazy();

        /*Container.Bind<PoolInitializer>().AsSingle().NonLazy();

        Container.Bind<ISpawner>()
            .WithId(SpawnerType.Decorative)
            .To<DecorativeObjectSpawner>()
            .AsSingle();

        Container.Bind<ISpawner>()
            .WithId(SpawnerType.Interactable)
            .To<InteractableObjectSpawner>()
            .AsSingle();

        Container.Bind<ISpawner>()
            .WithId(SpawnerType.Missile)
            .To<MissileSpawner>()
            .AsSingle();

        Container.Bind<ISpawner>()
            .WithId(SpawnerType.Monkey)
            .To<MonkeySpawner>()
            .AsSingle();

        Container.Bind<SpawnEventHandler>().AsSingle().NonLazy();     */
    }
}
