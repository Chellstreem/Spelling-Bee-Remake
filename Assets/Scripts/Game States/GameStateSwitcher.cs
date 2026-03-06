using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameStates
{
    public class GameStateSwitcher : IStateSwitcher
    {
        private readonly IStateInitializer stateInitializer;
        private GameState currentState;
        private GameStateType currentStateName;

        public GameState CurrentState => currentState;

        private readonly Dictionary<GameStateType, HashSet<GameStateType>> allowedTransitions =
            new Dictionary<GameStateType, HashSet<GameStateType>>
            {
                { GameStateType.Countdown, new HashSet<GameStateType> { GameStateType.Moving } },
                { GameStateType.Moving, new HashSet<GameStateType> { GameStateType.Loss, GameStateType.Victory } },
                { GameStateType.Loss, new HashSet<GameStateType> { }},
                { GameStateType.Victory, new HashSet<GameStateType>{ }}
            };

        public GameStateSwitcher(IStateInitializer stateInitializer)
        {
            this.stateInitializer = stateInitializer;
        }

        public void SetState(GameStateType newState)
        {
            if (CanTransitionTo(newState))
            {
                currentState?.Exit();
                currentState = stateInitializer.GetGameState(newState);
                currentStateName = newState;

                currentState.Enter();
            }
        }

        private bool CanTransitionTo(GameStateType state)
        {
            if (currentState == null)
                return true;

            if (allowedTransitions.TryGetValue(currentStateName, out var possibleTransitions))
            {
                return possibleTransitions.Contains(state);
            }

            return false;
        }
    }
}
