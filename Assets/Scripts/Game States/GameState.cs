using UnityEngine;
using Zenject;

namespace GameStates
{
    public class GameState
    {
        protected readonly GameStateDefinition definition;

        public GameStateController StateController { get; private set; }
        public CoroutineRunner Runner { get; private set; }
        public GameStateType StateType { get; private set; }
        public bool AllowMoving { get; private set; }

        public GameState(GameStateDefinition definition, GameStateController stateController, CoroutineRunner runner)
        {
            this.definition = definition;
            StateController = stateController;
            Runner = runner;
            StateType = definition.StateType;
            AllowMoving = definition.AllowMoving;
        }

        public void Enter() => definition.Enter(this);
        public void Exit() => definition.Exit(this);

        public bool AllowTransitionTo(GameStateType newStateType) => definition.AllowTransitionTo(newStateType);
    }
}
