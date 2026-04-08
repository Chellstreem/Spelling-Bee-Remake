using System;
using System.Collections.Generic;
using Zenject;

namespace GameStates
{
    public class GameStateController : IService
    {
        private readonly GameStateConfig config;
        private readonly DiContainer container;
        private readonly Dictionary<GameStateType, GameState> stateMap = new();

        public GameState CurrentState { get; private set; }
        public event Action OnStateChanged;

        public GameStateController(GameStateConfig config, DiContainer container)
        {
            this.config = config;
            this.container = container;

            CreateStates();
        }

        public void Initialize()
        {
            foreach (var kvp in stateMap)
                container.Inject(kvp.Value);
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

        private void CreateStates()
        {
            foreach (var definition in config.GameStates)
            {
                if (!stateMap.ContainsKey(definition.StateType))
                {
                    var state = definition.CreateGameState();
                    stateMap.Add(definition.StateType, state);
                }
            }
        }

        private bool CanTransitionTo(GameStateType stateType)
        {
            if (CurrentState == null)
                return true;

            return CurrentState.AllowTransitionTo(stateType);
        }
    }
}