using GameModules;
using Installers;
using UnityEngine;

namespace GameStateModule
{
    [CreateAssetMenu(fileName = "Game State Module", menuName = "Game/Game States/Game State Module")]
    public class GameStateModule : GameModule
    {
        [SerializeField] private GameStateConfig _gameStateConfig;

        public override void Install(SceneInstaller installer, GameConfig config)
        {
            GameStateController stateController = new(_gameStateConfig, installer.DiContainer);

            installer.DiContainer.Bind<GameStateController>()
                .FromInstance(stateController)
                .AsSingle()
                .NonLazy();
        }
    }
}