using GameStates;
using Installers;
using UnityEngine;

namespace GameModules
{
    [CreateAssetMenu(fileName = "Game State Module", menuName = "Scriptable Objects/Services/Game State Module")]
    public class GameStateModule : GameModule
    {
        public override void Install(GameServices services, SceneInstaller installer, GameConfig config)
        {
            GameStateController stateController = new(config.GameStateConfig);

            services.Register(stateController);
            stateController.Initialize(services);

            installer.DiContainer.Bind<GameStateController>()
                .FromInstance(stateController)
                .AsSingle()
                .NonLazy();
        }
    }
}