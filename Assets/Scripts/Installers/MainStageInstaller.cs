using UnityEngine;
using Units;

namespace Installers
{
    public class MainStageInstaller : SceneInstaller
    {
        [SerializeField] private Player _player;

        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.Bind<GameplayController>()
               .AsSingle()
               .WithArguments(_player)
               .NonLazy();
        }
    }
}
