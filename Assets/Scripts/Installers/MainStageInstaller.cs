using UnityEngine;
using Units;

namespace Installers
{
    public class MainStageInstaller : Installer
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
