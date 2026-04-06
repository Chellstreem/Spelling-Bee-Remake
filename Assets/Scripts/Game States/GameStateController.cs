using System;
using System.Collections.Generic;

namespace GameStates
{
    public class GameStateController : IService
    {
        private readonly GameStateConfig config;
        private readonly Dictionary<GameStateType, GameState> stateMap = new();

        public GameState CurrentState { get; private set; }
        public event Action OnStateChanged;

        public GameStateController(GameStateConfig config) => this.config = config;

        public void Initialize(GameServices context)
        {
            foreach (var definition in config.GameStates)
            {
                if (!stateMap.ContainsKey(definition.StateType))
                {
                    var state = definition.CreateGameState(context);
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