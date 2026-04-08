using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Speed Control Module", menuName = "Scriptable Objects/Services/Speed Control Module")]
    public class SpeedControlModule : GameModule
    {
        public override void Install(GameServices gameContext, SceneInstaller installer, GameConfig config)
        {
            installer.DiContainer.Bind<GameSpeedController>()
            .AsSingle()
            .NonLazy();
        }
    }
}