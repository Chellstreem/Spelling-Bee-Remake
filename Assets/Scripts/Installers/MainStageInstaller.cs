using UnityEngine;
using Units;

namespace Installers
{
    public class MainStageInstaller : SceneInstaller
    {
        [Tooltip("Reference to the player prefab/instance used for gameplay bindings")]
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
