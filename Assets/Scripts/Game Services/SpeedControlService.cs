using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Speed Control Service", menuName = "Scriptable Objects/Services/Speed Control Service")]
    public class SpeedControlService : GameModule
    {
        public override void Install(GameContext gameContext, MainStageInstaller installer, GameConfig config)
        {
            GameSpeedController speedController = new(gameContext.Get<CoroutineRunner>(), config);
            gameContext.Register(speedController);

            installer.DiContainer.Bind<GameSpeedController>()
                .FromInstance(speedController)
                .AsSingle()
                .NonLazy();
        }
    }
}