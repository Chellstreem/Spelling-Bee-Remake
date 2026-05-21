using Installers;
using UnityEngine;
using GameModules;

namespace SpeedControlModule
{
    [CreateAssetMenu(fileName = "Speed Control Module", menuName = "Game/Speed/Speed Control Module")]
    public class SpeedControlModule : GameModule
    {
        public override void Install(SceneInstaller installer, GameConfig config)
        {
            installer.DiContainer.Bind<GameSpeedController>()
            .AsSingle()
            .NonLazy();
        }
    }
}