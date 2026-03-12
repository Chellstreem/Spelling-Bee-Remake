using System;
using System.Collections.Generic;
using Spawn;
using Zenject;

namespace GameStates
{
    public class GameStateController
    {
        private readonly GameStateConfig config;
        private readonly Dictionary<GameStateType, GameState> stateMap = new();

        public GameState CurrentState { get; private set; }
        public event Action OnStateChanged;

        public GameStateController(GameStateConfig config) => this.config = config;

        public void Initialize(CoroutineRunner runner, Spawner spawner, GameSpeedController speedController)
        {
            foreach (var definition in config.GameStates)
            {
                if (!stateMap.ContainsKey(definition.StateType))
                {
                    var state = definition.CreateGameState(this, runner, spawner, speedController);
                    stateMap.Add(definition.StateType, state);
                }
            }
        }

        public void SetState(GameStateType newState)
        {
            if (CanTransitionTo(newState))
            {
                CurrentState?.Exit();

                CurrentState = GetGameState(newState);
                CurrentState.Enter();

                OnStateChanged?.Invoke();
            }
        }

        public GameState GetGameState(GameStateType type) => stateMap[type];

        private bool CanTransitionTo(GameStateType stateType)
        {
            if (CurrentState == null)
                return true;

            return CurrentState.AllowTransitionTo(stateType);
        }
    }
}