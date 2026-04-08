using UnityEngine;
using Units;
using BackgroundControl;

namespace Installers
{
    public class MainStageInstaller : SceneInstaller
    {
        [SerializeField] private Player _player;

        public override void InstallBindings()
        {
            base.InstallBindings();

            GameplayController gameplayController = new(_gameConfig, _player, _services);

            Container.Bind<GameplayController>()
               .FromInstance(gameplayController)
               .AsSingle()
               .NonLazy();
        }
    }
}
