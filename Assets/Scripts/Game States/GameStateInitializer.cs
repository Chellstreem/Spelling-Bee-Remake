using System.Collections.Generic;
using Zenject;

namespace GameStates
{
    public class GameStateInitializer : IStateInitializer
    {
        private readonly DiContainer container;
        private Dictionary<GameStateType, GameState> gameStates;

        public GameStateInitializer(DiContainer container)
        {
            this.container = container;
        }

        public void InitializeStates()
        {
            gameStates = new Dictionary<GameStateType, GameState>()
            {
            { GameStateType.Countdown, container.Resolve<CountdownState>() },
            { GameStateType.Moving, container.Resolve<MovingState>() },
            { GameStateType.Loss, container.Resolve<LossState>() },
            { GameStateType.Victory, container.Resolve<VictoryState>() }
        };
        }

        public GameState GetGameState(GameStateType gameState)
        {
            return gameStates.TryGetValue(gameState, out GameState StateObj) ? StateObj : null;
        }
    }
}