using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Speed Control Module", menuName = "Scriptable Objects/Services/Speed Control Module")]
    public class SpeedControlService : GameModule
    {
        public override void Install(GameServices services, SceneInstaller installer, GameConfig config)
        {
            GameSpeedController speedController = new(services.Get<CoroutineRunner>(), config);
            services.Register(speedController);

            installer.DiContainer.Bind<GameSpeedController>()
                .FromInstance(speedController)
                .AsSingle()
                .NonLazy();
        }
    }
}