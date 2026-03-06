using System;
using System.Collections.Generic;

namespace GameStates
{
    public class GameStateController
    {
        private readonly Dictionary<GameStateType, GameState> gameStates = new();

        public GameState CurrentState { get; private set; }
        public event Action OnStateChanged;

        public GameStateController(GameStateConfig config) => InitializeStates(config.GameStates);

        public void SetState(GameStateType newState)
        {
            if (CanTransitionTo(newState))
            {
                if (CurrentState != null)
                    CurrentState.Exit();

                CurrentState = GetGameState(newState);
                CurrentState.Enter();

                OnStateChanged?.Invoke();
            }
        }

        public GameState GetGameState(GameStateType type) => gameStates[type];

        private void InitializeStates(GameState[] states)
        {
            foreach (var state in states)
            {
                if (!gameStates.ContainsKey(state.StateType))
                    gameStates.Add(state.StateType, state);
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